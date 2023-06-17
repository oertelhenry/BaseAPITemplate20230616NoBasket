using Mobalyz.Domain.Odyssey.Models;

namespace Mobalyz.Odyssey.Resources.Provider
{
    public interface ITemplateProvider
    {
        Task<List<TemplateDto>> GetTemplatesByUsernameAsync(string userName, string templatetype);
        Task<TemplateDto> UpdateTemplateAsync(TemplateDto pdfTemplateDto);
        Task<TemplateDto> CreateTemplateAsync(TemplateDto pdfTemplateDto);
        Task<TemplateDto> DeletePdfTemplateAsync(int templateId, string templatetype);
        Task<TemplateDto> GetTemplateByIdAsync(int getTemplateById, string categorytype);
        Task<bool> VerifyTemplateExistAsync(string templateName);
        Task<Byte[]> DownloadTemplateAsync(string templateName, string categorytype);

    }
}
