namespace Mobalyz.Domain.Odyssey.Models
{
    public class GenericRequestDto
    {
        public Details details { get; set; }
        public byte[]? pdfTemplate { get; set; } = null;

        public bool? MailBodyIsHtml { get; set; } = false;
        public string? MailBody { get; set; } = "";
        public string? MailSubject { get; set; } = "";

        public string? PdfTemplateName { get; set; } = "";
        public string? HtmlTemplateName { get; set; } = "";

        //Security
        public bool? PasswordProtect { get; set; } = false;
        public string? Password { get; set; } = "";

        //Email
        public bool? SendEmail { get; set; } = false;
        public List<string>? SendTo { get; set; }
    }

    public class Details
    {
        //Individual
        public string? __Title { get; set; } = "";
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

        //Postal
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

        //Banking
        public string? __BankName { get; set; } = "";
        public string? __BankBranch { get; set; } = "";
        public string? __BankCode { get; set; } = "";
        public string? __BankAccNum { get; set; } = "";
        public string? __AccType { get; set; } = "";

        //Internal
        public string? __DealRef { get; set; } = "";
        public string? __DealerName { get; set; } = "";
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

        

        //Date
        public string? __StartDate { get; set; } = "";
        public string? __EndDate { get; set; } = "";

        //Other
        public string? __FSPNo { get; set; } = "";
        public string? __FinManIdNo { get; set; } = "";

        //Generic
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

        //List
        public List<string>? ItemHeadings { get; set; }
        public List<genList>? RowItems { get; set; }

        public string? __Amount { get; set; } = "";
        public string? __OthrAmt { get; set; } = "";
        public string? __Subtotal { get; set; } = "";
        public string? __Vat { get; set; } = "";
        public string? __Total { get; set; } = "";

        public class genList
        {
            public string? __Item0 { get; set; } = "";
            public string? __Item1 { get; set; } = "";
            public string? __Item2 { get; set; } = "";
            public string? __Item3 { get; set; } = "";
            public string? __Item4 { get; set; } = "";
            public string? __Item5 { get; set; } = "";
        }
    }
}
