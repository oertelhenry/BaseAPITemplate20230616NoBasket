using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobalyz.Domain.Odyssey.Models
{
    public class DocumentListResponseDto
    {
        public List<DocList> DocList  { get; set; }
    }

    public class DocList
    {
        public string appRef { get; set; }
        public string documentNumber { get; set; }
    }
}
