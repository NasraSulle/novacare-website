using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Shared.DTOs.PaymentDTOs;

namespace NovaCare.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public PaymentService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<InsuaranceDto>> GetAllInsuranceCompaniesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var companies = await context.Insuarances
                .AsNoTracking()
                .Select(insuranceCompany => new InsuaranceDto
                {
                    Id = insuranceCompany.Id,
                    InsuaranceName = insuranceCompany.InsuaranceName,

                })
                .ToListAsync();

            return companies;
        }

        public async Task<List<PaymentModeDto>> GetAllPaymentModesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var patientMode = await context.PaymentModes
                .AsNoTracking()
                .Select(mode => new PaymentModeDto
                {
                    Id = mode.Id,
                    PaymentModeName = mode.PaymentModeName,

                })
                .ToListAsync();

            return patientMode;
        }
    }
}
