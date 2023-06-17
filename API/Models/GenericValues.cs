using Mobalyz.Domain.Odyssey.Models;

namespace Mobalyz.Odyssey.Data.Models
{
    public class GenericValues
    {
        public GenericValues(GenericRequestDto request)
        {
            __Name = string.IsNullOrEmpty(request.details.__Name) ? "" : request.details.__Name;
            __SurName = string.IsNullOrEmpty(request.details.__SurName) ? "" : request.details.__SurName;
            __IdNum = string.IsNullOrEmpty(request.details.__IdNum) ? "" : request.details.__IdNum;
            __AccountNum = string.IsNullOrEmpty(request.details.__AccountNum) ? "" : request.details.__AccountNum;
            __HomeNum = string.IsNullOrEmpty(request.details.__HomeNum) ? "" : request.details.__HomeNum;
            __CellNum = string.IsNullOrEmpty(request.details.__CellNum) ? "" : request.details.__CellNum;
            __StreetAdd = string.IsNullOrEmpty(request.details.__StreetAdd) ? "" : request.details.__StreetAdd;
            __StreetAdd1 = string.IsNullOrEmpty(request.details.__StreetAdd1) ? "" : request.details.__StreetAdd1;
            __StreetAdd2 = string.IsNullOrEmpty(request.details.__StreetAdd2) ? "" : request.details.__StreetAdd2;
            __StreetAdd3 = string.IsNullOrEmpty(request.details.__StreetAdd3) ? "" : request.details.__StreetAdd3;
            __Suburb = string.IsNullOrEmpty(request.details.__Suburb) ? "" : request.details.__Suburb;
            __Province = string.IsNullOrEmpty(request.details.__Province) ? "" : request.details.__Province;
            __PoCode = string.IsNullOrEmpty(request.details.__PoCode) ? "" : request.details.__PoCode;
            __EmailAdd = string.IsNullOrEmpty(request.details.__EmailAdd) ? "" : request.details.__EmailAdd;
            __VatNumber = string.IsNullOrEmpty(request.details.__VatNumber) ? "" : request.details.__VatNumber;
            __BankName = string.IsNullOrEmpty(request.details.__BankName) ? "" : request.details.__BankName;
            __BankBranch = string.IsNullOrEmpty(request.details.__BankBranch) ? "" : request.details.__BankBranch;
            __BankCode = string.IsNullOrEmpty(request.details.__BankCode) ? "" : request.details.__BankCode;
            __BankAccNum = string.IsNullOrEmpty(request.details.__BankAccNum) ? "" : request.details.__BankAccNum;
            __AccType = string.IsNullOrEmpty(request.details.__AccType) ? "" : request.details.__AccType;

            __DealRef = string.IsNullOrEmpty(request.details.__DealRef) ? "" : request.details.__DealRef;
            __AssetDescription = string.IsNullOrEmpty(request.details.__AssetDescription) ? "" : request.details.__AssetDescription;
            __EngineNum = string.IsNullOrEmpty(request.details.__EngineNum) ? "" : request.details.__EngineNum;
            __ChassisNum = string.IsNullOrEmpty(request.details.__ChassisNum) ? "" : request.details.__ChassisNum;
            __VinNum = string.IsNullOrEmpty(request.details.__VinNum) ? "" : request.details.__VinNum;
            __RegNum = string.IsNullOrEmpty(request.details.__RegNum) ? "" : request.details.__RegNum;

            __Title = string.IsNullOrEmpty(request.details.__Title) ? "" : request.details.__Title;
            __FSPNo = string.IsNullOrEmpty(request.details.__FSPNo) ? "" : request.details.__FSPNo;
            __FinManIdNo = string.IsNullOrEmpty(request.details.__FinManIdNo) ? "" : request.details.__FinManIdNo;

            __Amount = string.IsNullOrEmpty(request.details.__Amount) ? "" : request.details.__Amount;
            __OthrAmt = string.IsNullOrEmpty(request.details.__OthrAmt) ? "" : request.details.__OthrAmt;
            __VehMake = string.IsNullOrEmpty(request.details.__VehMake) ? "" : request.details.__VehMake;
            __VehModel = string.IsNullOrEmpty(request.details.__VehModel) ? "" : request.details.__VehModel;
            __VehYear = string.IsNullOrEmpty(request.details.__VehYear) ? "" : request.details.__VehYear;
            __VehPrice = string.IsNullOrEmpty(request.details.__VehPrice) ? "" : request.details.__VehPrice;
            __PurchPrice = string.IsNullOrEmpty(request.details.__PurchPrice) ? "" : request.details.__PurchPrice;
            __ReqRate = string.IsNullOrEmpty(request.details.__ReqRate) ? "" : request.details.__ReqRate;
            __ApprRate = string.IsNullOrEmpty(request.details.__ApprRate) ? "" : request.details.__ApprRate;
            __RateType = string.IsNullOrEmpty(request.details.__RateType) ? "" : request.details.__RateType;
            __Instmnt = string.IsNullOrEmpty(request.details.__Instmnt) ? "" : request.details.__Instmnt;
            __TotInstmnt = string.IsNullOrEmpty(request.details.__TotInstmnt) ? "" : request.details.__TotInstmnt;
            __Term = string.IsNullOrEmpty(request.details.__Term) ? "" : request.details.__Term;
            __Deposit = string.IsNullOrEmpty(request.details.__Deposit) ? "" : request.details.__Deposit;
            __ResAmt = string.IsNullOrEmpty(request.details.__ResAmt) ? "" : request.details.__ResAmt;
            __ServFee = string.IsNullOrEmpty(request.details.__ServFee) ? "" : request.details.__ServFee;
            __Extras = string.IsNullOrEmpty(request.details.__Extras) ? "" : request.details.__Extras;
            __MonInsPrem = string.IsNullOrEmpty(request.details.__MonInsPrem) ? "" : request.details.__MonInsPrem;
            __DicAmt = string.IsNullOrEmpty(request.details.__DicAmt) ? "" : request.details.__DicAmt;
            __FNI = string.IsNullOrEmpty(request.details.__FNI) ? "" : request.details.__FNI;
            __AgreeType = string.IsNullOrEmpty(request.details.__AgreeType) ? "" : request.details.__AgreeType;

            __DealerName = string.IsNullOrEmpty(request.details.__DealerName) ? "" : request.details.__DealerName;
            __Deposit = string.IsNullOrEmpty(request.details.__Deposit) ? "" : request.details.__Deposit;
            __DocumentNumber = string.IsNullOrEmpty(request.details.__DocumentNumber) ? "" : request.details.__DocumentNumber;

            __PoAdd1 = string.IsNullOrEmpty(request.details.__PoAdd1) ? "" : request.details.__PoAdd1;
            __PoAdd2 = string.IsNullOrEmpty(request.details.__PoAdd2) ? "" : request.details.__PoAdd2;
            __PoAdd3 = string.IsNullOrEmpty(request.details.__PoAdd3) ? "" : request.details.__PoAdd3;
            __PoAdd4 = string.IsNullOrEmpty(request.details.__PoAdd4) ? "" : request.details.__PoAdd4;
            __PoAddCode = string.IsNullOrEmpty(request.details.__PoAddCode) ? "" : request.details.__PoAddCode;

            


            __OthrName = string.IsNullOrEmpty(request.details.__OthrName) ? "" : request.details.__OthrName;
            __OthrPoAdd = string.IsNullOrEmpty(request.details.__OthrPoAdd) ? "" : request.details.__OthrPoAdd;
            __OthrNum = string.IsNullOrEmpty(request.details.__OthrNum) ? "" : request.details.__OthrNum;
            __OthrFaxNum = string.IsNullOrEmpty(request.details.__OthrFaxNum) ? "" : request.details.__OthrFaxNum;

            __DateOfBirth = string.IsNullOrEmpty(request.details.__DateOfBirth) ? "" : request.details.__DateOfBirth;
            __AltNum = string.IsNullOrEmpty(request.details.__AltNum) ? "" : request.details.__AltNum;

            __StartDate = string.IsNullOrEmpty(request.details.__StartDate) ? "" : request.details.__StartDate;
            __EndDate = string.IsNullOrEmpty(request.details.__EndDate) ? "" : request.details.__EndDate;


            __GenField0 = string.IsNullOrEmpty(request.details.__GenField0) ? "" : request.details.__GenField0;
            __GenField1 = string.IsNullOrEmpty(request.details.__GenField1) ? "" : request.details.__GenField1;
            __GenField2 = string.IsNullOrEmpty(request.details.__GenField2) ? "" : request.details.__GenField2;
            __GenField3 = string.IsNullOrEmpty(request.details.__GenField3) ? "" : request.details.__GenField3;
            __GenField4 = string.IsNullOrEmpty(request.details.__GenField4) ? "" : request.details.__GenField4;
            __GenField5 = string.IsNullOrEmpty(request.details.__GenField5) ? "" : request.details.__GenField5;
            __GenField6 = string.IsNullOrEmpty(request.details.__GenField6) ? "" : request.details.__GenField6;
            __GenField7 = string.IsNullOrEmpty(request.details.__GenField7) ? "" : request.details.__GenField7;
            __GenField8 = string.IsNullOrEmpty(request.details.__GenField8) ? "" : request.details.__GenField8;
            __GenField9 = string.IsNullOrEmpty(request.details.__GenField9) ? "" : request.details.__GenField9;
            __GenField10 = string.IsNullOrEmpty(request.details.__GenField10) ? "" : request.details.__GenField10;
            __GenField11 = string.IsNullOrEmpty(request.details.__GenField11) ? "" : request.details.__GenField11;
            __GenField12 = string.IsNullOrEmpty(request.details.__GenField12) ? "" : request.details.__GenField12;
            __GenField13 = string.IsNullOrEmpty(request.details.__GenField13) ? "" : request.details.__GenField13;
            __GenField14 = string.IsNullOrEmpty(request.details.__GenField14) ? "" : request.details.__GenField14;
            __GenField15 = string.IsNullOrEmpty(request.details.__GenField15) ? "" : request.details.__GenField15;
            __GenField16 = string.IsNullOrEmpty(request.details.__GenField16) ? "" : request.details.__GenField16;
            __GenField17 = string.IsNullOrEmpty(request.details.__GenField17) ? "" : request.details.__GenField17;
            __GenField18 = string.IsNullOrEmpty(request.details.__GenField18) ? "" : request.details.__GenField18;
            __GenField19 = string.IsNullOrEmpty(request.details.__GenField19) ? "" : request.details.__GenField19;
            __GenField20 = string.IsNullOrEmpty(request.details.__GenField20) ? "" : request.details.__GenField20;
            __Subtotal = string.IsNullOrEmpty(request.details.__Subtotal) ? "" : request.details.__Subtotal;
            __Vat = string.IsNullOrEmpty(request.details.__Vat) ? "" : request.details.__Vat;
            __Total = string.IsNullOrEmpty(request.details.__Total) ? "" : request.details.__Total;

        }

        //public string? __PdfTemplateName { get; set; } = "";
        //Individual
        public string? __Name { get; set; } = "";
        public string? __SurName { get; set; } = "";
        public string? __IdNum { get; set; } = "";
        public string? __AccountNum { get; set; } = "";
        public string? __HomeNum { get; set; } = "";
        public string? __CellNum { get; set; } = "";
        public string? __AltNum { get; set; } = "";
        public string? __DateOfBirth { get; set; } = "";

        //Address
        public string? __StreetAdd { get; set; } = "";
        public string? __StreetAdd1 { get; set; } = "";
        public string? __StreetAdd2 { get; set; } = "";
        public string? __StreetAdd3 { get; set; } = "";
        public string? __Suburb { get; set; } = "";
        public string? __Province { get; set; } = "";
        public string? __PoCode { get; set; } = "";
        public string? __EmailAdd { get; set; } = "";
        public string? __VatNumber { get; set; } = "";
        public string? __PoAdd1 { get; set; } = "";
        public string? __PoAdd2 { get; set; } = "";
        public string? __PoAdd3 { get; set; } = "";
        public string? __PoAdd4 { get; set; } = ""; 
        public string? __PoAddCode { get; set; } = "";



        //3rd Party
        public string? __OthrName { get; set; } = "";
        public string? __OthrPoAdd { get; set; } = "";
        public string? __OthrNum { get; set; } = "";
        public string? __OthrFaxNum { get; set; } = "";



        public string? __BankName { get; set; } = "";
        public string? __BankBranch { get; set; } = "";
        public string? __BankCode { get; set; } = "";
        public string? __BankAccNum { get; set; } = "";
        public string? __AccType { get; set; } = "";


        public string? __DealRef { get; set; } = "";
        public string? __AssetDescription { get; set; } = "";
        public string? __EngineNum { get; set; } = "";
        public string? __ChassisNum { get; set; } = "";
        public string? __VinNum { get; set; } = "";
        public string? __RegNum { get; set; } = "";

        public string? __VehMake { get; set; } = "";
        public string? __VehModel { get; set; } = "";
        public string? __VehYear { get; set; } = "";
        public string? __VehPrice { get; set; } = "";
        public string? __PurchPrice { get; set; } = "";
        public string? __ReqRate { get; set; } = "";
        public string? __ApprRate { get; set; } = "";
        public string? __RateType { get; set; } = "";
        public string? __Instmnt { get; set; } = "";
        public string? __TotInstmnt { get; set; } = "";
        public string? __Term { get; set; } = "";
        public string? __Deposit { get; set; } = "";
        public string? __ResAmt { get; set; } = "";
        public string? __ServFee { get; set; } = "";
        public string? __Extras { get; set; } = "";
        public string? __MonInsPrem { get; set; } = "";
        public string? __DicAmt { get; set; } = "";
        public string? __FNI { get; set; } = "";
        public string? __AgreeType { get; set; } = "";
        public string? __DocumentNumber { get; set; } = "";

        

        public string? __DealerName { get; set; } = "";

        //Date
        public string? __StartDate { get; set; } = "";
        public string? __EndDate { get; set; } = "";

        public string? __Title { get; set; } = "";
        public string? __FSPNo { get; set; } = "";
        public string? __FinManIdNo { get; set; } = "";


        public string? __GenField0 { get; set; } = "";
        public string? __GenField1 { get; set; } = "";
        public string? __GenField2 { get; set; } = "";
        public string? __GenField3 { get; set; } = "";
        public string? __GenField4 { get; set; } = "";
        public string? __GenField5 { get; set; } = "";
        public string? __GenField6 { get; set; } = "";
        public string? __GenField7 { get; set; } = "";
        public string? __GenField8 { get; set; } = "";
        public string? __GenField9 { get; set; } = "";
        public string? __GenField10 { get; set; } = "";
        public string? __GenField11 { get; set; } = "";
        public string? __GenField12 { get; set; } = "";
        public string? __GenField13 { get; set; } = "";
        public string? __GenField14 { get; set; } = "";
        public string? __GenField15 { get; set; } = "";
        public string? __GenField16 { get; set; } = "";
        public string? __GenField17 { get; set; } = "";
        public string? __GenField18 { get; set; } = "";
        public string? __GenField19 { get; set; } = "";
        public string? __GenField20 { get; set; } = "";


        public string? __Subtotal { get; set; } = "";
        public string? __Vat { get; set; } = "";
        public string? __Total { get; set; } = "";
        public string? __Amount { get; set; } = "";
        public string? __OthrAmt { get; set; } = "";

    }
}
