using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobalyz.Domain.Odyssey.Models
{
    public class DocumentResponseDto
    {
        public byte[] pdfDocument { get; set; }
        public int documentVersion { get; set; }
    }
}
