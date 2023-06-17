using Mobalyz.Domain.Odyssey.Models;

namespace Mobalyz.Odyssey.Data.Models
{
    public class InsuranceValues
    {
        public InsuranceValues(InsuranceRequestDto request)
        {
            TodayDate = request.TodayDate;
            PolNumber = request.PolNumber;
            FirstName = request.FirstName;
            SurName = request.SurName;
            IDNumber = request.IDNumber;
            DateOfBirth = request.DateOfBirth;
            CellNum = request.CellNum;
            HomeTel = request.HomeTel;
            AltPhoneNum = request.AltPhoneNum;
            PoAdd1 = request.PoAdd1;
            PoAdd2 = request.PoAdd2;
            PoAdd3 = request.PoAdd3;
            PoAdd4 = request.PoAdd4;
            PoAddCode = request.PoAddCode;
            PhAddr1 = request.PhAddr1;
            PhAddr2 = request.PhAddr2;
            PhAddr3 = request.PhAddr3;
            PhAddr4 = request.PhAddr4;
            PhAddrCode = request.PhAddrCode;
            VehDesc = request.VehDesc;
            ChasNum = request.ChasNum;
            EngNum = request.EngNum;
            VehYear = request.VehYear;
            TrackUnit = request.TrackUnit;
            TrackCompany = request.TrackCompany;
        TrackCompUnitType = request.TrackCompUnitType;
            //RetValue = request.RetValue;
            CreditProvName = request.CreditProvName;
            BroName = request.BroName;
            BroPostAddr = request.BroPostAddr;
            BroNumber = request.BroNumber;
            BroFaxNum = request.BroFaxNum;
            StartDate = request.StartDate;
            RenewalDate = request.RenewalDate;
            RetailValue = request.RetailValue;
            MonthAmount = request.MonthAmount;
            SasriaAmount = request.SasriaAmount;
            VatAmount = request.VatAmount;
            TotalAmount = request.TotalAmount;
        }

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
        public string TrackUnit { get; set; } = "";
        public string TrackCompany { get; set; } = "";
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
