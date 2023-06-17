using System.ComponentModel.DataAnnotations;

namespace Mobalyz.Data.Models.Dto.Authorization
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
