using Microsoft.EntityFrameworkCore.Metadata;

namespace Mobalyz.Data.Models.Dto.Authorization
{
    public class AuthResult
    {
        public bool authed { get; set; }
        public string? fullName { get; set; }
        public string? userName { get; set; }
        public string? token { get; set; }
    }
}
