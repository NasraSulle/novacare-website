using NovaCare.Shared.DTOs.InsuranceDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.PaymentDTOs;
using NovaCare.Shared.Enums;

namespace NovaCare.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentModeDto>> GetAllPaymentModesAsync();
        Task<List<InsuaranceDto>> GetAllInsuranceCompaniesAsync();
        Task<List<InsuranceSchemeDto>> GetSchemesByInsuranceIdAsync(int insuaranceId);

        // Insurer Tariffs (Agreed pricing & coverage rules per NHIF/ZHIF/Jubilee/Strategis scheme)
        Task<List<InsurerTariffDto>> GetTariffsByInsuranceAsync(int insuaranceId, int? schemeId = null);
        Task<InsurerTariffDto> SaveInsurerTariffAsync(InsurerTariffDto dto);
        Task<BillItemDto> CalculateBillItemCoverageAsync(BillItemDto item, int paymentModeId, int? patientInsuaranceId);

        // Payments (Patient Cash/MNO/Card + Insurer Claim Settlements)
        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto> GetPaymentByIdAsync(int paymentId);
        Task<int> AddPaymentAsync(PaymentDto payment);
        Task<bool> UpdatePaymentAsync(PaymentDto payment, int paymentId);

        // Bills & BillItems
        Task<List<BillDto>> GetAllBillsAsync();
        Task<BillDto> GetBillByIdAsync(int billId);
        Task<BillDto> AddBillAsync(BillDto bill);
        Task<bool> UpdateBillAsync(BillDto bill, int billId);

        // Insurance Claims (NHIF, ZHIF, TPA Portals, Manual)
        Task<List<InsuranceClaimDto>> GetAllInsuranceClaimsAsync(ClaimStatus? status = null);
        Task<InsuranceClaimDto?> GetClaimByBillIdAsync(int billId);
        //Task<InsuranceClaimDto> CreateOrSubmitClaimAsync(int billId, string staffId, string? primaryIcd10Code = null, string? folioNumber = null);
        Task<InsuranceClaimDto> CreateOrSubmitClaimAsync(int billId, string staffId, string? primaryIcd10Code = null, string? folioNumber = null, bool submitImmediately = true);

        Task<bool> UpdateClaimAdjudicationAsync(int claimId, ClaimStatus status, decimal approvedAmount, decimal rejectedAmount, string? reason = null);
        Task<int> RecordInsurerSettlementAsync(int claimId, decimal settledAmount, string settlementReference, string? batchNumber = null);
        Task<int> AppendServiceToVisitBillAsync(int visitId, BillItemDto item, string staffId);

    }
}
