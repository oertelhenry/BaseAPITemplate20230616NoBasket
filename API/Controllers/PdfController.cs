using Mobalyz.Domain.Odyssey.Models;
using Mobalyz.Odyssey.Resources.Provider;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.Text;
using Newtonsoft.Json;
using System.Dynamic;
using AutoMapper;
using Core.Injection.Models;

namespace Mobalyz.Odyssey.Service
{
    /// <summary>
    /// Used to Upload documents to Mobalyz
    /// </summary>
    [ApiController, EnableCors, Route("vehicleassetfinance/odyssey/v5/api"), Produces("application/json"), Consumes("application/json"), ApiVersion("1")]

    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _logger;
        private readonly IPdfCreationProvider pdfProvider;
        private readonly IMapper _mapper;

        public PdfController(ILogger<PdfController> logger,
            IPdfCreationProvider pdfProvider,
            IMapper mapper)
        {
            _logger = logger;
            this.pdfProvider = pdfProvider;
            _mapper = mapper;
        }


        [HttpGet, Route("rescore")]
        public async Task<ApiResponse<bool>> DoIt()
        {
            using (LogContextExtensions.AddKey(0000))
            {
                DateTime timeStart = DateTime.UtcNow;

                var result = await this.pdfProvider.rescoreBlazeRequstXml();

                DateTime timeEnd = DateTime.UtcNow;
                var resultTime = timeEnd - timeStart;

                var response = new ApiResponse<bool>(true);
                response.MetaData.Detail = (resultTime.ToString() + " " + timeStart.ToString() + " - " + timeEnd.ToString());

                return response;
            }
        }

        [HttpPost, Route("contract")]
        public async Task<ApiResponse<DocumentResponseDto>> GenerateContractAsync(ContractRequestDto request)
        {
            using (LogContextExtensions.AddKey(request.ContractNumber.ToString()))
            {
                DateTime timeStart = DateTime.UtcNow;

                var result = await this.pdfProvider.GenerateContract(request);
                
                DateTime timeEnd = DateTime.UtcNow;
                var resultTime = timeEnd - timeStart;

                var response = new ApiResponse<DocumentResponseDto>(result);
                response.MetaData.Detail = (resultTime.ToString() + " " + timeStart.ToString() + " - " + timeEnd.ToString());

                return response;
            }
        }

        [HttpPost, Route("podium")]
        public async Task<ApiResponse<DocumentResponseDto>> GeneratePodiumAsync(PodiumRequestDto request)
        {
            using (LogContextExtensions.AddKey(0000))
            {
                DateTime timeStart = DateTime.UtcNow;

                var result = await this.pdfProvider.GeneratePodium(request);

                DateTime timeEnd = DateTime.UtcNow;
                var resultTime = timeEnd - timeStart;

                var response = new ApiResponse<DocumentResponseDto>(result);
                response.MetaData.Detail = (resultTime.ToString() + " " + timeStart.ToString() + " - " + timeEnd.ToString());

                return response;
            }
        }

        [HttpPost, Route("dicinvoice")]
        public async Task<ApiResponse<DocumentResponseDto>> GenerateDicInvoiceAsync(DicInvoiceRequest request)
        {
            using (LogContextExtensions.AddKey(0000))
            {
                DateTime timeStart = DateTime.UtcNow;

                var result = await this.pdfProvider.GenerateDicInvoice(request);

                DateTime timeEnd = DateTime.UtcNow;
                var resultTime = timeEnd - timeStart;

                var response = new ApiResponse<DocumentResponseDto>(result);
                response.MetaData.Detail = (resultTime.ToString() + " " + timeStart.ToString() + " - " + timeEnd.ToString());

                return response;
            }
        }

        [HttpPost, Route("insuranceproposal")]
        public async Task<ApiResponse<DocumentResponseDto>> GenerateInsuranceDocAsync(InsuranceRequestDto request)
        {
            using (LogContextExtensions.AddKey(0000))
            {
                DateTime timeStart = DateTime.UtcNow;

                var result = await this.pdfProvider.GenerateInsuranceProposal(request);

                DateTime timeEnd = DateTime.UtcNow;
                var resultTime = timeEnd - timeStart;

                var response = new ApiResponse<DocumentResponseDto>(result);
                response.MetaData.Detail = (resultTime.ToString() + " " + timeStart.ToString() + " - " + timeEnd.ToString());

                return response;
            }
        }
        
        [HttpPost, Route("insurancepolicy")]
        public async Task<ApiResponse<DocumentResponseDto>> GenerateInsurancePolicyScheduleAsync(InsuranceRequestDto request)
        {
            using (LogContextExtensions.AddKey(0000))
            {
                DateTime timeStart = DateTime.UtcNow;

                var result = await this.pdfProvider.GenerateInsurancePolicy(request);

                DateTime timeEnd = DateTime.UtcNow;
                var resultTime = timeEnd - timeStart;

                var response = new ApiResponse<DocumentResponseDto>(result);
                response.MetaData.Detail = (resultTime.ToString() + " " + timeStart.ToString() + " - " + timeEnd.ToString());

                return response;
            }
        }

        [HttpPost("create-pdf")]
        public async Task<ApiResponse<DocumentResponseDto>> ReadStringDataManual()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();

                dynamic obje = JsonConvert.DeserializeObject<ExpandoObject>(request);

                var result =  await this.pdfProvider.CreatePdf(obje);
                try
                {
                    var response = new ApiResponse<DocumentResponseDto>(result);
                    return response;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

                

                //return response;
            }
        }

        public ExpandoObject CreateDynamicCustomer(string propertyName, string PropertyValue)
        {
            dynamic cust = new ExpandoObject();
            ((IDictionary<string, object>)cust)[propertyName] = PropertyValue;
            return cust;
        }

        public T AnonymousTypeCast<T>(object anonymous, T typeExpression)
        {
            return (T)anonymous;
        }

        public static void Iterate(object variable, string name)
        {
            //if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JObject))
            //{
            //    Console.WriteLine("type is Object");
            //}
            if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JArray))
            {
                Console.WriteLine("type is Array");
            }
            else if (variable.GetType() == typeof(Newtonsoft.Json.Linq.JValue))
            {
                Console.WriteLine("type is Variable");
                Console.WriteLine("value: " + variable.ToString());
            }

            var type = variable.GetType();
            var prop = type.GetProperty(name);
            var jol = prop.GetValue(variable);


        }

        [HttpPost, Route("HtmlToPdfAsync")]
        public async Task<ApiResponse<FileContentResult>> HtmlToPdfAsync(PodiumRequestDto request)
        {
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

            //Convert URL to PDF document
            PdfDocument document = htmlConverter.Convert("https://www.syncfusion.com");

            //Create memory stream
            MemoryStream stream = new MemoryStream();

            //Save the document
            document.Save(stream);

            var result = File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "HTML-to-PDF.pdf");

            var response = new ApiResponse<FileContentResult>(result);

            return response;
        }

        [HttpPost, Route("create-pdf1")]
        public async Task<ApiResponse<GenericResponseDto>> GenerateGenericPdfAsync(GenericRequestDto request)
        {
            using (LogContextExtensions.AddKey(0000))
            {
                DateTime timeStart = DateTime.UtcNow; //test

                var result = await this.pdfProvider.GenerateGenericPdfAsync(request);

                DateTime timeEnd = DateTime.UtcNow;
                var resultTime = timeEnd - timeStart;

                var response = new ApiResponse<GenericResponseDto>(result);
                response.MetaData.Detail = (resultTime.ToString() + " " + timeStart.ToString() + " - " + timeEnd.ToString());

                return response;
            }
        }

        //[HttpPost, Route("test")]
        //public async Task<ApiResponse<GenericResponseDto>> GenerateTempTestPdfAsync(GenericRequestDto request)
        //{
        //    using (LogContextExtensions.AddKey(0000))
        //    {
        //        DateTime timeStart = DateTime.UtcNow;

        //        var result = await this.pdfProvider.GenerateTempTestPdfAsync(request);

        //        DateTime timeEnd = DateTime.UtcNow;
        //        var resultTime = timeEnd - timeStart;

        //        var response = new ApiResponse<GenericResponseDto>(result);
        //        response.MetaData.Detail = (resultTime.ToString() + " " + timeStart.ToString() + " - " + timeEnd.ToString());

        //        return response;
        //    }
        //}

        
    }
}
