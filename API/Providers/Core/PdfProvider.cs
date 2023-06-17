using Syncfusion.Pdf;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.OfficeChart;
using AutoMapper;
using Mobalyz.Domain.Odyssey.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Mobalyz.Odyssey.Resources.Data.Models;
using System.Reflection;
using Mobalyz.Odyssey.Resources.Data.Models.Integration;
using Microsoft.Extensions.Options;
using Mobalyz.Odyssey.Data.Models;
using Syncfusion.Pdf.Security;
using System.Globalization;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.Extensions.Caching.Memory;
using Mobalyz.Odyssey.Data.Cache;
using Microsoft.Data.SqlClient;
using System.Data;
using Syncfusion.Drawing;
using Mobalyz.Data;
using Mobalyz.Domain.Models;
using static Mobalyz.Domain.Odyssey.Models.Details;
using Mobalyz.Domain.Odyssey.Entities;
using Data.Networking;

namespace Mobalyz.Odyssey.Resources.Provider
{
    public class PdfProvider : IPdfCreationProvider
    {
        private readonly IMapper mapper;
        private readonly ILogger logger;
        private IConfiguration configuration;
        private readonly IDataRepository repository;
        private readonly IMemoryCache _templateCache;
        private readonly ResourceSettings resourceSettings;
        private readonly IRestClient restClient;

        public PdfProvider(
            IRestClient restClient,
            IMapper mapper,
            IDataRepository repository,
            ILogger<PdfProvider> logger,
            IConfiguration configuration,
            IMemoryCache templateCache,
            IOptionsMonitor<ResourceSettings> resourceOptionsMonitor)
        {
            this.restClient = restClient;
            this.mapper = mapper;
            this.repository = repository;
            _templateCache = templateCache;
            this.resourceSettings = resourceOptionsMonitor?.CurrentValue ?? throw new ArgumentNullException(nameof(resourceOptionsMonitor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> rescoreBlazeRequstXml()
        {
            List<Temp> tmpList = new List<Temp>();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.PersistSecurityInfo = false;
                builder.IntegratedSecurity = false;
                builder.UserID = "Sa";
                builder.Password = "Password09!";
                builder.InitialCatalog = "IRISLive";
                builder.DataSource = "DBIRIS01";
                builder.TrustServerCertificate = true;

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    string queryString = "SELECT t1.IncidentId,t1.Response,t1.CreatedDate FROM [ConsumerFinance_Logs].[dbo].[trnDataAuditLog] t1 " +
                        " WITH (NOLOCK) INNER JOIN ( SELECT IncidentId ,MAX(Createddate) AS LastBlazeCallDate " +
                        " FROM [ConsumerFinance_Logs].[dbo].[trnDataAuditLog] WITH (NOLOCK)" +
                        " WHERE SystemName = 'Blaze' AND Description = 'Blaze Integration Request (Input Data)'" +
                        " AND CreatedDate > '2023-03-20 00:00:00.063'" +
                        " AND CreatedDate < '2023-04-01 00:00:00.063'" +
                        " GROUP BY IncidentID) t2 ON t1.IncidentId = t2.IncidentId AND t1.CreatedDate = t2.LastBlazeCallDate";




                    //The One below is the correct one, that is what I run for DD everytime for paidout deals
                    //string queryString = "SELECT t1.IncidentId , t2.Response FROM [IRISLive].[dbo].[trnPayOut] t1 " +
                    // "CROSS APPLY ( SELECT TOP 1 * FROM [ConsumerFinance_Logs].[dbo].[trnDataAuditLog] t2 WHERE t1.IncidentId = t2.IncidentId " +
                    // "and SystemName = 'Blaze' " +
                    // "and Description = 'Blaze Integration Request (Input Data)' " +
                    // " ORDER BY t2.DataImportId DESC) t2 where t1.CreatedDate > '2023-03-01 00:00:00.063' and t1.CreatedDate < '2023-04-01 00:00:00.063' order by t1.CreatedDate asc";

                    //string queryString = "SELECT t1.IncidentId , t2.Response FROM [IRISLive].[dbo].[trnPayOut] t1 " +
                    //    "CROSS APPLY ( SELECT TOP 1 * FROM [ConsumerFinance_Logs].[dbo].[trnDataAuditLog] t2 WHERE t1.IncidentId = t2.IncidentId " +
                    //    "and SystemName = 'Blaze' " +
                    //    "and Description = 'Blaze Integration Request (Input Data)' " +
                    //    " ORDER BY t2.DataImportId DESC) t2 where t1.IncidentId < '110949' order by t1.IncidentId asc";

                    // > '159337'  159349   where t1.IncidentId = '100005'    where t1.IncidentId > '159337'
                    //string queryString = "SELECT top (20) t1.IncidentId , t2.Response FROM [IRISLive].[dbo].[trnPayOut] t1 " +
                    //    "CROSS APPLY ( SELECT TOP 1 * FROM [ConsumerFinance_Logs].[dbo].[trnDataAuditLog] t2 WHERE t1.IncidentId = t2.IncidentId " +
                    //    "and SystemName = 'Blaze' " +
                    //    "and Description = 'Blaze Integration Request (Input Data)' " +
                    //    " ORDER BY t2.DataImportId DESC) t2 where t1.IncidentId = '100204'";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        SqlDataReader dr = command.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                long IncidentNumber = 0;
                                string BlazeRequestXml = "";

                                IncidentNumber = dr.GetInt64(0);
                                BlazeRequestXml = dr.GetString(1);

                                String XmlRequest = BlazeRequestXml.Substring(BlazeRequestXml.IndexOf("<request>"));
                                XmlRequest = XmlRequest.Replace("</Message>", "");
                                XmlRequest = XmlRequest.Replace("<dealerData>", "<dealerData><settlementAmt>0</settlementAmt>");

                                XmlRequest = Regex.Replace(XmlRequest, @"\r\n?|\n", "");

                                Temp tmpObj = new Temp
                                {
                                    incidentNo = IncidentNumber,
                                    BlazeRequestXml = XmlRequest
                                };

                                tmpList.Add(tmpObj);
                            }
                        }

                        if (connection.State != ConnectionState.Closed)
                            connection.Close();

                        foreach (Temp tmpOb in tmpList)
                        {
                            string blazeRequest = tmpOb.BlazeRequestXml;

                            BlazeSubmissionMessage submissionMessage = new BlazeSubmissionMessage
                            {
                                data = blazeRequest,
                                token = ""
                            };

                            var scoreEndpoint = "http://localhost:12001/v1/blaze/score";

                            var message = JsonConvert.SerializeObject(submissionMessage);

                            try
                            {
                                var response = await this.restClient.PostAsync(
                                        scoreEndpoint,
                                        options =>
                                        {
                                            options.BodyContentType = "application/json";
                                            options.Body = submissionMessage;
                                        });
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            string path = @"C:\temp\extract_" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + ".csv";
            string delimiter = ", ";
            string headers = "IncidentNumber" + delimiter + "Instalment_Amount" + delimiter + "Account_Type_Desc" + delimiter +
                "Account_Type" + Environment.NewLine;


            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, headers);

            string createText = "";
            foreach (Temp tmpOb in tmpList)
            {
                createText = tmpOb.incidentNo + delimiter + tmpOb.CompuInstalment_Amount + delimiter + tmpOb.CompuAccount_Type_Desc + delimiter + tmpOb.CompuAccount_Type + Environment.NewLine;
                File.AppendAllText(path, createText);
            }
            return true;
        }

        public async Task<DocumentResponseDto> GenerateContract(ContractRequestDto request)
        {
            DateTime timeStart = DateTime.Now;
            this.logger.LogInformation($"(CLog) Creating contract for {request.page1.DealAccountNumber}");
            this.logger.LogInformation($"(CLog) Creating contract for {JsonConvert.SerializeObject(request)}");

            request.DateAndTime = DateTime.Now.ToString();

            request.page1.ClientStreetAddress = Regex.Replace(request.page1.ClientStreetAddress, @"\s+", " ");
            request.page1.ClientStreetAddress1 = Regex.Replace(request.page1.ClientStreetAddress1, @"\s+", " ");
            request.page9.HomeAddress1 = Regex.Replace(request.page9.HomeAddress1, @"\s+", " ");
            request.page9.HomeAddress2 = Regex.Replace(request.page9.HomeAddress2, @"\s+", " ");

            request.page1.ClientStreetAddress = request.page1.ClientStreetAddress.Replace("\\r\\n", " ").ToTitleCase();
            request.page1.ClientStreetAddress1 = request.page1.ClientStreetAddress1.Replace("\\r\\n", " ").ToTitleCase();
            request.page9.HomeAddress1 = request.page9.HomeAddress1.Replace("\\r\\n", " ").ToTitleCase();
            request.page9.HomeAddress2 = request.page9.HomeAddress2.Replace("\\r\\n", " ").ToTitleCase();

            var newVaps = request.vaps;

            List<ContractVaps> TmpContractVaps = new List<ContractVaps>();

            foreach (ContractVaps vap in newVaps)
            {
                TmpContractVaps.Add(vap);
            }

            request.vaps.Clear();
            request.vaps = TmpContractVaps;

            var response = new DocumentResponseDto
            {
                documentVersion = request.ContractNumber,
                pdfDocument = await CreateContractContent(request)
            };

            this.logger.LogInformation($"(CLog) Completed creating contract for {request.page1.DealAccountNumber} version {request.ContractNumber}");

            return response;
        }

        private async Task<byte[]> CreateContractContent(ContractRequestDto request)
        {
            var templateName = request.Category.ToLower() == "cata" ? "catacontract" : request.Category.ToLower();

            var templateBytes = await GetTemplateByTemplateName(templateName);

            var contents = new MemoryStream(templateBytes);

            this.logger.LogInformation($"(CLog) Using cached template to create a contract with name {templateName}");

            try
            {
                    using (WordDocument wordDocument = new WordDocument(contents, Syncfusion.DocIO.FormatType.Automatic))
                    {
                        ContractValues contractValues = new ContractValues(request);

                    if (request.page1.PCCurSymb.ToLower() == "r")
                    {
                        wordDocument.Replace("_QR", "R", false, true);
                    } else
                    {
                        wordDocument.Replace("_QR", "", false, true);
                    }

                        foreach (PropertyInfo prop in contractValues.GetType().GetProperties())
                        {
                            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            if (type == typeof(String))
                            {
                                Console.WriteLine(prop.Name.ToLower() + " - " + prop.GetValue(contractValues, null).ToString());
                                wordDocument.Replace(prop.Name.ToLower(), prop.GetValue(contractValues, null).ToString() == null ? "" : prop.GetValue(contractValues, null).ToString(), false, true);
                            }
                        }

                        int vapsCount = 1;
                        foreach (ContractVaps vaps in request.vaps)
                        {
                            wordDocument.Replace("vaps" + vapsCount, vaps.VapS == null ? "" : vaps.VapS, false, true);
                            wordDocument.Replace("vc" + vapsCount, vaps.VapC == null ? "" : vaps.VapC, false, true);
                            wordDocument.Replace("vaps" + vapsCount + "Amt", vaps.VapsAmt == null ? "" : vaps.VapsAmt, false, true);
                            vapsCount++;
                        }
                        while (vapsCount <= 8)
                        {
                            wordDocument.Replace("vaps" + vapsCount, "", false, true);
                            wordDocument.Replace("vc" + vapsCount, "", false, true);
                            wordDocument.Replace("vaps" + vapsCount + "Amt", "", false, true);
                            vapsCount++;
                        }

                    DateTime dt = DateTime.Now;
                    //this.logger.LogInformation("(Clog) replacing short date 1");
                    wordDocument.Replace("__ShortDate", dt.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo), false, true);

                    using (DocIORenderer renderer = new DocIORenderer())
                        {
                            renderer.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
                            renderer.Settings.EmbedCompleteFonts = true;

                            using (PdfDocument pdfDocument = renderer.ConvertToPDF(wordDocument))
                            {
                                string pdfFilePath = $"{this.resourceSettings.TempFolder}\\Cont_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".pdf";

                                using (FileStream outputStream = new FileStream(Path.GetFullPath(pdfFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                                {
                                    pdfDocument.Save(outputStream);

                                    var memoryStream = new MemoryStream();

                                    outputStream.Position = 0;
                                    outputStream.CopyTo(memoryStream);

                                    byte[] pdfBytes = memoryStream.ToArray();

                                    if (!ResourceSettingsAccessor.Current.SaveContractFile)
                                    {
                                        Task.Run(() =>
                                        {
                                            removeCreatedPdf(pdfFilePath);
                                        }).ConfigureAwait(false);
                                    }

                                    return pdfBytes;
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"(CLog) Something went wrong while creating the contract {ex}");
            }
            return null;
        }

        private async void removeCreatedPdf(string pdfFilePath)
        {
            Thread.Sleep(20000);
            File.Delete(pdfFilePath);
        }

        public async Task<DocumentResponseDto> GeneratePodium(PodiumRequestDto request)
        {
            this.logger.LogInformation($"(CLog) creating podium with data {JsonConvert.SerializeObject(request)}");
            DateTime timeStart = DateTime.Now;

            request.DateAndTime = timeStart.ToString();

            var response = new DocumentResponseDto
            {
                documentVersion = request.PodiumNumber,
                pdfDocument = await CreatePodiumContent(request)
            };

            return response;
        }
        

        public async Task<DocumentResponseDto> GenerateInsuranceProposal(InsuranceRequestDto request)
        {
            this.logger.LogInformation($"(CLog) creating insurance proposal with data {JsonConvert.SerializeObject(request)}");
            DateTime timeStart = DateTime.Now;

            request.TodayDate = DateTime.Now.ToString("yyyy-MM-dd");

            var response = new DocumentResponseDto
            {
                documentVersion = 0,
                pdfDocument = await CreateInsuranceProposalContent(request)
            };

            return response;
        }

        public async Task<DocumentResponseDto> GenerateInsurancePolicy(InsuranceRequestDto request)
        {
            this.logger.LogInformation($"(CLog) creating insurance policy with data {JsonConvert.SerializeObject(request)}");
            DateTime timeStart = DateTime.Now;

            request.TodayDate = DateTime.Now.ToString("yyyy-MM-dd");

            var response = new DocumentResponseDto
            {
                documentVersion = 0,
                pdfDocument = await CreateInsurancePolicyContent(request)
            };

            return response;
        }

        public async Task<DocumentResponseDto> GenerateDicInvoice(DicInvoiceRequest request)
        {
            DateTime timeStart = DateTime.Now;

            Serilog.Log.Logger.Information($"(CLog) Getting latest DIC podium for - {request.DealAcountNr}");

            request.CurrentDateTime = timeStart.ToString();

            var response = new DocumentResponseDto
            {
                documentVersion = 0,
                pdfDocument = await CreateDicPodiumContent(request, 0)
            };

            return response;
        }


        private async Task<byte[]> CreatePodiumContent(PodiumRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var templateBytes = await GetTemplateByTemplateName(request.Category);

            var contents = new MemoryStream(templateBytes);

            this.logger.LogInformation($"(CLog) Using cached template to create a podium with name {request.Category}");

            try
            {
                    using (WordDocument wordDocument = new WordDocument(contents, Syncfusion.DocIO.FormatType.Automatic))
                    {
                        PodiumValues podiumValues = new PodiumValues(request);

                        foreach (PropertyInfo prop in podiumValues.GetType().GetProperties())
                        {
                            var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            if (type == typeof(String))
                            {
                                wordDocument.Replace(prop.Name, prop.GetValue(podiumValues, null).ToString() == null ? "" : prop.GetValue(podiumValues, null).ToString(), false, true);
                            }
                        }

                        int CredReqCount = 1;
                        foreach (PodCreditRequirements credReq in request.CreditRequirements)
                        {
                            wordDocument.Replace("Cr" + CredReqCount.ToString(), CredReqCount.ToString() + ".", false, true);
                            wordDocument.Replace("CreditRequirement" + CredReqCount.ToString(), credReq.creditRequirement == null ? "" : credReq.creditRequirement, false, true);
                            CredReqCount++;
                        }

                        if (request.DealStatus == "Validations Pending" || request.DealStatus == "Payout Pending")
                        {
                            wordDocument.Replace("CreditReqm", "Pending Requirements", false, true);
                        }


                        while (CredReqCount <= 8)
                        {
                            wordDocument.Replace("Cr" + CredReqCount.ToString(), "", false, true);
                            wordDocument.Replace("CreditRequirement" + CredReqCount.ToString(), "", false, true);
                            CredReqCount++;
                        }

                        using (DocIORenderer renderer = new DocIORenderer())
                        {
                            renderer.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
                            renderer.Settings.EmbedCompleteFonts = true;

                            using (PdfDocument pdfDocument = renderer.ConvertToPDF(wordDocument))
                            {
                                string pdfFilePath = $"{this.resourceSettings.TempFolder}\\Pod_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".pdf";

                                using (FileStream outputStream = new FileStream(Path.GetFullPath(pdfFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                                {
                                    pdfDocument.Save(outputStream);
                                    pdfDocument.Close();

                                    var memoryStream = new MemoryStream();

                                    outputStream.Position = 0;
                                    outputStream.CopyTo(memoryStream);

                                    byte[] pdfBytes = memoryStream.ToArray();

                                    if (!ResourceSettingsAccessor.Current.SaveContractFile)
                                    {
                                        Task.Run(() =>
                                        {
                                            removeCreatedPdf(pdfFilePath);
                                        }).ConfigureAwait(false);
                                    }

                                this.logger.LogInformation($"(CLog) finished creating podium for {request.DealAccountNr}");

                                return pdfBytes;
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"(CLog) Something went wrong while creating podium {request.DealAccountNr}");

                throw ex;
            }
        }

        private async Task<byte[]> CreateInsuranceProposalContent(InsuranceRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string templateName = "insuranceproposal";

            var templateBytes = await GetTemplateByTemplateName(templateName);

            var contents = new MemoryStream(templateBytes);

            this.logger.LogInformation($"(CLog) Using Insurance template to create InsuranceProposal");

            try
            {
                using (WordDocument wordDocument = new WordDocument(contents, Syncfusion.DocIO.FormatType.Automatic))
                {
                    InsuranceValues insuranceProposalValues = new InsuranceValues(request);

                    foreach (PropertyInfo prop in insuranceProposalValues.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        if (type == typeof(String))
                        {
                            Console.WriteLine(prop.Name + " - " + prop.GetValue(insuranceProposalValues, null).ToString());
                            wordDocument.Replace(prop.Name, prop.GetValue(insuranceProposalValues, null).ToString() == null ? "" : prop.GetValue(insuranceProposalValues, null).ToString(), false, true);
                        }
                    }

                    using (DocIORenderer renderer = new DocIORenderer())
                    {
                        renderer.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
                        renderer.Settings.EmbedCompleteFonts = true;

                        using (PdfDocument pdfDocument = renderer.ConvertToPDF(wordDocument))
                        {
                            string pdfFilePath = $"{this.resourceSettings.TempFolder}\\InsPro_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".pdf";

                            using (FileStream outputStream = new FileStream(Path.GetFullPath(pdfFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                            {
                                pdfDocument.Save(outputStream);

                                var memoryStream = new MemoryStream();

                                outputStream.Position = 0;
                                outputStream.CopyTo(memoryStream);

                                byte[] pdfBytes = memoryStream.ToArray();

                                if (!ResourceSettingsAccessor.Current.SaveContractFile)
                                {
                                    Task.Run(() =>
                                    {
                                        removeCreatedPdf(pdfFilePath);
                                    }).ConfigureAwait(false);
                                }
                                this.logger.LogInformation($"(CLog) Completed creating insurance Proposal for {request.PolNumber}");
                                return pdfBytes;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"(CLog) There was an exception during the Insurance Proposal creation process. error - {ex.Message}");
                throw ex;
            }
        }

        private async Task<byte[]> CreateInsurancePolicyContent(InsuranceRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string templateName = "insurancepolicy";

            var templateBytes = await GetTemplateByTemplateName(templateName);

            var contents = new MemoryStream(templateBytes);

            this.logger.LogInformation($"(CLog) Using Insurance policy template to create policy for {request.PolNumber}");

            try
            {
                using (WordDocument wordDocument = new WordDocument(contents, Syncfusion.DocIO.FormatType.Automatic))
                {
                    InsuranceValues insurancePolicyValues = new InsuranceValues(request);

                    foreach (PropertyInfo prop in insurancePolicyValues.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        if (type == typeof(String))
                        {
                            Console.WriteLine(prop.Name + " - " + prop.GetValue(insurancePolicyValues, null).ToString());
                            wordDocument.Replace(prop.Name, prop.GetValue(insurancePolicyValues, null).ToString() == null ? "" : prop.GetValue(insurancePolicyValues, null).ToString(), false, true);
                        }
                    }

                    using (DocIORenderer renderer = new DocIORenderer())
                    {
                        renderer.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
                        renderer.Settings.EmbedCompleteFonts = true;

                        using (PdfDocument pdfDocument = renderer.ConvertToPDF(wordDocument))
                        {
                            string pdfFilePath = $"{this.resourceSettings.TempFolder}\\InsPol_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".pdf";

                            using (FileStream outputStream = new FileStream(Path.GetFullPath(pdfFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                            {
                                pdfDocument.Save(outputStream);

                                var memoryStream = new MemoryStream();

                                outputStream.Position = 0;
                                outputStream.CopyTo(memoryStream);

                                byte[] pdfBytes = memoryStream.ToArray();

                                if (!ResourceSettingsAccessor.Current.SaveContractFile)
                                {
                                    Task.Run(() =>
                                    {
                                        removeCreatedPdf(pdfFilePath);
                                    }).ConfigureAwait(false);
                                }
                                this.logger.LogInformation($"(CLog) Done creating policy for {request.PolNumber}");
                                return pdfBytes;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"(CLog) There was an exception during the Insurance Policy creation process. error - {ex.Message}");
                throw;
            }
        }

        private async Task<byte[]> CreateDicPodiumContent(DicInvoiceRequest request, int DocNr1)
        {
            if (request == null)
            {
                this.logger.LogError($"(CLog) The request to create dic podium was empty - docnr = {DocNr1}");
                throw new ArgumentNullException(nameof(request));
            }

            string templateName = "DICInvoice";

            var templateBytes = await GetTemplateByTemplateName(templateName);

            var contents = new MemoryStream(templateBytes);

            this.logger.LogInformation($"(CLog) Creating DicInvoice for {request.ClientIdNumber}");

            try
            {
                using (WordDocument wordDocument = new WordDocument(contents, Syncfusion.DocIO.FormatType.Automatic))
                {
                    wordDocument.Replace("invNumber", request.invNumber == null ? "" : request.invNumber, false, true);
                    wordDocument.Replace("CurrentDateTime", request.CurrentDateTime == null ? "" : request.CurrentDateTime, false, true);
                    wordDocument.Replace("DicAmount", request.RemitDic == null ? "" : request.DicAmount, false, true);
                    wordDocument.Replace("DicVatAmount", request.DicVatAmount == null ? "" : request.DicVatAmount, false, true);
                    wordDocument.Replace("TotalDicAmount", request.TotalDicAmount == null ? "" : request.TotalDicAmount, false, true);
                    wordDocument.Replace("DealAcountNr", request.DealAcountNr == null ? "" : request.DealAcountNr, false, true);
                    wordDocument.Replace("DealerName", request.DealerName == null ? "" : request.DealerName, false, true);
                    wordDocument.Replace("FandIName", request.FandIName == null ? "" : request.FandIName, false, true);
                    wordDocument.Replace("ClientName", request.ClientName == null ? "" : request.ClientName, false, true);
                    wordDocument.Replace("ClientIdNumber", request.ClientIdNumber == null ? "" : request.ClientIdNumber, false, true);
                    wordDocument.Replace("NewOrUsed", request.NewOrUsed == null ? "" : request.NewOrUsed, false, true);
                    wordDocument.Replace("VehicleYear", request.VehicleYear == null ? "" : request.VehicleYear, false, true);
                    wordDocument.Replace("VehMake", request.VehMake == null ? "" : request.VehMake, false, true);
                    wordDocument.Replace("VehModel", request.VehModel == null ? "" : request.VehModel, false, true);
                    wordDocument.Replace("RemitDate", request.RemitDate == null ? "" : request.RemitDate, false, true);
                    wordDocument.Replace("CashPrice", request.CashPrice == null ? "" : request.CashPrice, false, true);
                    wordDocument.Replace("RemitVat", request.RemitVat == null ? "" : request.RemitVat, false, true);
                    wordDocument.Replace("RemitNoVatExtras", request.RemitNoVatExtras == null ? "" : request.RemitNoVatExtras, false, true);
                    wordDocument.Replace("RemitVatableExtras", request.RemitVatableExtras == null ? "" : request.RemitVatableExtras, false, true);
                    wordDocument.Replace("RemitDeposit", request.RemitDeposit == null ? "" : request.RemitDeposit, false, true);
                    wordDocument.Replace("RemitTotal", request.RemitTotal == null ? "" : request.RemitTotal, false, true);
                    wordDocument.Replace("RemitDic", request.RemitDic == null ? "" : request.RemitDic, false, true);
                    wordDocument.Replace("RemitTotalAmtPay", request.RemitTotalAmtPay == null ? "" : request.RemitTotalAmtPay, false, true);

                    int noteCount = 1;

                    foreach (notes note in request.Notes)
                    {
                        wordDocument.Replace("Note" + noteCount, note.Note == null ? "" : note.Note, false, true);
                        noteCount++;
                    }

                    while(noteCount < 8)
                    {
                        wordDocument.Replace("Note" + noteCount, "", false, true);
                        noteCount++;
                    }

                    using (DocIORenderer renderer = new DocIORenderer())
                    {
                        renderer.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
                        renderer.Settings.EmbedCompleteFonts = true;

                        using (PdfDocument pdfDocument = renderer.ConvertToPDF(wordDocument))
                        {
                            string pdfFilePath = $"{this.resourceSettings.TempFolder}\\DicInv_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".pdf";

                            using (FileStream outputStream = new FileStream(Path.GetFullPath(pdfFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                            {
                                pdfDocument.Save(outputStream);

                                var memoryStream = new MemoryStream();

                                outputStream.Position = 0;
                                outputStream.CopyTo(memoryStream);

                                byte[] pdfBytes = memoryStream.ToArray();

                                if (!ResourceSettingsAccessor.Current.SaveContractFile)
                                {
                                    Task.Run(() =>
                                    {
                                        removeCreatedPdf(pdfFilePath);
                                    }).ConfigureAwait(false);
                                }

                                this.logger.LogInformation($"(CLog) Copleted creating DicInvoice for {request.ClientIdNumber}");
                                return pdfBytes;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"(CLog) There was an exception during the DIC creation process. error - {ex.Message}");
                throw;
            }
        }


        public async Task<GenericResponseDto> GenerateGenericPdfAsync(GenericRequestDto request)
        {
            var response = new GenericResponseDto
            {
                pdfDocument = await CreateGenericPdfContent(request)
            };

            return response;
        }

        private async Task<byte[]> CreateGenericPdfContent(GenericRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var templateBytes = new byte[] { };

            if (request.pdfTemplate == null)
            {
                if (!string.IsNullOrEmpty(request.PdfTemplateName))
                {
                    templateBytes = await GetTemplateByTemplateName(request.PdfTemplateName);

                    this.logger.LogInformation($"(CLog) Creating generic pdf with template {request.PdfTemplateName}");
                } else
                {
                    this.logger.LogError($"(CLog) no Template base64 or template name was sent in. {request.PdfTemplateName}");
                }
            }
            else
            {
                templateBytes = request.pdfTemplate;

                this.logger.LogInformation($"(CLog) Creating generic pdf with passed in template");
            }

            try
            {
                var contents = new MemoryStream(templateBytes);

                using (WordDocument wordDocument = new WordDocument(contents, Syncfusion.DocIO.FormatType.Automatic))
                {
                    if(request.PdfTemplateName != "fullterms")
                    { 
                    DateTime dt = DateTime.Now;

                    wordDocument.Replace("__Date", dt.ToString("dd MMMM yyyy", DateTimeFormatInfo.InvariantInfo), false, true);
                    //this.logger.LogInformation("(Clog) replacing short date 2");
                    wordDocument.Replace("__ShortDate", dt.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo), false, true);
                    wordDocument.Replace("__DateTime", dt.ToString("dd MM yyyy", DateTimeFormatInfo.InvariantInfo) + " " + dt.ToString("t", DateTimeFormatInfo.InvariantInfo), false, true);
                    wordDocument.Replace("__Weekday", dt.ToString("dddd", DateTimeFormatInfo.InvariantInfo), false, true);
                    wordDocument.Replace("__Month", dt.ToString("MMMM", DateTimeFormatInfo.InvariantInfo), false, true);
                    wordDocument.Replace("__Time", dt.ToString("t", DateTimeFormatInfo.InvariantInfo), false, true);

                    ///table MailMerge start
                    if (request.details.ItemHeadings != null && request.details.RowItems != null)
                    {
                        if (request.details.ItemHeadings.Count > 0 && request.details.RowItems.Count > 0)
                        {
                            DataSet ds = GetData(request.details.ItemHeadings, request.details.RowItems);
                            wordDocument.MailMerge.ExecuteGroup(ds.Tables["LS"]);
                        }
                    }
                    ///table MailMerge end

                    if (request.details.RowItems?.Count > 0)
                    {
                        int rowCount = 1;
                        foreach (genList item in request.details.RowItems)
                        {
                            wordDocument.Replace("__row" + rowCount + "Item0", item.__Item0 == null ? "" : item.__Item0, false, true);
                            wordDocument.Replace("__row" + rowCount + "Item1", item.__Item1 == null ? "" : item.__Item1, false, true);
                            wordDocument.Replace("__row" + rowCount + "Item2", item.__Item2 == null ? "" : item.__Item2, false, true);
                            wordDocument.Replace("__row" + rowCount + "Item3", item.__Item3 == null ? "" : item.__Item3, false, true);
                            wordDocument.Replace("__row" + rowCount + "Item4", item.__Item4 == null ? "" : item.__Item4, false, true);

                            rowCount++;
                        }

                        while (rowCount < 20)
                        {
                            wordDocument.Replace("__row" + rowCount + "Item0", "", false, true);
                            wordDocument.Replace("__row" + rowCount + "Item1", "", false, true);
                            wordDocument.Replace("__row" + rowCount + "Item2", "", false, true);
                            wordDocument.Replace("__row" + rowCount + "Item3", "", false, true);
                            wordDocument.Replace("__row" + rowCount + "Item4", "", false, true);
                            rowCount++;
                        };
                    }

                    GenericValues genericValues = new GenericValues(request);

                    foreach (PropertyInfo prop in genericValues.GetType().GetProperties())
                    {
                        var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        if (type == typeof(String))
                        {
                            Console.WriteLine(prop.Name + " - " + prop.GetValue(genericValues, null).ToString());
                            wordDocument.Replace(prop.Name, prop.GetValue(genericValues, null).ToString() == null ? "" : prop.GetValue(genericValues, null).ToString(), false, true);
                        }
                    }

                    int GenericRowCount = 1;
                    int GenericItemCount = 1;
                }
                    string fileName = "Gen_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".pdf";

                    using (DocIORenderer renderer = new DocIORenderer())
                    {
                        renderer.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
                        renderer.Settings.EmbedCompleteFonts = true;

                        using (PdfDocument pdfDocument = renderer.ConvertToPDF(wordDocument))
                        {
                            string pdfFilePath = $"{this.resourceSettings.TempFolder}\\{fileName}";

                            using (FileStream outputStream = new FileStream(Path.GetFullPath(pdfFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                            {
                                if (request.PasswordProtect == true)
                                {
                                    PdfSecurity security = pdfDocument.Security;
                                    security.KeySize = PdfEncryptionKeySize.Key256BitRevision6;
                                    security.Algorithm = PdfEncryptionAlgorithm.AES;
                                    security.UserPassword = request.Password;
                                }

                                pdfDocument.Save(outputStream);
                                pdfDocument.Close();

                                var memoryStream = new MemoryStream();

                                outputStream.Position = 0;
                                outputStream.CopyTo(memoryStream);

                                byte[] pdfBytes = memoryStream.ToArray();

                                this.logger.LogInformation($"(CLog) Done creating generic pdf");

                                if (request.SendEmail == true)
                                {
                                    Task.Factory.StartNew(async () =>
                                    {
                                        this.logger.LogInformation($"sending email for {request.PdfTemplateName}");
                                        if (request.SendTo.Count == 0) throw new ArgumentException("An email address is required");
                                        try
                                        {
                                            await SendMail1(request.SendTo, pdfFilePath, true, request.MailBody, request.MailSubject, request.HtmlTemplateName);
                                        }
                                        catch (Exception ex)
                                        {
                                            this.logger.LogInformation($"There was a problem sendint the email {ex}");
                                            throw ex;
                                        }
                                    });
                                }
                                return pdfBytes;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"(CLog) There was an exception during the Generic Pdf creation process. error - {ex.Message}");
                throw;
            }
        }

        private static DataSet GetData(List<string> ItemHeadings, List<genList> RowItems)
        {
            DataSet ds = new DataSet();

            if (RowItems.Count > 0)
            {
                DataRow row;
                ds.Tables.Add();

                ds.Tables[0].TableName = "LS";

                foreach (string Heading in ItemHeadings)
                {
                    ds.Tables[0].Columns.Add(Heading);
                }

                int count = 0;

                string heading0 = "";
                string heading1 = "";
                string heading2 = "";
                string heading3 = "";
                string heading4 = "";
                string heading5 = "";

                if (ItemHeadings.Count > 0) { heading0 = ItemHeadings[0] == null ? "" : ItemHeadings[0]; }
                if (ItemHeadings.Count > 1) { heading1 = ItemHeadings[1] == null ? "" : ItemHeadings[1]; }
                if (ItemHeadings.Count > 2) { heading2 = ItemHeadings[2] == null ? "" : ItemHeadings[2]; }
                if (ItemHeadings.Count > 3) { heading3 = ItemHeadings[3] == null ? "" : ItemHeadings[3]; }
                if (ItemHeadings.Count > 4) { heading4 = ItemHeadings[4] == null ? "" : ItemHeadings[4]; }
                if (ItemHeadings.Count > 5) { heading5 = ItemHeadings[5] == null ? "" : ItemHeadings[5]; }


                foreach (genList Item in RowItems)
                {
                    row = ds.Tables["LS"].NewRow();
                    if (heading0 != "") { row[heading0] = Item.__Item0 != null ? Item.__Item0 : ""; }
                    if (heading1 != "") { row[heading1] = Item.__Item1 != null ? Item.__Item1 : ""; }
                    if (heading2 != "") { row[heading2] = Item.__Item2 != null ? Item.__Item2 : ""; }
                    if (heading3 != "") { row[heading3] = Item.__Item3 != null ? Item.__Item3 : ""; }
                    if (heading4 != "") { row[heading4] = Item.__Item4 != null ? Item.__Item4 : ""; }
                    if (heading5 != "") { row[heading5] = Item.__Item5 != null ? Item.__Item5 : ""; }

                    ds.Tables["LS"].Rows.Add(row);
                    count++;
                }
            }
            return ds;
        }

        public async Task<string> GetTemplateNameAsync(dynamic request)
        {
            string pdfTemplate = "";
            foreach (KeyValuePair<string, object> kvp in ((IDictionary<string, object>)request))
            {
                if (kvp.Key.ToLower() == "pdftemplatename")
                {
                    try
                    {
                        pdfTemplate = kvp.Value.ToString();

                        if (!string.IsNullOrEmpty(pdfTemplate))
                        { return pdfTemplate; }
                        
                        continue;
                    }
                    catch { }
                }


                try
                {
                    pdfTemplate = request.pdfTemplateName;

                    if (!string.IsNullOrEmpty(pdfTemplate))
                    { return pdfTemplate; }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (string.IsNullOrEmpty(pdfTemplate))
                    {
                        pdfTemplate = request.PdfTemplateName;

                        if (!string.IsNullOrEmpty(pdfTemplate))
                        { return pdfTemplate; }
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (string.IsNullOrEmpty(pdfTemplate))
                    {
                        pdfTemplate = request.Category;

                        if (!string.IsNullOrEmpty(pdfTemplate))
                        { return pdfTemplate; }
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (string.IsNullOrEmpty(pdfTemplate))
                    {
                        pdfTemplate = request.category;

                        if (!string.IsNullOrEmpty(pdfTemplate))
                        { return pdfTemplate; }
                    }
                }
                catch (Exception)
                {
                }
            }

            return pdfTemplate;
        }

        public async Task<DocumentResponseDto> CreatePdf(dynamic request)
        {
            this.logger.LogInformation($"(CLog) Creating document from Generic body for {JsonConvert.SerializeObject(request)}");

            List<string> ItemHeadings = new List<string>();
            List<string> SendTo = new List<string>();
            List<string> Notes = new List<string>();
            List<string> MailReplace = new List<string>();
            List<genList> RowItems = new List<genList>();
            List<ContractVaps> Vaps = new List<ContractVaps>();
            List<credReq> CreditReq = new List<credReq>();


            //List<string> ItemHeadings, List< genList > RowItems

            var templateBytes = new byte[] { };
            string pdfTemplate = "";
            string htmlTemplateName = "";
            string title = "";
            string name = "";
            string surname = "";
            bool sendmail = false;
            bool ishtml = false;
            string mailfrom = "";
            bool passwordProtect = false;
            string password = "";
            string mailSubject = "";
            string attachmentFileName = "";

            pdfTemplate = await GetTemplateNameAsync(request);

            //templateBytes = await GetTemplateByTemplateName("GFSContractGenNewNames");
            templateBytes = await GetTemplateByTemplateName(pdfTemplate);

            var contents = new MemoryStream(templateBytes);

            using (WordDocument wordDocument = new WordDocument(contents, Syncfusion.DocIO.FormatType.Automatic))
            {
                foreach (KeyValuePair<string, object> kvp in ((IDictionary<string, object>)request))
                {
                    if (kvp.Key.ToLower() == "mailtemplatename")
                    {
                        htmlTemplateName = kvp.Value.ToString();
                        continue;
                    }

                    if (kvp.Key.ToLower() == "attachmentfilename")
                    {
                        attachmentFileName = kvp.Value.ToString();
                        continue;
                    }

                    

                    if (kvp.Key.ToLower() == "sendemail")
                    {
                        sendmail = Convert.ToBoolean(kvp.Value.ToString());
                        continue;
                    }

                    if (kvp.Key.ToLower() == "mailbodyishtml")
                    {
                        ishtml = Convert.ToBoolean(kvp.Value.ToString());
                        continue;
                    }

                    if (kvp.Key.ToLower() == "passwordprotect")
                    {
                        passwordProtect = Convert.ToBoolean(kvp.Value.ToString());
                        continue;
                    }

                    if (kvp.Key.ToLower() == "password")
                    {
                        password = kvp.Value.ToString();
                        continue;
                    }

                    if (kvp.Key.ToLower() == "mailsubject")
                    {
                        mailSubject = kvp.Value.ToString();
                        continue;
                    }
                    


                    if (kvp.Key.ToLower() == "mailfrom")
                    {
                        mailfrom = kvp.Value.ToString();
                        continue;
                    }

                        try
                    {
                        List<object> list = (List<object>)kvp.Value;

                            if (kvp.Key.ToLower() == "itemheadings")
                            {
                                try
                                {
                                    List<object> itemList = (List<object>)kvp.Value;
                                    foreach (var item in itemList)
                                    {
                                        if (!string.IsNullOrEmpty(item.ToString()))
                                        {
                                            ItemHeadings.Add(item.ToString());
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                }
                            }

                            if (kvp.Key.ToLower() == "sendto")
                            {
                                try
                                {
                                    List<object> itemList = (List<object>)kvp.Value;
                                    foreach (var item in itemList)
                                    {
                                        SendTo.Add(item.ToString());
                                    }
                                    continue;
                                }
                                catch
                                {
                                }
                            }



                        if (kvp.Key.ToLower() == "notes")
                            {
                                try
                                {
                                    List<object> itemList = (List<object>)kvp.Value;
                                    foreach (var item in itemList)
                                    {
                                        if (!string.IsNullOrEmpty(item.ToString()))
                                        {
                                            var notes = ((IDictionary<string, object>)item).Values;
                                            foreach (var note in notes)
                                            {
                                                Notes.Add(note.ToString());
                                            }
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                }
                            }

                        if (kvp.Key.ToLower() == "rowitems")
                            {
                                try
                                {
                                    List<object> itemList = (List<object>)kvp.Value;
                                    foreach (var item in itemList)
                                    {
                                    var item0 = "";
                                    var item1 = "";
                                    var item2 = "";
                                    var item3 = "";
                                    var item4 = "";
                                    var item5 = "";


                                    if (!string.IsNullOrEmpty(item.ToString()))
                                        {
                                            var rwItms = ((IDictionary<string, object>)item).Values;
                                            int x = 0;

                                            foreach (var eq in rwItms)
                                            {
                                                if (x == 0) item0 = eq.ToString();
                                                if (x == 1) item1 = eq.ToString();
                                                if (x == 2) item2 = eq.ToString();
                                                if (x == 3) item3 = eq.ToString();
                                                if (x == 4) item4 = eq.ToString();
                                                if (x == 5) item5 = eq.ToString();
                                                x++;
                                            }

                                            genList gLst = new genList
                                            {
                                                __Item0 = item0,
                                                __Item1 = item1,
                                                __Item2 = item2,
                                                __Item3 = item3,
                                                __Item4 = item4,
                                                __Item5 = item5
                                            };

                                            RowItems.Add(gLst);
                                        }
                                    }
                                    continue;
                                }
                                catch
                                {
                                }
                            }

                        if (kvp.Key.ToLower() == "creditrequirements")
                        {
                            try
                            {
                                List<object> itemList = (List<object>)kvp.Value;
                                int x = 1;

                                foreach (var item in itemList)
                                {
                                    if (!string.IsNullOrEmpty(item.ToString()))
                                    {
                                        var creditReqs = ((IDictionary<string, object>)item).Values;
                                        foreach (var cred in creditReqs)
                                        {
                                            credReq creditReq = new credReq
                                            {
                                                Cr = x.ToString(),
                                                CreditRequirement = cred.ToString()
                                            };
                                            x++;

                                            CreditReq.Add(creditReq);
                                        }
                                    }
                                }
                                continue;
                            }
                            catch
                            {
                            }
                        }



                        


                        if (kvp.Key.ToLower() == "vaps")
                        {
                            try
                            {
                                List<object> itemList = (List<object>)kvp.Value;
                                foreach (var item in itemList)
                                {
                                    var VapC = "";
                                    var VapS = "";
                                    var VapsAmt = "";
                                    var item3 = "";
                                    var item4 = "";
                                    var item5 = "";


                                    if (!string.IsNullOrEmpty(item.ToString()))
                                    {
                                        var vapItms = ((IDictionary<string, object>)item).Values;
                                        int x = 0;

                                        foreach (var vp in vapItms)
                                        {
                                            if (x == 0) VapC = vp.ToString();
                                            if (x == 1) VapS = vp.ToString();
                                            if (x == 2) VapsAmt = vp.ToString();
                                            x++;
                                        }

                                        ContractVaps vaps = new ContractVaps
                                        {
                                            VapC = VapC,
                                            VapS = VapS,
                                            VapsAmt = VapsAmt
                                        };

                                        Vaps.Add(vaps);
                                    }
                                }
                                continue;
                            }
                            catch
                            {
                            }
                        }

                    }
                    catch
                    {
                        if (kvp.Key.ToLower() != "details" && kvp.Key.ToLower() != "page1" && kvp.Key.ToLower() != "page2" && kvp.Key.ToLower() != "page7" && kvp.Key.ToLower() != "page9"
                            && kvp.Key.ToLower() != "contractpack" && kvp.Key.ToLower() != "incomeandexpenses")
                        {
                            wordDocument.Replace(kvp.Key.ToLower(), kvp.Value == null ? "" : kvp.Value.ToString(), false, true);
                        }
                        //wordDocument.Replace(kvp.Key.ToLower(), kvp.Value.ToString(), false, true);

                        if (kvp.Key.ToLower() == "mailreplace")
                        {
                            try
                            {
                                dynamic itemList = (dynamic)kvp.Value;

                                foreach (KeyValuePair<string, object> itm in ((IDictionary<string, object>)itemList))
                                {
                                    MailReplace.Add(itm.Key.ToString() + "~" + itm.Value.ToString());
                                }
                            }
                            catch
                            {
                            }
                        }

                        if (kvp.Key.ToLower() == "details")
                        {
                            try
                            {
                                dynamic itemList = (dynamic)kvp.Value;

                                foreach (KeyValuePair<string, object> itm in ((IDictionary<string, object>)itemList))
                                {
                                    wordDocument.Replace(itm.Key.ToLower(), itm.Value == null ? "" : itm.Value.ToString(), false, true);
                                    title = itm.Key.ToLower() == "__Title" ? itm.Value == null ? "" : itm.Value.ToString() : "";
                                    name = itm.Key.ToLower() == "__Name" ? itm.Value == null ? "" : itm.Value.ToString() : "";
                                    surname = itm.Key.ToLower() == "__SurName" ? itm.Value == null ? "" : itm.Value.ToString() : "";

                                    if (itm.Key.ToLower() == "itemheadings")
                                    {
                                        try
                                        {
                                            List<object> itemListHead = (List<object>)itm.Value;
                                            foreach (var item in itemListHead)
                                            {
                                                if (!string.IsNullOrEmpty(item.ToString()))
                                                {
                                                    ItemHeadings.Add(item.ToString());
                                                }
                                            }
                                            continue;
                                        }
                                        catch
                                        {
                                        }
                                    }

                                    if (itm.Key.ToLower() == "rowitems")
                                    {
                                        try
                                        {
                                            List<object> itemListLocal = (List<object>)itm.Value;
                                            foreach (var item in itemListLocal)
                                            {
                                                var item0 = "";
                                                var item1 = "";
                                                var item2 = "";
                                                var item3 = "";
                                                var item4 = "";
                                                var item5 = "";


                                                if (!string.IsNullOrEmpty(item.ToString()))
                                                {
                                                    var rwItms = ((IDictionary<string, object>)item).Values;
                                                    int x = 0;

                                                    foreach (var eq in rwItms)
                                                    {
                                                        if (x == 0) item0 = eq.ToString();
                                                        if (x == 1) item1 = eq.ToString();
                                                        if (x == 2) item2 = eq.ToString();
                                                        if (x == 3) item3 = eq.ToString();
                                                        if (x == 4) item4 = eq.ToString();
                                                        if (x == 5) item5 = eq.ToString();
                                                        x++;
                                                    }

                                                    genList gLst = new genList
                                                    {
                                                        __Item0 = item0,
                                                        __Item1 = item1,
                                                        __Item2 = item2,
                                                        __Item3 = item3,
                                                        __Item4 = item4,
                                                        __Item5 = item5
                                                    };

                                                    RowItems.Add(gLst);
                                                }
                                            }
                                            continue;
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }

                        if (kvp.Key.ToLower() == "page1" || kvp.Key.ToLower() == "page2" || kvp.Key.ToLower() == "page7" || kvp.Key.ToLower() == "page9"
                            || kvp.Key.ToLower() == "contractpack" || kvp.Key.ToLower() == "incomeandexpenses")
                        {
                            try
                            {
                                dynamic itemList = (dynamic)kvp.Value;

                                foreach (KeyValuePair<string, object> itm in ((IDictionary<string, object>)itemList))
                                {
                                    if (itm.Key.ToLower() == "clientstreetaddress")
                                    {
                                        string streetAddress = itm.Value == null ? "" : itm.Value.ToString();
                                        streetAddress = streetAddress.Replace("\r\n", " ");
                                        wordDocument.Replace(itm.Key, streetAddress, false, true);
                                    }

                                    wordDocument.Replace(itm.Key.ToLower(), itm.Value == null ? "" : itm.Value.ToString() , false, true);
                                    if (itm.Key.ToLower() == "pccursymb")
                                    {
                                        wordDocument.Replace("_qr", itm.Value == null ? "" : itm.Value.ToString(), false, true);
                                    }
                                }
                            }
                            catch
                            {
                            }
                            
                            
                        }
                    }
                }
                DateTime dt = DateTime.Now;

                wordDocument.Replace("__date", dt.ToString("dd MMMM yyyy", DateTimeFormatInfo.InvariantInfo), false, true);
                //this.logger.LogInformation("(Clog) replacing short date 3");
                wordDocument.Replace("__ShortDate", dt.ToString("dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo), false, true);
                wordDocument.Replace("__datetime", dt.ToString("dd MMMM yyyy", DateTimeFormatInfo.InvariantInfo) + " " + dt.ToString("t", DateTimeFormatInfo.InvariantInfo), false, true);
                wordDocument.Replace("dateandtime", dt.ToString("dd MMMM yyyy", DateTimeFormatInfo.InvariantInfo) + " " + dt.ToString("t", DateTimeFormatInfo.InvariantInfo), false, true);
                wordDocument.Replace("__weekday", dt.ToString("dddd", DateTimeFormatInfo.InvariantInfo), false, true);
                wordDocument.Replace("__month", dt.ToString("MMMM", DateTimeFormatInfo.InvariantInfo), false, true);
                wordDocument.Replace("__time", dt.ToString("t", DateTimeFormatInfo.InvariantInfo), false, true);

                if (ItemHeadings.Count > 0 && RowItems.Count > 0)
                {
                    DataSet ds = GetData(ItemHeadings, RowItems);
                    wordDocument.MailMerge.ExecuteGroup(ds.Tables["LS"]);
                }

                if (RowItems?.Count > 0)
                {
                    int rowCount = 1;
                    foreach (genList item in RowItems)
                    {
                        wordDocument.Replace("__row" + rowCount + "Item0", item.__Item0 == null ? "" : item.__Item0, false, true);
                        wordDocument.Replace("__row" + rowCount + "Item1", item.__Item1 == null ? "" : item.__Item1, false, true);
                        wordDocument.Replace("__row" + rowCount + "Item2", item.__Item2 == null ? "" : item.__Item2, false, true);
                        wordDocument.Replace("__row" + rowCount + "Item3", item.__Item3 == null ? "" : item.__Item3, false, true);
                        wordDocument.Replace("__row" + rowCount + "Item4", item.__Item4 == null ? "" : item.__Item4, false, true);

                        rowCount++;
                    }

                    while (rowCount < 20)
                    {
                        wordDocument.Replace("__row" + rowCount + "Item0", "", false, true);
                        wordDocument.Replace("__row" + rowCount + "Item1", "", false, true);
                        wordDocument.Replace("__row" + rowCount + "Item2", "", false, true);
                        wordDocument.Replace("__row" + rowCount + "Item3", "", false, true);
                        wordDocument.Replace("__row" + rowCount + "Item4", "", false, true);
                        rowCount++;
                    };
                }

                if (Notes?.Count > 0)
                {
                    int noteCount = 1;

                    foreach(string note in Notes)
                    {
                        wordDocument.Replace("note" + noteCount, note == null ? "" : note, false, true);
                        noteCount++;
                    }

                    while (noteCount < 8)
                    {
                        wordDocument.Replace("note" + noteCount, "", false, true);
                        noteCount++;
                    }
                } else
                {
                    int noteCount = 1;

                    while (noteCount < 8)
                    {
                        wordDocument.Replace("note" + noteCount, "", false, true);
                        noteCount++;
                    }
                }

                if (CreditReq?.Count > 0)
                {
                    int credCount = 1;

                    foreach (credReq cred in CreditReq)
                    {
                        wordDocument.Replace("cr" + credCount, cred.Cr + ".", false, true);
                        wordDocument.Replace("creditrequirement" + credCount, cred.CreditRequirement, false, true);
                        credCount++;
                    }

                    while (credCount <= 8)
                    {
                        wordDocument.Replace("cr" + credCount, "", false, true);
                        wordDocument.Replace("creditrequirement" + credCount, "", false, true);
                        credCount++;
                    }
                }
                else
                {
                    int credCount = 1;

                    while (credCount <= 8)
                    {
                        wordDocument.Replace("cr" + credCount, "", false, true);
                        wordDocument.Replace("creditrequirement" + credCount, "", false, true);
                        credCount++;
                    }
                }



                if (Vaps?.Count > 0)
                {
                    int vapCount = 1;

                    foreach (ContractVaps vap in Vaps)
                    {
                        wordDocument.Replace("vaps" + vapCount, vap.VapS == null ? "" : vap.VapC, false, true);
                        wordDocument.Replace("vc" + vapCount, vap.VapC == null ? "" : vap.VapS, false, true);
                        wordDocument.Replace("vaps" + vapCount + "amt", vap.VapsAmt == null ? "" : vap.VapsAmt, false, true);
                        vapCount++;
                    }

                    while (vapCount <= 8)
                    {
                        wordDocument.Replace("vaps" + vapCount, "", false, true);
                        wordDocument.Replace("vc" + vapCount, "", false, true);
                        wordDocument.Replace("vaps" + vapCount + "amt", "", false, true);
                        vapCount++;
                    }
                } else
                {
                    int vapCount = 1;

                    while (vapCount <= 8)
                    {
                        wordDocument.Replace("vaps" + vapCount, "", false, true);
                        wordDocument.Replace("vc" + vapCount, "", false, true);
                        wordDocument.Replace("vaps" + vapCount + "amt", "", false, true);
                        vapCount++;
                    }
                }

                


                string fileName = "Gen_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".pdf";

                using (DocIORenderer renderer = new DocIORenderer())
                {
                    renderer.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
                    renderer.Settings.EmbedCompleteFonts = true;

                    using (PdfDocument pdfDocument = renderer.ConvertToPDF(wordDocument))
                    {
                        if (File.Exists($"{this.resourceSettings.TempFolder}\\{attachmentFileName}"))
                        {
                            DateTime dte = DateTime.Now;
                            var seconds = dte.ToString("ss", DateTimeFormatInfo.InvariantInfo);
                            attachmentFileName = attachmentFileName.Substring(0, attachmentFileName.IndexOf(".")) + "-" + seconds + ".pdf";
                        }
                            


                        string pdfFilePath = attachmentFileName == "" ? $"{this.resourceSettings.TempFolder}\\{fileName}" : $"{this.resourceSettings.TempFolder}\\{attachmentFileName}";

                        using (FileStream outputStream = new FileStream(Path.GetFullPath(pdfFilePath), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            try
                            {
                                if (passwordProtect == true)
                                {
                                    PdfSecurity security = pdfDocument.Security;
                                    security.KeySize = PdfEncryptionKeySize.Key256BitRevision6;
                                    security.Algorithm = PdfEncryptionAlgorithm.AES;
                                    security.UserPassword = password;
                                }
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    if (passwordProtect == true)
                                    {
                                        if (string.IsNullOrEmpty(password))
                                        {
                                            this.logger.LogError($"(CLog - ) Couldn't password protect document. A password is required");
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }

                            pdfDocument.Save(outputStream);
                            pdfDocument.Close();

                            var memoryStream = new MemoryStream();

                            outputStream.Position = 0;
                            outputStream.CopyTo(memoryStream);

                            byte[] pdfBytes = memoryStream.ToArray();

                            this.logger.LogInformation($"(CLog) Done creating generic pdf");

                            var response = new DocumentResponseDto
                            {
                                documentVersion = 0,
                                pdfDocument = pdfBytes
                            };

                            if (sendmail == true)
                            {

                                Task.Run(() =>
                                {
                                    this.logger.LogInformation($"sending email for {pdfTemplate}");
                                    if (SendTo.Count == 0) throw new ArgumentException("An email address is required");
                                    try
                                    {
                                        Thread.Sleep(7000);
                                        SendMail1(SendTo, pdfFilePath, true, "", mailSubject, htmlTemplateName, MailReplace, mailfrom);
                                    }
                                    catch (Exception ex)
                                    {
                                        this.logger.LogInformation($"There was a problem sendint the email {ex}");
                                        throw ex;
                                    }
                                }).ConfigureAwait(false);
                            }

                            return response;
                        }
                    }
                }
            }
        }

        public async Task SendMail1(List<string> sendTo, string filePath, bool isHtml = false, string mailBody = "", string mailSubject = "", string mailTemplateName = "", List<string> MailReplace = null, string fromMail = "")
        {
            this.logger.LogInformation($"sendTo {sendTo} filePath {filePath} mailBody {mailBody} mailSubject {mailSubject} mailTemplateName {mailTemplateName}");
            const string fromPassword = "g8KH~.}][&5zq(G;";

            string fromEmail = string.IsNullOrEmpty(fromMail) ? "info_gomo@gomo.co.za" : fromMail;

            var fromAddress = new MailAddress(fromEmail, "Information");

            this.logger.LogInformation($"Starting SmtpClient");
            var smtp = new SmtpClient
            {
                //Host = "smtp.sataxi.co.za",
                Host = "relay.sataxifin.local",
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                //Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 55000,
                UseDefaultCredentials = false
            };

            this.logger.LogInformation($"Starting Message");
            MailMessage message = new MailMessage(
               fromAddress.Address,
               fromEmail,
               //"info_gomo@gomo.co.za",
               mailSubject,
               "")
            {
                IsBodyHtml = true,
                Body = mailBody
            };

            if (isHtml)
            {
                this.logger.LogInformation($"(CLog) Sending an html email");
                var sharedfolder = this.resourceSettings.SharedFolder;

                var htmlTemplate = string.IsNullOrEmpty(mailTemplateName) ? $"{sharedfolder}\\Templates\\MailTemplates\\new.html" : $"{sharedfolder}\\Templates\\MailTemplates\\" + mailTemplateName + ".html";
                this.logger.LogInformation($"(CLog) using Template {htmlTemplate}");
                string mailHtmlBodyString  = string.IsNullOrEmpty(mailBody) ? (string.IsNullOrEmpty(htmlTemplate) ? System.IO.File.ReadAllText($"{sharedfolder}\\Templates\\MailTemplates\\new.html") : System.IO.File.ReadAllText(htmlTemplate)) : mailBody;

                this.logger.LogInformation($"(CLog) Sending 1");
                if (MailReplace.Count > 0)
                {
                    foreach (string item in MailReplace)
                    {
                        string strToReplace = "";
                        string strReplaceBy = "";
                        int startIndex = item.IndexOf("~") + 1;
                        int endIndex = item.Length - startIndex;
                        if (item.Contains("~"))
                        {
                            strToReplace = item.Substring(0, item.IndexOf("~"));
                            strReplaceBy = item.Substring(startIndex, endIndex);
                            mailHtmlBodyString = mailHtmlBodyString.Replace(strToReplace, strReplaceBy);
                        }
                    }
                    this.logger.LogInformation($"(CLog) Sending 2");
                }

                message.Body = mailHtmlBodyString;
                this.logger.LogInformation($"(CLog) Sending 3s");
                message.IsBodyHtml = true;
            }
            else
            {
                message.IsBodyHtml = false;
            }

            try
            {
                Attachment attached = new Attachment(filePath,
                MediaTypeNames.Application.Octet);
                message.Attachments.Add(attached);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Something went wrong in sending email {ex}");
            }

            try
            {
                
                foreach (string mailAddress in sendTo)
                {
                    message.To.Clear();
                    message.To.Add(mailAddress);
                    this.logger.LogInformation($"Sending Message");
                    smtp.Send(message);
                    this.logger.LogInformation($"Message sent");
                };

                
                Console.WriteLine("End!");
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Problem sending message {ex}");
                throw ex;
            }
        }


        public async Task<byte[]> GetTemplateByTemplateName(string templateName)
        {
            var cacheKey = "templateCache";
            TemplateDetails cachedTemplate = new TemplateDetails();
            string TemplatePath;

            if (!_templateCache.TryGetValue(cacheKey, out List<TemplateDetails> templateList))
            {
                try
                {
                    this.logger.LogInformation($"(CLog) Template Cache is empty");

                    TemplatePath = $"{this.resourceSettings.SharedFolder}\\Templates\\{templateName}.docx";

                    Byte[] bytes = File.ReadAllBytes(TemplatePath);

                    cachedTemplate.TemplateBytes = bytes;
                    cachedTemplate.FileName = templateName;

                    this.logger.LogInformation($"(CLog) added base64 for {templateName}.docx to templatecache");

                    var cacheExpiryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddDays(1),
                        Priority = CacheItemPriority.High,
                        SlidingExpiration = TimeSpan.FromDays(1)
                    };

                    templateList = new List<TemplateDetails>
                {
                    cachedTemplate
                };

                    _templateCache.Set(cacheKey, templateList, cacheExpiryOptions);
                }
                catch (Exception ex)
                {
                    this.logger.LogInformation($"(CLog) - Something went wrong during initializing the Cache - {ex}");
                    throw ex;
                }
            }
            else
            {
                try
                {
                    cachedTemplate = templateList.FirstOrDefault(r => r.FileName == templateName);

                    if (cachedTemplate == null)
                    {
                        cachedTemplate = new TemplateDetails();

                        this.logger.LogInformation($"(CLog) Template for ({templateName}) was not found in template cache");

                        TemplatePath = $"{this.resourceSettings.SharedFolder}\\Templates\\{templateName}.docx";

                        if (!File.Exists(TemplatePath))
                        {
                            this.logger.LogInformation($"(CLog) No template exist for ({templateName}) at {TemplatePath}");
                            throw new Exception($"No template exist for ({templateName}) at {TemplatePath}");
                        }

                        Byte[] bytes = File.ReadAllBytes(TemplatePath);

                        cachedTemplate.TemplateBytes = bytes;

                        cachedTemplate.FileName = templateName;

                        var cacheExpiryOptions = new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddDays(1),
                            Priority = CacheItemPriority.High,
                            SlidingExpiration = TimeSpan.FromDays(1)
                        };

                        templateList.Add(cachedTemplate);

                        _templateCache.Set(cacheKey, templateList, cacheExpiryOptions);
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogInformation($"(CLog) - Something went wrong while obtaining template from Cache - {ex}");
                    throw ex;
                }
                
                this.logger.LogInformation($"(CLog) Template ({templateName}) was found in cache");
            }

            return cachedTemplate.TemplateBytes;   
        }

        private static void AlternateRows_MergeField(object sender, MergeFieldEventArgs args)
        {
            if (args.RowIndex % 2 != 0)
            {
                args.CharacterFormat.TextColor = Color.FromArgb(196, 89, 17);
            }
        }

    }

    public class BlazeSubmissionMessage
    {
        public string? data { get; set; }
        public string? token { get; set; }
    }
}


