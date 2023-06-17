namespace Data.Authorization
{
    public class Identity
    {
        public int Id { get; set; }
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string lastLogon { get; set; }
        public bool enabled { get; set; }
        public string role { get; set; }
    }
}

