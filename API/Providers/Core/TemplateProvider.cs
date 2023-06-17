using AutoMapper;
using Core.Domain;
using Domain.Odyssey.Entities.Documents;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Mobalyz.Data;
using Mobalyz.Domain.Odyssey.Models;
//using Syncfusion.Pdf.Graphics;

namespace Mobalyz.Odyssey.Resources.Provider
{
    public class TemplateProvider : ITemplateProvider
    {
        private readonly IDataRepository repository;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly ResourceSettings resourceSettings;
        private readonly IMemoryCache _templateCache;
        public TemplateProvider(ILogger<TemplateProvider> logger, 
            IDataRepository repository,
            IMapper mapper,
            IOptionsMonitor<ResourceSettings> resourceOptionsMonitor,
            IMemoryCache templateCache)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _templateCache = templateCache;
            this.resourceSettings = resourceOptionsMonitor?.CurrentValue ?? throw new ArgumentNullException(nameof(resourceOptionsMonitor));
        }

        public async Task<List<TemplateDto>> GetTemplatesByUsernameAsync(string userName, string templatetype)
        {
            if (templatetype.ToLower() == "pdftemplate")
            {
                this.logger.LogInformation($"(CLog) Getting templates for {userName}");

                var templatesList = await this.repository.GetPdfTemplateList(userName);
                var templatesDto = this.mapper.Map<List<TemplateDto>>(templatesList);
                return templatesDto;
            }

            if (templatetype.ToLower() == "mailtemplate")
            {
                this.logger.LogInformation($"(CLog) Getting templates for {userName}");

                var templatesList = await this.repository.GetMailTemplateList(userName);
                var templatesDto = this.mapper.Map<List<TemplateDto>>(templatesList);
                return templatesDto;
            }

            List<TemplateDto> templateDto = new List<TemplateDto>();
            return templateDto;
        }
        

        public async Task<bool> VerifyTemplateExistAsync(string templateName)
        {
            this.logger.LogInformation($"(CLog) Checking for template - {templateName}");

            var templateExist = await this.repository.VerifyTemplateExist(templateName);

            return templateExist;
        }

        public async Task<TemplateDto> UpdateTemplateAsync(TemplateDto pdfTemplateDto)
        {
            this.logger.LogInformation($"(CLog) Editing template with Category {pdfTemplateDto.fileCategory} ID {pdfTemplateDto.Id}");

            string oldTemplateName = "";
            TemplateDto templateDto = new TemplateDto();
            string origFilePath = "";
            string newFilePath = "";
            string filePath = "";


            if (pdfTemplateDto.fileCategory == "pdftemplate")
            {
                var template = await this.repository.GetPdfTemplateById(pdfTemplateDto.Id);

                oldTemplateName = template.TemplateName;

                template.Description = pdfTemplateDto.Description;
                template.FileName = pdfTemplateDto.FileName;
                template.TemplateName = pdfTemplateDto.templateName.ToLower();
                template.FileSize = pdfTemplateDto.FileSize.ToString();
                template.FileType = pdfTemplateDto.FileType;
                template.LastModifiedDate = pdfTemplateDto.lastModifiedDate;
                template.LastModifiedTime = pdfTemplateDto.lastModifiedTime;
                template.ModifiedDate = DateTime.Now;

                await this.repository.SaveAsync();

                templateDto = this.mapper.Map<TemplateDto>(template);

                origFilePath = $"{this.resourceSettings.SharedFolder}\\Templates\\" + oldTemplateName + ".docx";
                newFilePath = $"{this.resourceSettings.SharedFolder}\\Templates\\Backup\\" + oldTemplateName + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".docx";
                filePath = $"{this.resourceSettings.SharedFolder}\\Templates\\" + pdfTemplateDto.templateName + ".docx";
            }

            if (pdfTemplateDto.fileCategory == "htmlmailtemplate")
            {
                var template = await this.repository.GetMailTemplateById(pdfTemplateDto.Id);

                oldTemplateName = template.TemplateName;

                template.Description = pdfTemplateDto.Description;
                template.FileName = pdfTemplateDto.FileName;
                template.TemplateName = pdfTemplateDto.templateName.ToLower();
                template.FileSize = pdfTemplateDto.FileSize.ToString();
                template.FileType = pdfTemplateDto.FileType;
                template.LastModifiedDate = pdfTemplateDto.lastModifiedDate;
                template.LastModifiedTime = pdfTemplateDto.lastModifiedTime;
                template.ModifiedDate = DateTime.Now;

                await this.repository.SaveAsync();

                templateDto = this.mapper.Map<TemplateDto>(template);

                origFilePath = $"{this.resourceSettings.SharedFolder}\\Templates\\MailTemplates\\" + oldTemplateName + ".html";
                newFilePath = $"{this.resourceSettings.SharedFolder}\\Templates\\Backup\\" + oldTemplateName + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".html";
                filePath = $"{this.resourceSettings.SharedFolder}\\Templates\\" + pdfTemplateDto.templateName + ".html";
            }


            try
            {
                System.IO.File.Move(origFilePath, newFilePath);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
            File.WriteAllBytes(filePath, Convert.FromBase64String(pdfTemplateDto.FileAsBase64.Substring(pdfTemplateDto.FileAsBase64.IndexOf("base64,") + 7)));

            CacheHelper.Clear(_templateCache);


            return templateDto;
        }

        public async Task<TemplateDto> CreateTemplateAsync(TemplateDto templateDto)
        {
            this.logger.LogInformation($"(CLog) Creating template with for {templateDto.Description} Category {templateDto.fileCategory}");
            templateDto.templateName = templateDto.templateName.ToLower();

            PdfTemplate entPdfTemplate = new PdfTemplate();
            HtmlMailTemplate htmlMailTemplate = new HtmlMailTemplate();

            templateDto.CreatedDate = DateTime.Now;
            templateDto.CreatedDate = DateTime.Now;

            if (templateDto.fileCategory.ToLower() == "pdftemplate")
            {
                entPdfTemplate = this.mapper.Map<PdfTemplate>(templateDto);

                this.repository.Create(entPdfTemplate);
                await this.repository.SaveAsync();
            }

            if (templateDto.fileCategory.ToLower() == "htmlmailtemplate")
            {
                htmlMailTemplate = this.mapper.Map<HtmlMailTemplate>(templateDto);

                this.repository.Create(htmlMailTemplate);
                await this.repository.SaveAsync();
            }

            string filePath = "";

            try
            {
                if (templateDto.fileCategory.ToLower() == "pdftemplate")
                {
                    filePath = $"{this.resourceSettings.SharedFolder}\\Templates\\" + templateDto.templateName + ".docx";
                    this.logger.LogInformation($"(CLog) 1111111111111");
                }
                if (templateDto.fileCategory.ToLower() == "htmlmailtemplate")
                {
                    filePath = $"{this.resourceSettings.SharedFolder}\\Templates\\MailTemplates\\{templateDto.templateName}.html";
                    this.logger.LogInformation($"(CLog) 2222222222222");
                }

                this.logger.LogInformation($"(CLog) writing template to {filePath}");
                
                File.WriteAllBytes(filePath, Convert.FromBase64String(templateDto.FileAsBase64.Substring(templateDto.FileAsBase64.IndexOf("base64,") + 7)));
            }
            catch (Exception ex )
            {
                string path = $"{this.resourceSettings.SharedFolder}\\Templates\\MailTemplates\\{templateDto.templateName}.html";
                logger.LogError($"Something went wrong during template upload {ex} path {path}");
                throw ex;
            }

            if (templateDto.fileCategory.ToLower() == "pdftemplate")
            {
                return this.mapper.Map<TemplateDto>(entPdfTemplate);
            }

            if (templateDto.fileCategory.ToLower() == "htmlmailtemplate")
            {
                return this.mapper.Map<TemplateDto>(htmlMailTemplate);
            }

            return null;
        }

        public async Task<TemplateDto> DeletePdfTemplateAsync(int templateId, string templatetype)
        {
            string origFilePath = "";
            string newFilePath = "";
            TemplateDto data = new TemplateDto();

            if (templatetype.ToLower() == "pdftemplate")
            {
                var template = await this.repository.GetFirstAsync<PdfTemplate>(r => r.Id == templateId);

                this.repository.Delete(template);
                await this.repository.SaveAsync();

                origFilePath = $"{this.resourceSettings.SharedFolder}\\Templates\\" + template.TemplateName + ".docx";
                newFilePath = $"{this.resourceSettings.SharedFolder}\\Templates\\Backup\\" + template.TemplateName + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".docx";

                data = this.mapper.Map<TemplateDto>(template);
            }

            if (templatetype.ToLower() == "htmlmailtemplate")
            {
                var template = await this.repository.GetFirstAsync<HtmlMailTemplate>(r => r.Id == templateId);

                this.repository.Delete(template);
                await this.repository.SaveAsync();

                origFilePath = $"{this.resourceSettings.SharedFolder}\\Templates\\MailTemplates\\" + template.TemplateName + ".html";
                newFilePath = $"{this.resourceSettings.SharedFolder}\\Templates\\Backup\\" + template.TemplateName + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".html";

                data = this.mapper.Map<TemplateDto>(template);
            }

            System.IO.File.Move(origFilePath, newFilePath);

            CacheHelper.Clear(_templateCache);

            return data;
        }
        
        public async Task<TemplateDto> GetTemplateByIdAsync(int templateId, string categorytype)
        {
            try
            {
                if (categorytype.ToLower() == "pdftemplate")
                {
                    var template = await this.repository.GetFirstAsync<PdfTemplate>(r => r.Id == templateId);

                    var data = this.mapper.Map<TemplateDto>(template);

                    return data;
                }
                if (categorytype.ToLower() == "htmlmailtemplate")
                {
                    var template = await this.repository.GetFirstAsync<HtmlMailTemplate>(r => r.Id == templateId);

                    var data = this.mapper.Map<TemplateDto>(template);

                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Byte[]> DownloadTemplateAsync(string templateName, string categorytype)
        {
            this.logger.LogInformation($"(CLog) downloading template for {templateName} - {categorytype}");
            string TemplatePath = "";
            Byte[] bytes = null;

            if (categorytype.ToLower() == "pdftemplate")
            {
                TemplatePath = $"{this.resourceSettings.SharedFolder}\\Templates\\{templateName}.docx";

                bytes = File.ReadAllBytes(TemplatePath);

                return bytes;
            }

            if (categorytype.ToLower() == "htmlmailtemplate")
            {
                TemplatePath = $"{this.resourceSettings.SharedFolder}\\Templates\\MailTemplates\\{templateName}.html";

                bytes = File.ReadAllBytes(TemplatePath);
            }

            return bytes;
        }

    }
}
