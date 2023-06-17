namespace Mobalyz.Data.Models.Dto
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public bool? DealersAccess { get; set; }
        public int DealersRole { get; set; }
        public int ProfileStatus { get; set; }
    }
}