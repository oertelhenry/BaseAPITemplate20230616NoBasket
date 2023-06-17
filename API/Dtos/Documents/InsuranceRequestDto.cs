namespace Mobalyz.Domain.Odyssey.Models
{
    public class InsuranceRequestDto
    {
        public string TodayDate { get; set; } = "";
        public string PolNumber { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string SurName { get; set; } = "";
        public string IDNumber { get; set; } = "";
        public string DateOfBirth { get; set; } = "";
        public string CellNum { get; set; } = "";
        public string HomeTel { get; set; } = "";
        public string AltPhoneNum { get; set; } = "";
        public string PoAdd1 { get; set; } = "";
        public string PoAdd2 { get; set; } = "";
        public string PoAdd3 { get; set; } = "";
        public string PoAdd4 { get; set; } = "";
        public string PoAddCode { get; set; } = "";
        public string PhAddr1 { get; set; } = "";
        public string PhAddr2 { get; set; } = "";
        public string PhAddr3 { get; set; } = "";
        public string PhAddr4 { get; set; } = "";
        public string PhAddrCode { get; set; } = "";
        public string VehDesc { get; set; } = "";
        public string ChasNum { get; set; } = "";
        public string EngNum { get; set; } = "";
        public string VehYear { get; set; } = "";
        public string TrackCompany { get; set; } = "";
        public string TrackUnit { get; set; } = "";
        public string TrackCompUnitType { get; set; } = "";
        //public string RetValue { get; set; } = "";
        public string CreditProvName { get; set; } = "";  //????????same
        public string BroName { get; set; } = "";
        public string BroPostAddr { get; set; } = "";
        public string BroNumber { get; set; } = "";
        public string BroFaxNum { get; set; } = "";  //Email?????????
        public string StartDate { get; set; } = "";
        public string RenewalDate { get; set; } = "";
        public string RetailValue { get; set; } = "";  //should this be removed??????????
        public string MonthAmount { get; set; } = "";
        public string SasriaAmount { get; set; } = "";
        public string VatAmount { get; set; } = "";
        public string TotalAmount { get; set; } = "";
    }
}
