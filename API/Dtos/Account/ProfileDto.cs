namespace Mobalyz.Data.Models.Dto
{
    public class ProfileDto
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime LastActive { get; set; }
        public string? Role { get; set; }
        public DateTime LastLoggedOnDate { get; set; }
        public int LineManagerId { get; set; }
        public Boolean IsActive { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ProfileStatus { get; set; }
        public bool DealersAccess { get; set; } = true;
        public int DealersRole { get; set; }
        public string UserPrefix { get; set; }
        //[JsonIgnore]
        public string? ApplicationNumber { get; set; }
    }
}
