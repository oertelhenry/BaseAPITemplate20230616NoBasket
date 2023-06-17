using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Mobalyz.Domain.Odyssey.Models;
using Mobalyz.Odyssey.Resources.Provider;
using Serilog.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mobalyz.Odyssey.Service
{
    [ApiController, EnableCors, Route("vehicleassetfinance/odyssey/v5/template"), Produces("application/json"), Consumes("application/json"),  ApiVersion("1")] //Security.ClientIdAuthorize,
    public class TemplateController : ControllerBase
    {
        private readonly ILogger<PdfController> logger;
        private readonly ITemplateProvider templateProvider;

        public TemplateController(ILogger<TemplateController> logger,
            ITemplateProvider templateProvider)
        {
            logger = logger;
            this.templateProvider = templateProvider;
        }


        [HttpGet, Route("{username}/{templatetype}")]
        public async Task<ActionResult<TemplateDto>> GetTemplatesByUsernameAsync(string username, string templatetype)
        {
            using (LogContextExtensions.AddKey($"username"))
            {
                var pdfTemplates = await this.templateProvider.GetTemplatesByUsernameAsync(username, templatetype);

                //Response.AddPaginationHeader(dealers.CurrentPage, dealers.PageSize, dealers.TotalCount, dealers.TotalPages);

                return Ok(pdfTemplates);
            }
        }

        [HttpPut, Route("createTemplate")]
        public async Task<ActionResult<TemplateDto>> CreateTemplateAsync(TemplateDto template)
        {
            using (LogContextExtensions.AddKey(template.FileName))
            {
                var pdfTemplate = await this.templateProvider.CreateTemplateAsync(template);

                return Ok(pdfTemplate);
            }
        }

        [HttpPost, Route("updateTemplate")]
        public async Task<ActionResult<TemplateDto>> updateTemplate(TemplateDto template)
        {
            using (LogContextExtensions.AddKey(template.FileName))
            {
                var pdfTemplate = await this.templateProvider.UpdateTemplateAsync(template);

                return Ok(pdfTemplate);
            }
        }

        [HttpDelete, Route("{templateid}/{templatetype}")]
        public async Task<ActionResult<TemplateDto>> deleteTemplate(int templateid, string templatetype)
        {
            using (LogContextExtensions.AddKey(templateid))
            {
                var pdfTemplate = await this.templateProvider.DeletePdfTemplateAsync(templateid, templatetype);

                return Ok(pdfTemplate);
            }
        }

        [HttpGet, Route("edit/{templateid}/{categorytype}")]
        public async Task<ActionResult<TemplateDto>> getTemplateById(int templateid, string categorytype)
        {
            using (LogContextExtensions.AddKey(templateid))
            {
                var pdfTemplate = await this.templateProvider.GetTemplateByIdAsync(templateid, categorytype);

                return Ok(pdfTemplate);
            }
        }

        [HttpGet, Route("download/{templateName}/{categorytype}")]
        public async Task<ActionResult<Byte[]>> downloadTemplateAsync(string templateName, string categorytype)
        {
            using (LogContextExtensions.AddKey(categorytype))
            {
                var templateDwn = await this.templateProvider.DownloadTemplateAsync(templateName, categorytype);

                return Ok(templateDwn);
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                //var blobContainerClient = new BlobContainerClient("UseDevelopmentStorage=true", "images");
                //blobContainerClient.CreateIfNotExists();
                //var containerClient = blobContainerClient.GetBlobClient(file.FileName);
                //var blobHttpHeader = new BlobHttpHeaders
                //{
                //    ContentType = file.ContentType
                //};
                //await containerClient.UploadAsync(file.OpenReadStream(), blobHttpHeader);
            }

            return Ok();
        }

        [HttpGet, Route("templateexist/{templatename}")]
        public async Task<ActionResult<bool>> VerifyTemplateExistAsync(string templatename)
        {
            using (LogContextExtensions.AddKey($"templatename"))
            {
                var templateExist = await this.templateProvider.VerifyTemplateExistAsync(templatename.ToLower());

                return Ok(templateExist);
            }
        }


        //[HttpPost, Route("Upload")]
        //public async Task<IActionResult> Upload([FromForm] PdfTemplateDto template)
        //{
        //    var formCollection = await Request.ReadFormAsync();
        //    var files = formCollection.Files;
        //    foreach (var file in files)
        //    {
        //        //var blobContainerClient = new BlobContainerClient("UseDevelopmentStorage=true", "images");
        //        //blobContainerClient.CreateIfNotExists();
        //        //var containerClient = blobContainerClient.GetBlobClient(file.FileName);
        //        //var blobHttpHeader = new BlobHttpHeaders
        //        //{
        //        //    ContentType = file.ContentType
        //        //};
        //        //await containerClient.UploadAsync(file.OpenReadStream(), blobHttpHeader);
        //    }

        //    return Ok();
        //}


    }
}
