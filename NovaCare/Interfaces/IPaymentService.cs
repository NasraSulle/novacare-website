using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.PaymentDTOs;

namespace NovaCare.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentModeDto>> GetAllPaymentModesAsync();
        Task<List<InsuaranceDto>> GetAllInsuranceCompaniesAsync();

        Task<List<PaymentDto>> GetAllPaymentsAsync();
        Task<List<BillDto>> GetAllBillsAsync();

        Task<PaymentDto> GetPaymentByIdAsync(int paymentId);
        Task<BillDto> GetBillByIdAsync(int billId);

        Task<int> AddPaymentAsync(PaymentDto payment);
        Task<BillDto> AddBillAsync(BillDto bill);

        Task<bool> UpdatePaymentAsync(PaymentDto payment, int paymentId);
        Task<bool> UpdateBillAsync(BillDto bill, int billId);
    }
}
