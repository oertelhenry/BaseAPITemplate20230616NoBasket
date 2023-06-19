using AutoMapper;
using Domain.Odyssey.Entities.Documents;
using Mobalyz.Domain.Odyssey.Entities;
using Mobalyz.Domain.Odyssey.Models;

namespace Odyssey.Resources.Data.Mapping
{
    public class DocumentationMappingProfile : Profile
    {
        public DocumentationMappingProfile()
        {
            CreateMap<PdfTemplate, TemplateDto>().ReverseMap();
            CreateMap<HtmlMailTemplate, TemplateDto>().ReverseMap();
        }
    }

}
