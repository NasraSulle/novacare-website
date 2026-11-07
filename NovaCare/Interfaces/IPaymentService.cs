using NovaCare.Shared.DTOs.PaymentDTOs;

namespace NovaCare.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentModeDto>> GetAllPaymentModesAsync();
        Task<List<InsuaranceDto>> GetAllInsuranceCompaniesAsync();
    }
}
