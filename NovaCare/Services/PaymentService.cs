using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NovaCare.Components.Pages.LabPages;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.PatientModals;
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

        public async Task<BillDto?> AddBillAsync(BillDto bill)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var newBill = new Bill
                {
                    BillDate = bill.BillDate,
                    BillNumber = bill.BillNumber,
                    BillDescription = bill.BillDescription,
                    TotalAmount = bill.TotalAmount,
                    BillTypeId = bill.BillTypeId,
                    StaffId = bill.StaffId,
                    VisitId = bill.VisitId,

                    BillItems = bill.BillItems?.Select(item => new BillItem
                    {
                        ItemName = item.ItemName,
                        SourceType = item.SourceType,
                        SourceItemId = item.SourceItemId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    }).ToList() ?? new List<BillItem>()
                };

                context.Bills.Add(newBill);
                await context.SaveChangesAsync();

                // Populate the generated Bill ID and BillItem IDs back onto the DTO
                bill.Id = newBill.Id;

                if (bill.BillItems != null)
                {
                    var savedItems = newBill.BillItems.ToList();
                    for (int i = 0; i < bill.BillItems.Count; i++)
                    {
                        bill.BillItems[i].Id = savedItems[i].Id;
                        bill.BillItems[i].BillId = newBill.Id;
                    }
                }

                return bill;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding bill: {ex.Message}");
                return null;
            }
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

                    VisitName = (bill.Visit != null && bill.Visit.VisitType != null)
                        ? bill.Visit.VisitType.TypeName
                        : string.Empty,

                    BillTypeId = bill.BillTypeId,
                    BillTypeName = bill.BillType != null ? bill.BillType.TypeNme : string.Empty,

                    Payment = bill.Payment == null ? null : new PaymentDto
                    {
                        Id = bill.Payment.Id,
                        Amount = bill.Payment.Amount,
                        PaymentDate = bill.Payment.PaymentDate,

                        BillId = bill.Id,
                        PaymentStatusId = bill.Payment.PaymentStatusId,
                        PaymentModeId = bill.Payment.PaymentModeId,

                        PaymentStatusName = bill.Payment.PaymentStatus != null ? bill.Payment.PaymentStatus.PaymentStatusName : string.Empty,
                        PaymentModeName = bill.Payment.PaymentMode != null ? bill.Payment.PaymentMode.PaymentModeName : string.Empty,
                        BillTypeName = bill.BillType != null ? bill.BillType.TypeNme : string.Empty
                    }
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

        public async Task<bool> UpdateBillAsync(BillDto dto, int billId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var extBill = await context.Bills.FirstOrDefaultAsync(x => x.Id == billId);

                if (extBill != null)
                {
                    extBill.BillDescription = dto.BillDescription;
                    extBill.TotalAmount = dto.TotalAmount;
                    extBill.PaidAmount = dto.PaidAmount;
                    extBill.VisitId = dto.VisitId;
                    extBill.BillTypeId = dto.BillTypeId;
                    extBill.PaymentId = dto.PaymentId;

                    if (dto.BillItems != null)
                    {
                        foreach (var biDto in dto.BillItems)
                        {
                            var biToAdd = new BillItem
                            {
                                ItemName = biDto.ItemName,
                                SourceType = biDto.SourceType,
                                SourceItemId = biDto.SourceItemId,
                                Quantity = biDto.Quantity,
                                UnitPrice = biDto.UnitPrice,
                                BillId = extBill.Id,
                            };

                            extBill.BillItems.Add(biToAdd);
                        }
                    }
                    

                    context.Bills.Update(extBill);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return false;
            }
        }

        public async Task<int> UpdatePaymentAsync(PaymentDto payment, int paymentId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IPaymentService.UpdatePaymentAsync(PaymentDto payment, int paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
