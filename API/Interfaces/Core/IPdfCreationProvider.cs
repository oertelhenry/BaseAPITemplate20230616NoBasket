using Mobalyz.Domain.Odyssey.Models;

namespace Mobalyz.Odyssey.Resources.Provider
{
    public interface IPdfCreationProvider
    {
        //Task<bool> ExtractNlr();
        //Task<bool> extractFromCompuscanReponseXml();
        //Task<bool> extractFromBlazeReponseXml();
        Task<bool> rescoreBlazeRequstXml();

        Task<DocumentResponseDto> CreatePdf(dynamic request);

        Task<DocumentResponseDto> GenerateContract(ContractRequestDto request);

        Task<DocumentResponseDto> GeneratePodium(PodiumRequestDto request);

        Task<DocumentResponseDto> GenerateDicInvoice(DicInvoiceRequest request);

        //Task<List<DocList>> GetPodiumVersionListAsync(string appRef);

        //Task<List<DocList>> GetContractVersionListAsync(string appRef);

        //Task<DocumentResponseDto> ReCreateContract(string appref, string contractNumber);

        //Task<DocumentResponseDto> ReCreatePodium(string appref, string podiumNumber);

        Task<DocumentResponseDto> GenerateInsuranceProposal(InsuranceRequestDto request);
        Task<DocumentResponseDto> GenerateInsurancePolicy(InsuranceRequestDto request);

        Task<GenericResponseDto> GenerateGenericPdfAsync(GenericRequestDto request);
        //Task<GenericResponseDto> GenerateTempTestPdfAsync(GenericRequestDto request);
    }
}
