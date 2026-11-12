using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Shared.DTOs.PatientDTOs;
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

        public async Task<int> AddBillAsync(BillDto bill)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddPaymentAsync(PaymentDto payment)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BillDto>> GetAllBillsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var bills = await context.Bills
                .AsNoTracking()
                .Select(bill => new BillDto
                {
                    Id = bill.Id,
                    BillNumber = bill.BillNumber,
                    BillDate = bill.BillDate,
                    BillDescription = bill.BillDescription,
                    TotalAmount = bill.TotalAmount,
                    VisitId = bill.VisitId,

                    // Access Visit -> VisitType directly since Visit is a single object
                    VisitName = (bill.Visit != null && bill.Visit.VisitType != null)
                        ? bill.Visit.VisitType.TypeName
                        : string.Empty,

                    BillTypeId = bill.BillTypeId,
                    BillTypeName = bill.BillType != null ? bill.BillType.TypeNme : string.Empty,

                    // Map Payments so PaidAmount, BalanceDue, PaymentStatusId, and PaymentStatusName auto-calculate
                    Payments = bill.Payments != null
                        ? bill.Payments.Select(p => new PaymentDto
                        {
                            Id = p.Id,
                            PaymentDate = p.PaymentDate,
                            Amount = p.Amount,
                            BillId = p.BillId,
                            PaymentStatusId = p.PaymentStatusId,
                            PaymentModeId = p.PaymentModeId
                        }).ToList()
                        : new List<PaymentDto>()
                })
                .ToListAsync();

            return bills;
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

        public async Task<List<PaymentDto>> GetAllPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BillDto> GetBillByIdAsync(int billId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateBillAsync(BillDto bill, int billId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePaymentAsync(PaymentDto payment, int paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
