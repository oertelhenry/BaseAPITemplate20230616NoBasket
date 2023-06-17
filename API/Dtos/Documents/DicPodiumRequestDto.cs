namespace Mobalyz.Domain.Odyssey.Models
{
    public class DicInvoiceRequest
    {
        public string? invNumber { get; set; } = "";
        public string? CurrentDateTime { get; set; } = "";
        public string? DicAmount { get; set; } = "";
        public string? DicVatAmount { get; set; } = "";
        public string? TotalDicAmount { get; set; } = "";

        public string? DealAcountNr { get; set; } = "";
        public string? DealerName { get; set; } = "";
        public string? FandIName { get; set; } = "";
        public string? ClientName { get; set; } = "";
        public string? ClientIdNumber { get; set; } = "";
        public string? NewOrUsed { get; set; } = "";
        public string? VehicleYear { get; set; } = "";
        public string? VehMake { get; set; } = "";
        public string? VehModel { get; set; } = "";
        public string? RemitDate { get; set; } = "";
        public string? CashPrice { get; set; } = "";
        public string? RemitVat { get; set; } = "";
        public string? RemitNoVatExtras { get; set; } = "";
        public string? RemitVatableExtras { get; set; } = "";
        public string? RemitDeposit { get; set; } = "";
        public string? RemitTotal { get; set; } = "";
        public string? RemitDic { get; set; } = "";
        public string? RemitTotalAmtPay { get; set; } = "";
        public List<notes>? Notes { get; set; }
    }

    public class notes
    {
        public string? Note { get; set; } = "";
    }

}
