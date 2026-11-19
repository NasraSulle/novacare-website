using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using NovaCare.Components.Pages.LabPages;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.InsuranceModals;
using NovaCare.Modals.LabTestModals;
using NovaCare.Modals.PatientModals;
using NovaCare.Modals.PaymentModals;
using NovaCare.Shared.DTOs.InsuranceDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.PaymentDTOs;
using NovaCare.Shared.Enums;

namespace NovaCare.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public PaymentService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<BillItemDto> CalculateBillItemCoverageAsync(BillItemDto item, int paymentModeId, int? patientInsuaranceId)
        {
            using var context = _contextFactory.CreateDbContext();
            //return await CalculateItemInternalAsync(context, item, paymentModeId, patientInsuaranceId);
            return await CalculateItemInternalAsync(item, paymentModeId, patientInsuaranceId);
        }

        //private static async Task<BillItemDto> CalculateItemInternalAsync(BillItemDto item, int paymentModeId, int? patientInsuaranceId)
        private async Task<BillItemDto> CalculateItemInternalAsync(BillItemDto item, int paymentModeId, int? patientInsuaranceId)
        {
            using var context = _contextFactory.CreateDbContext();

            if (paymentModeId != 2 || !patientInsuaranceId.HasValue)
            {
                var cashSubTotal = Math.Round(item.UnitPrice * (decimal)item.Quantity, 2);
                item.IsInsuranceCovered = false;
                item.InsuranceCoveragePercent = 0m;
                item.InsuranceCoveredAmount = 0m;
                item.PatientPayableAmount = cashSubTotal;
                item.ClaimItemStatus = ClaimStatus.Not_Applicable;
                return item;
            }

            var policy = await context.PatientInsuarances
                .AsNoTracking()
                .Include(pi => pi.InsuranceScheme)
                .FirstOrDefaultAsync(pi => pi.PatientInsuaranceId == patientInsuaranceId.Value);

            if (policy == null || !policy.IsActive)
            {
                var fallbackSubTotal = Math.Round(item.UnitPrice * (decimal)item.Quantity, 2);
                item.IsInsuranceCovered = false;
                item.InsuranceCoveragePercent = 0m;
                item.InsuranceCoveredAmount = 0m;
                item.PatientPayableAmount = fallbackSubTotal;
                item.ClaimItemStatus = ClaimStatus.Not_Applicable;
                return item;
            }

            var tariff = await context.InsurerTariffs
                .AsNoTracking()
                .Where(t => t.InsuaranceId == policy.InsuaranceId
                            && t.ServiceType == item.SourceType
                            && t.ServiceItemId == item.SourceItemId
                            && (t.InsuranceSchemeId == policy.InsuranceSchemeId || t.InsuranceSchemeId == null))
                .OrderByDescending(t => t.InsuranceSchemeId.HasValue)
                .FirstOrDefaultAsync();

            if (tariff != null)
            {
                item.InsurerItemCode = tariff.InsurerItemCode;
                if (tariff.AgreedPrice > 0)
                    item.UnitPrice = tariff.AgreedPrice;

                var subTotal = Math.Round(item.UnitPrice * (decimal)item.Quantity, 2);

                if (!tariff.IsCovered || tariff.CoveragePercent <= 0)
                {
                    item.IsInsuranceCovered = false;
                    item.InsuranceCoveragePercent = 0m;
                    item.InsuranceCoveredAmount = 0m;
                    item.PatientPayableAmount = subTotal;
                    item.ClaimItemStatus = ClaimStatus.Not_Applicable;
                }
                else
                {
                    item.IsInsuranceCovered = true;
                    item.InsuranceCoveragePercent = tariff.CoveragePercent;
                    item.InsuranceCoveredAmount = Math.Round(subTotal * (tariff.CoveragePercent / 100m), 2);
                    item.PatientPayableAmount = subTotal - item.InsuranceCoveredAmount;
                    item.ClaimItemStatus = ClaimStatus.Draft;
                }
            }
            else
            {
                var subTotal = Math.Round(item.UnitPrice * (decimal)item.Quantity, 2);
                var defaultPercent = policy.InsuranceScheme?.DefaultCoveragePercent ?? 100m;

                item.IsInsuranceCovered = defaultPercent > 0;
                item.InsuranceCoveragePercent = defaultPercent;
                item.InsuranceCoveredAmount = Math.Round(subTotal * (defaultPercent / 100m), 2);
                item.PatientPayableAmount = subTotal - item.InsuranceCoveredAmount;
                item.ClaimItemStatus = defaultPercent > 0 ? ClaimStatus.Draft : ClaimStatus.Not_Applicable;
            }

            return item;
        }

        public async Task<BillDto> AddBillAsync(BillDto bill)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var visit = await context.Visits
                    .AsNoTracking()
                    .FirstOrDefaultAsync(v => v.Id == bill.VisitId);

                int effectivePaymentModeId = bill.PaymentModeId > 0
                    ? bill.PaymentModeId
                    : (visit?.PaymentModeId ?? 1);

                int? effectivePolicyId = effectivePaymentModeId == 2
                    ? (bill.PatientInsuaranceId ?? visit?.PatientInsuaranceId)
                    : null;

                var itemEntities = new List<BillItem>();
                if (bill.BillItems != null)
                {
                    foreach (var itemDto in bill.BillItems)
                    {
                        //await CalculateItemInternalAsync(context, itemDto, effectivePaymentModeId, effectivePolicyId);
                        await CalculateItemInternalAsync(itemDto, effectivePaymentModeId, effectivePolicyId);

                        itemEntities.Add(new BillItem
                        {
                            ItemName = itemDto.ItemName,
                            SourceType = itemDto.SourceType,
                            SourceItemId = itemDto.SourceItemId,
                            InsurerItemCode = itemDto.InsurerItemCode,
                            Quantity = itemDto.Quantity,
                            UnitPrice = itemDto.UnitPrice,
                            IsInsuranceCovered = itemDto.IsInsuranceCovered,
                            InsuranceCoveragePercent = itemDto.InsuranceCoveragePercent,
                            InsuranceCoveredAmount = itemDto.InsuranceCoveredAmount,
                            PatientPayableAmount = itemDto.PatientPayableAmount,
                            ClaimItemStatus = itemDto.ClaimItemStatus,
                            RejectionReason = itemDto.RejectionReason
                        });
                    }
                }

                decimal totalAmount = itemEntities.Sum(i => i.TotalAmount);
                decimal totalInsuranceAmount = itemEntities.Sum(i => i.InsuranceCoveredAmount);
                decimal totalPatientAmount = itemEntities.Sum(i => i.PatientPayableAmount);

                int initialStatusId = (effectivePaymentModeId == 2 && totalPatientAmount == 0m && totalInsuranceAmount > 0m)
                    ? 4 // Insurance Review
                    : 1; // Unpaid

                var newBill = new Bill
                {
                    BillDate = bill.BillDate == default ? DateTime.UtcNow : bill.BillDate,
                    BillNumber = string.IsNullOrWhiteSpace(bill.BillNumber)
                        ? $"NCMH-BN-{DateTime.UtcNow:yyMMddHHmmss}"
                        : bill.BillNumber,
                    BillDescription = bill.BillDescription,
                    TotalAmount = totalAmount,
                    TotalPatientAmount = totalPatientAmount,
                    TotalInsuranceAmount = totalInsuranceAmount,
                    PatientPaidAmount = 0m,
                    InsurancePaidAmount = 0m,
                    VisitId = bill.VisitId,
                    BillTypeId = bill.BillTypeId,
                    PaymentModeId = effectivePaymentModeId,
                    PaymentStatusId = initialStatusId,
                    PatientInsuaranceId = effectivePolicyId,
                    StaffId = bill.StaffId,
                    BillItems = itemEntities
                };

                context.Bills.Add(newBill);
                await context.SaveChangesAsync();

                bill.Id = newBill.Id;
                bill.BillNumber = newBill.BillNumber;
                bill.PaymentModeId = newBill.PaymentModeId;
                bill.PaymentStatusId = newBill.PaymentStatusId;
                bill.PatientInsuaranceId = newBill.PatientInsuaranceId;
                bill.TotalAmount = newBill.TotalAmount;
                bill.TotalPatientAmount = newBill.TotalPatientAmount;
                bill.TotalInsuranceAmount = newBill.TotalInsuranceAmount;

                if (bill.BillItems != null)
                {
                    for (int i = 0; i < bill.BillItems.Count; i++)
                    {
                        bill.BillItems[i].Id = itemEntities[i].Id;
                        bill.BillItems[i].BillId = newBill.Id;
                    }
                }

                return bill;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding bill: {ex.Message}");
                return bill;
            }
        }


        public async Task<int> AddPaymentAsync(PaymentDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == dto.BillId);
                if (bill == null)
                    return 0;

                var payment = new Payment
                {
                    BillId = dto.BillId,
                    PaymentDate = dto.PaymentDate == default ? DateTime.UtcNow : dto.PaymentDate,
                    Amount = dto.Amount,
                    PayType = dto.PayType,
                    PaymentStatusId = dto.PaymentStatusId > 0 ? dto.PaymentStatusId : 3, // 3 = Paid
                    InsuranceClaimId = dto.InsuranceClaimId,

                    CashReferenceNumber = dto.CashReferenceNumber,
                    CashReceivedBy = dto.CashReceivedBy,
                    ChequeNumber = dto.ChequeNumber,
                    ChequeBank = dto.ChequeBank,
                    ChequeAccName = dto.ChequeAccName,
                    ChequeAccNumber = dto.ChequeAccNumber,
                    WireReferenceNo = dto.WireReferenceNo,
                    WireTransferDate = dto.WireTransferDate,
                    WireSourceAcc = dto.WireSourceAcc,
                    WireSourceBank = dto.WireSourceBank,
                    CardHolder = dto.CardHolder,
                    CardNumber = dto.CardNumber,
                    CardOfType = dto.CardOfType,
                    MNONumber = dto.MNONumber,
                    MNOReference = dto.MNOReference,
                    MNOOperator = dto.MNOOperator,
                    InsurerSettlementReference = dto.InsurerSettlementReference,
                    SettlementBatchNumber = dto.SettlementBatchNumber
                };

                context.Payments.Add(payment);

                if (dto.PayType == PaymentType.Insurance)
                    bill.InsurancePaidAmount += dto.Amount;
                else
                    bill.PatientPaidAmount += dto.Amount;

                RecalculateBillPaymentStatus(bill);

                await context.SaveChangesAsync();
                return payment.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding payment: {ex.Message}");
                return 0;
            }
        }

        private static void RecalculateBillPaymentStatus(Bill bill)
        {
            decimal totalPaid = bill.PatientPaidAmount + bill.InsurancePaidAmount;

            if (totalPaid >= bill.TotalAmount && bill.TotalAmount > 0m)
                bill.PaymentStatusId = 3; // Paid
            else if (totalPaid > 0m)
                bill.PaymentStatusId = 2; // Partial Payment
            else if (bill.PaymentModeId == 2 && bill.TotalInsuranceAmount > 0m)
                bill.PaymentStatusId = 4; // Insurance Review
            else
                bill.PaymentStatusId = 1; // Unpaid
        }


        /*
        public async Task<InsuranceClaimDto> CreateOrSubmitClaimAsync(
    int billId,
    string staffId,
    string? primaryIcd10Code = null,
    string? folioNumber = null)
        {
            using var context = _contextFactory.CreateDbContext();

            var bill = await context.Bills
                .Include(b => b.BillItems)
                .Include(b => b.PatientInsuarance)
                .Include(b => b.Visit)
                    .ThenInclude(v => v!.VisitInsuranceAuth)
                .Include(b => b.Visit)
                    .ThenInclude(v => v!.Consultations)
                .FirstOrDefaultAsync(b => b.Id == billId)
                ?? throw new InvalidOperationException($"Bill {billId} not found.");

            if (bill.PatientInsuarance == null)
                throw new InvalidOperationException("Cannot submit an insurance claim for a bill with no linked insurance policy.");

            var icdCode = primaryIcd10Code
                ?? bill.Visit?.Consultations.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.Icd10Code))?.Icd10Code;

            var authNumber = bill.Visit?.VisitInsuranceAuth?.AuthorizationNumber;

            var claim = await context.InsuranceClaims.FirstOrDefaultAsync(c => c.BillId == billId);
            if (claim == null)
            {
                claim = new InsuranceClaim
                {
                    BillId = bill.Id,
                    InsuaranceId = bill.PatientInsuarance.InsuaranceId,
                    PatientInsuaranceId = bill.PatientInsuaranceId,
                    CreatedAt = DateTime.UtcNow
                };
                context.InsuranceClaims.Add(claim);
            }

            claim.ClaimedAmount = bill.TotalInsuranceAmount;
            claim.AuthorizationNumber = authNumber;
            claim.PrimaryIcd10Code = icdCode;
            claim.FolioNumber = folioNumber ?? claim.FolioNumber;
            claim.SubmittedByStaffId = staffId;
            claim.SubmittedDate = DateTime.UtcNow;
            claim.Status = ClaimStatus.Submitted;

            foreach (var item in bill.BillItems.Where(i => i.IsInsuranceCovered))
            {
                item.ClaimItemStatus = ClaimStatus.Submitted;
            }

            if (bill.PaymentStatusId == 1)
                bill.PaymentStatusId = 4; // Insurance Review

            await context.SaveChangesAsync();

            return (await GetClaimByBillIdAsync(billId))!;
        }*/

        public async Task<InsuranceClaimDto> CreateOrSubmitClaimAsync(
    int billId,
    string staffId,
    string? primaryIcd10Code = null,
    string? folioNumber = null,
    bool submitImmediately = true)
        {
            using var context = _contextFactory.CreateDbContext();

            var bill = await context.Bills
                .Include(b => b.BillItems)
                .Include(b => b.PatientInsuarance)
                .Include(b => b.Visit)
                    .ThenInclude(v => v!.VisitInsuranceAuth)
                .Include(b => b.Visit)
                    .ThenInclude(v => v!.Consultations)
                .FirstOrDefaultAsync(b => b.Id == billId)
                ?? throw new InvalidOperationException($"Bill {billId} not found.");

            if (bill.PatientInsuarance == null)
                throw new InvalidOperationException("Cannot create an insurance claim for a bill with no linked insurance policy.");

            var icdCode = primaryIcd10Code
                ?? bill.Visit?.Consultations.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.Icd10Code))?.Icd10Code;

            var authNumber = bill.Visit?.VisitInsuranceAuth?.AuthorizationNumber;

            var claim = await context.InsuranceClaims.FirstOrDefaultAsync(c => c.BillId == billId);
            if (claim == null)
            {
                claim = new InsuranceClaim
                {
                    BillId = bill.Id,
                    InsuaranceId = bill.PatientInsuarance.InsuaranceId,
                    PatientInsuaranceId = bill.PatientInsuaranceId,
                    CreatedAt = DateTime.UtcNow
                };
                context.InsuranceClaims.Add(claim);
            }

            claim.ClaimedAmount = bill.TotalInsuranceAmount;
            claim.AuthorizationNumber = authNumber;
            claim.PrimaryIcd10Code = icdCode;
            claim.FolioNumber = folioNumber ?? claim.FolioNumber;
            claim.SubmittedByStaffId = staffId;

            if (submitImmediately)
            {
                claim.SubmittedDate = DateTime.UtcNow;
                claim.Status = ClaimStatus.Submitted;
            }
            else
            {
                claim.Status = ClaimStatus.Draft;
            }

            foreach (var item in bill.BillItems.Where(i => i.IsInsuranceCovered))
            {
                item.ClaimItemStatus = submitImmediately ? ClaimStatus.Submitted : ClaimStatus.Draft;
            }

            if (bill.PaymentStatusId == 1)
                bill.PaymentStatusId = 4; // Insurance Review

            await context.SaveChangesAsync();

            return (await GetClaimByBillIdAsync(billId))!;
        }

        public async Task<List<BillDto>> GetAllBillsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var bills = await context.Bills
                .AsNoTracking()
                .OrderByDescending(b => b.BillDate)
                .Select(bill => new BillDto
                {
                    Id = bill.Id,
                    BillNumber = bill.BillNumber,
                    BillDate = bill.BillDate,
                    BillDescription = bill.BillDescription,

                    // Split Billing & Payment Totals
                    TotalAmount = bill.TotalAmount,
                    TotalPatientAmount = bill.TotalPatientAmount,
                    TotalInsuranceAmount = bill.TotalInsuranceAmount,
                    PatientPaidAmount = bill.PatientPaidAmount,
                    InsurancePaidAmount = bill.InsurancePaidAmount,
                    PaidAmount = bill.PatientPaidAmount + bill.InsurancePaidAmount,

                    // Foreign Keys
                    VisitId = bill.VisitId,
                    BillTypeId = bill.BillTypeId,
                    PaymentModeId = bill.PaymentModeId,
                    PaymentStatusId = bill.PaymentStatusId,
                    PatientInsuaranceId = bill.PatientInsuaranceId,
                    StaffId = bill.StaffId,

                    // UI Display Helpers
                    VisitName = (bill.Visit != null && bill.Visit.VisitType != null)
                        ? bill.Visit.VisitType.TypeName
                        : string.Empty,
                    PatientFullName = (bill.Visit != null && bill.Visit.Patient != null)
                        ? bill.Visit.Patient.PatientFullName
                        : string.Empty,
                    BillTypeName = bill.BillType != null ? bill.BillType.TypeNme : string.Empty,
                    PaymentModeName = bill.PaymentMode != null ? bill.PaymentMode.PaymentModeName : "Cash",
                    PaymentStatusName = bill.PaymentStatus != null ? bill.PaymentStatus.PaymentStatusName : "Unpaid",
                    InsuranceProviderName = (bill.PatientInsuarance != null && bill.PatientInsuarance.Insuarance != null)
                        ? bill.PatientInsuarance.Insuarance.InsuaranceName
                        : string.Empty,
                    //StaffFullName = bill.Staff != null ? bill.Staff.FullName : string.Empty
                })
                .ToListAsync();

            return bills;
        }

        public async Task<List<InsuranceClaimDto>> GetAllInsuranceClaimsAsync(ClaimStatus? status = null)
        {
            using var context = _contextFactory.CreateDbContext();

            var query = context.InsuranceClaims
                .AsNoTracking()
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status.Value);
            }

            return await query
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new InsuranceClaimDto
                {
                    Id = c.Id,
                    ClaimNumber = c.ClaimNumber,
                    ExternalReferenceNumber = c.ExternalReferenceNumber,
                    FolioNumber = c.FolioNumber,
                    AuthorizationNumber = c.AuthorizationNumber,
                    ClaimedAmount = c.ClaimedAmount,
                    ApprovedAmount = c.ApprovedAmount,
                    RejectedAmount = c.RejectedAmount,
                    SettledAmount = c.SettledAmount,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt,
                    SubmittedDate = c.SubmittedDate,
                    AdjudicatedDate = c.AdjudicatedDate,
                    SettledDate = c.SettledDate,
                    PrimaryIcd10Code = c.PrimaryIcd10Code,
                    QueryOrRejectionReason = c.QueryOrRejectionReason,
                    BillId = c.BillId,
                    InsuaranceId = c.InsuaranceId,
                    PatientInsuaranceId = c.PatientInsuaranceId,
                    SubmittedByStaffId = c.SubmittedByStaffId,
                    BillNumber = c.Bill != null ? c.Bill.BillNumber : string.Empty,
                    InsuranceProviderName = c.Insuarance != null ? c.Insuarance.InsuaranceName : string.Empty,
                    PatientFullName = (c.PatientInsuarance != null && c.PatientInsuarance.Patient != null)
                        ? c.PatientInsuarance.Patient.PatientFullName
                        : string.Empty,
                    MemberCardNumber = c.PatientInsuarance != null ? c.PatientInsuarance.MemberCardNumber : string.Empty
                })
                .ToListAsync();
        }


        public async Task<List<InsuaranceDto>> GetAllInsuranceCompaniesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var companies = await context.Insuarances
                .AsNoTracking()
                .Where(ic => ic.IsActive)
                .OrderBy(ic => ic.PrivillageCodeNumber)
                .Select(ic => new InsuaranceDto
                {
                    Id = ic.Id,
                    InsuaranceName = ic.InsuaranceName,
                    InsuranceNumber = ic.InsuranceNumber,
                    PrivillageCodeNumber = ic.PrivillageCodeNumber,
                    IntegrationType = ic.IntegrationType,
                    RequiresPreAuth = ic.RequiresPreAuth,
                    IsActive = ic.IsActive,
                    PortalUrl = ic.PortalUrl,
                    Schemes = ic.Schemes
                        .Where(s => s.IsActive)
                        .Select(s => new InsuranceSchemeDto
                        {
                            Id = s.Id,
                            InsuaranceId = s.InsuaranceId,
                            SchemeName = s.SchemeName,
                            SchemeCode = s.SchemeCode,
                            DefaultCoveragePercent = s.DefaultCoveragePercent,
                            DefaultCopayPercent = s.DefaultCopayPercent,
                            AnnualLimit = s.AnnualLimit,
                            PerVisitLimit = s.PerVisitLimit,
                            IsActive = s.IsActive,
                            InsuranceProviderName = ic.InsuaranceName
                        }).ToList()
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
            using var context = _contextFactory.CreateDbContext();

            return await context.Payments
                .AsNoTracking()
                .OrderByDescending(p => p.PaymentDate)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    BillId = p.BillId,
                    PaymentDate = p.PaymentDate,
                    Amount = p.Amount,
                    PayType = p.PayType,
                    PaymentStatusId = p.PaymentStatusId,
                    InsuranceClaimId = p.InsuranceClaimId,

                    // UI Helpers
                    PaymentStatusName = p.PaymentStatus != null ? p.PaymentStatus.PaymentStatusName : string.Empty,
                    BillNumber = p.Bill != null ? p.Bill.BillNumber : string.Empty,
                    BillTypeName = (p.Bill != null && p.Bill.BillType != null) ? p.Bill.BillType.TypeNme : string.Empty,
                    ConfirmedAmount = p.Amount,

                    // Cash / MNO / Card / Cheque / Wire Details
                    CashReferenceNumber = p.CashReferenceNumber,
                    CashReceivedBy = p.CashReceivedBy,
                    MNONumber = p.MNONumber,
                    MNOReference = p.MNOReference,
                    MNOOperator = p.MNOOperator,
                    CardHolder = p.CardHolder,
                    CardNumber = p.CardNumber,
                    ChequeNumber = p.ChequeNumber,
                    ChequeBank = p.ChequeBank,
                    WireReferenceNo = p.WireReferenceNo,

                    // Insurance Settlement Details
                    InsurerSettlementReference = p.InsurerSettlementReference,
                    SettlementBatchNumber = p.SettlementBatchNumber
                })
                .ToListAsync();
        }

        public async Task<BillDto> GetBillByIdAsync(int billId)
        {
            using var context = _contextFactory.CreateDbContext();

            var bill = await context.Bills
                .AsNoTracking()
                .Where(b => b.Id == billId)
                .Select(b => new BillDto
                {
                    Id = b.Id,
                    BillNumber = b.BillNumber,
                    BillDate = b.BillDate,
                    BillDescription = b.BillDescription,

                    // Split Billing & Payment Totals
                    TotalAmount = b.TotalAmount,
                    TotalPatientAmount = b.TotalPatientAmount,
                    TotalInsuranceAmount = b.TotalInsuranceAmount,
                    PatientPaidAmount = b.PatientPaidAmount,
                    InsurancePaidAmount = b.InsurancePaidAmount,
                    PaidAmount = b.PatientPaidAmount + b.InsurancePaidAmount,

                    // Foreign Keys
                    VisitId = b.VisitId,
                    BillTypeId = b.BillTypeId,
                    PaymentModeId = b.PaymentModeId,
                    PaymentStatusId = b.PaymentStatusId,
                    PatientInsuaranceId = b.PatientInsuaranceId,
                    StaffId = b.StaffId,
                    StaffFullName = b.Staff != null
                                    ? $"{string.Join(" ", b.Staff.StaffTitles!.Select(st => st.StaffTitleName))} {b.Staff.LastName}"
                                    : string.Empty,

                    // UI Display Helpers
                    VisitName = (b.Visit != null && b.Visit.VisitType != null) ? b.Visit.VisitType.TypeName : string.Empty,
                    PatientFullName = (b.Visit != null && b.Visit.Patient != null) ? b.Visit.Patient.PatientFullName : string.Empty,
                    BillTypeName = b.BillType != null ? b.BillType.TypeNme : string.Empty,
                    PaymentModeName = b.PaymentMode != null ? b.PaymentMode.PaymentModeName : "Cash",
                    PaymentStatusName = b.PaymentStatus != null ? b.PaymentStatus.PaymentStatusName : "Unpaid",
                    InsuranceProviderName = (b.PatientInsuarance != null && b.PatientInsuarance.Insuarance != null)
                        ? b.PatientInsuarance.Insuarance.InsuaranceName
                        : string.Empty,
                    //StaffFullName = b.Staff != null ? b.Staff.FullName : string.Empty,

                    // Line Items (with Insurance Coverage & Patient Copay Breakdown)
                    BillItems = b.BillItems.Select(bi => new BillItemDto
                    {
                        Id = bi.Id,
                        BillId = bi.BillId,
                        ItemName = bi.ItemName,
                        SourceType = bi.SourceType,
                        SourceItemId = bi.SourceItemId,
                        InsurerItemCode = bi.InsurerItemCode,
                        Quantity = bi.Quantity,
                        UnitPrice = bi.UnitPrice,
                        IsInsuranceCovered = bi.IsInsuranceCovered,
                        InsuranceCoveragePercent = bi.InsuranceCoveragePercent,
                        InsuranceCoveredAmount = bi.InsuranceCoveredAmount,
                        PatientPayableAmount = bi.PatientPayableAmount,
                        ClaimItemStatus = bi.ClaimItemStatus,
                        RejectionReason = bi.RejectionReason
                    }).ToList(),

                    // All Payments Recorded Against This Bill
                    Payments = b.Payments.Select(p => new PaymentDto
                    {
                        Id = p.Id,
                        BillId = p.BillId,
                        PaymentDate = p.PaymentDate,
                        Amount = p.Amount,
                        PayType = p.PayType,
                        PaymentStatusId = p.PaymentStatusId,
                        InsuranceClaimId = p.InsuranceClaimId,
                        PaymentStatusName = p.PaymentStatus != null ? p.PaymentStatus.PaymentStatusName : string.Empty,
                        CashReferenceNumber = p.CashReferenceNumber,
                        CashReceivedBy = p.CashReceivedBy,
                        ChequeNumber = p.ChequeNumber,
                        ChequeBank = p.ChequeBank,
                        WireReferenceNo = p.WireReferenceNo,
                        CardHolder = p.CardHolder,
                        CardNumber = p.CardNumber,
                        MNONumber = p.MNONumber,
                        MNOReference = p.MNOReference,
                        MNOOperator = p.MNOOperator,
                        InsurerSettlementReference = p.InsurerSettlementReference,
                        SettlementBatchNumber = p.SettlementBatchNumber
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return bill ?? new BillDto();
        }


        public async Task<InsuranceClaimDto?> GetClaimByBillIdAsync(int billId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.InsuranceClaims
                .AsNoTracking()
                .Where(c => c.BillId == billId)
                .Select(c => new InsuranceClaimDto
                {
                    Id = c.Id,
                    ClaimNumber = c.ClaimNumber,
                    ExternalReferenceNumber = c.ExternalReferenceNumber,
                    FolioNumber = c.FolioNumber,
                    AuthorizationNumber = c.AuthorizationNumber,
                    ClaimedAmount = c.ClaimedAmount,
                    ApprovedAmount = c.ApprovedAmount,
                    RejectedAmount = c.RejectedAmount,
                    SettledAmount = c.SettledAmount,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt,
                    SubmittedDate = c.SubmittedDate,
                    AdjudicatedDate = c.AdjudicatedDate,
                    SettledDate = c.SettledDate,
                    PrimaryIcd10Code = c.PrimaryIcd10Code,
                    QueryOrRejectionReason = c.QueryOrRejectionReason,
                    BillId = c.BillId,
                    InsuaranceId = c.InsuaranceId,
                    PatientInsuaranceId = c.PatientInsuaranceId,
                    SubmittedByStaffId = c.SubmittedByStaffId,
                    BillNumber = c.Bill != null ? c.Bill.BillNumber : string.Empty,
                    InsuranceProviderName = c.Insuarance != null ? c.Insuarance.InsuaranceName : string.Empty,
                    PatientFullName = (c.PatientInsuarance != null && c.PatientInsuarance.Patient != null)
                        ? c.PatientInsuarance.Patient.PatientFullName
                        : string.Empty,
                    MemberCardNumber = c.PatientInsuarance != null ? c.PatientInsuarance.MemberCardNumber : string.Empty
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PaymentDto> GetPaymentByIdAsync(int paymentId)
        {
            using var context = _contextFactory.CreateDbContext();
            var payment = await context.Payments
                .AsNoTracking()
                .Where(p => p.Id == paymentId)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    BillId = p.BillId,
                    PaymentDate = p.PaymentDate,
                    Amount = p.Amount,
                    PayType = p.PayType,
                    PaymentStatusId = p.PaymentStatusId,
                    InsuranceClaimId = p.InsuranceClaimId,
                    PaymentStatusName = p.PaymentStatus != null ? p.PaymentStatus.PaymentStatusName : string.Empty,
                    BillNumber = p.Bill != null ? p.Bill.BillNumber : string.Empty,
                    BillTypeName = (p.Bill != null && p.Bill.BillType != null) ? p.Bill.BillType.TypeNme : string.Empty,
                    ConfirmedAmount = p.Amount,
                    CashReferenceNumber = p.CashReferenceNumber,
                    CashReceivedBy = p.CashReceivedBy,
                    ChequeNumber = p.ChequeNumber,
                    ChequeBank = p.ChequeBank,
                    ChequeAccName = p.ChequeAccName,
                    ChequeAccNumber = p.ChequeAccNumber,
                    WireReferenceNo = p.WireReferenceNo,
                    WireTransferDate = p.WireTransferDate,
                    WireSourceAcc = p.WireSourceAcc,
                    WireSourceBank = p.WireSourceBank,
                    CardHolder = p.CardHolder,
                    CardNumber = p.CardNumber,
                    CardOfType = p.CardOfType,
                    MNONumber = p.MNONumber,
                    MNOReference = p.MNOReference,
                    MNOOperator = p.MNOOperator,
                    InsurerSettlementReference = p.InsurerSettlementReference,
                    SettlementBatchNumber = p.SettlementBatchNumber
                })
                .FirstOrDefaultAsync();

            return payment ?? new PaymentDto();
        }


        public async Task<int> RecordInsurerSettlementAsync(
    int claimId,
    decimal settledAmount,
    string settlementReference,
    string? batchNumber = null)
        {
            using var context = _contextFactory.CreateDbContext();
            var claim = await context.InsuranceClaims.FirstOrDefaultAsync(c => c.Id == claimId);
            if (claim == null)
                return 0;

            claim.SettledAmount += settledAmount;
            claim.SettledDate = DateTime.UtcNow;
            claim.Status = ClaimStatus.Settled;

            await context.SaveChangesAsync();

            return await AddPaymentAsync(new PaymentDto
            {
                BillId = claim.BillId,
                PaymentDate = DateTime.UtcNow,
                Amount = settledAmount,
                PayType = PaymentType.Insurance,
                PaymentStatusId = 3,
                InsuranceClaimId = claim.Id,
                InsurerSettlementReference = settlementReference,
                SettlementBatchNumber = batchNumber
            });
        }

        public async Task<InsurerTariffDto> SaveInsurerTariffAsync(InsurerTariffDto dto)
        {
            using var context = _contextFactory.CreateDbContext();

            InsurerTariff? entity = dto.Id > 0
                ? await context.InsurerTariffs.FirstOrDefaultAsync(t => t.Id == dto.Id)
                : await context.InsurerTariffs.FirstOrDefaultAsync(t =>
                    t.InsuaranceId == dto.InsuaranceId &&
                    t.InsuranceSchemeId == dto.InsuranceSchemeId &&
                    t.ServiceType == dto.ServiceType &&
                    t.ServiceItemId == dto.ServiceItemId);

            if (entity == null)
            {
                entity = new InsurerTariff
                {
                    InsuaranceId = dto.InsuaranceId,
                    InsuranceSchemeId = dto.InsuranceSchemeId,
                    ServiceType = dto.ServiceType,
                    ServiceItemId = dto.ServiceItemId
                };
                context.InsurerTariffs.Add(entity);
            }

            entity.InsurerItemCode = dto.InsurerItemCode;
            entity.InsurerItemName = dto.InsurerItemName;
            entity.AgreedPrice = dto.AgreedPrice;
            entity.CoveragePercent = dto.CoveragePercent;
            entity.IsCovered = dto.IsCovered;
            entity.RequiresPreApproval = dto.RequiresPreApproval;

            await context.SaveChangesAsync();
            dto.Id = entity.Id;
            return dto;
        }


        public async Task<bool> UpdateBillAsync(BillDto dto, int billId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var extBill = await context.Bills
                    .Include(b => b.BillItems)
                    .FirstOrDefaultAsync(x => x.Id == billId);

                if (extBill == null)
                    return false;

                extBill.BillDescription = dto.BillDescription;
                extBill.VisitId = dto.VisitId;
                extBill.BillTypeId = dto.BillTypeId;

                if (dto.PaymentModeId > 0)
                {
                    extBill.PaymentModeId = dto.PaymentModeId;
                    extBill.PatientInsuaranceId = extBill.PaymentModeId == 2 ? dto.PatientInsuaranceId : null;
                }

                if (dto.BillItems != null)
                {
                    // Add any newly added items (Id == 0) with automatic tariff/coverage calculation
                    foreach (var biDto in dto.BillItems.Where(i => i.Id == 0))
                    {
                        //await CalculateItemInternalAsync(context, biDto, extBill.PaymentModeId, extBill.PatientInsuaranceId);
                        await CalculateItemInternalAsync(biDto, extBill.PaymentModeId, extBill.PatientInsuaranceId);

                        extBill.BillItems.Add(new BillItem
                        {
                            ItemName = biDto.ItemName,
                            SourceType = biDto.SourceType,
                            SourceItemId = biDto.SourceItemId,
                            InsurerItemCode = biDto.InsurerItemCode,
                            Quantity = biDto.Quantity,
                            UnitPrice = biDto.UnitPrice,
                            IsInsuranceCovered = biDto.IsInsuranceCovered,
                            InsuranceCoveragePercent = biDto.InsuranceCoveragePercent,
                            InsuranceCoveredAmount = biDto.InsuranceCoveredAmount,
                            PatientPayableAmount = biDto.PatientPayableAmount,
                            ClaimItemStatus = biDto.ClaimItemStatus,
                            RejectionReason = biDto.RejectionReason,
                            BillId = extBill.Id
                        });
                    }
                }

                // Re-sum bill totals across all items
                extBill.TotalAmount = extBill.BillItems.Sum(i => i.TotalAmount);
                extBill.TotalInsuranceAmount = extBill.BillItems.Sum(i => i.InsuranceCoveredAmount);
                extBill.TotalPatientAmount = extBill.BillItems.Sum(i => i.PatientPayableAmount);

                RecalculateBillPaymentStatus(extBill);

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating bill: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdateClaimAdjudicationAsync(
            int claimId,
            ClaimStatus status,
            decimal approvedAmount,
            decimal rejectedAmount,
            string? reason = null)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var claim = await context.InsuranceClaims.FirstOrDefaultAsync(c => c.Id == claimId);
                if (claim == null)
                    return false;

                claim.Status = status;
                claim.ApprovedAmount = approvedAmount;
                claim.RejectedAmount = rejectedAmount;
                claim.QueryOrRejectionReason = reason;
                claim.AdjudicatedDate = DateTime.UtcNow;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating claim adjudication: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdatePaymentAsync(PaymentDto dto, int paymentId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var extPayment = await context.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
                if (extPayment == null)
                    return false;

                var bill = await context.Bills.FirstOrDefaultAsync(b => b.Id == extPayment.BillId);

                // Adjust parent Bill paid totals by any change in payment amount
                if (bill != null)
                {
                    decimal diff = dto.Amount - extPayment.Amount;
                    if (extPayment.PayType == PaymentType.Insurance)
                        bill.InsurancePaidAmount += diff;
                    else
                        bill.PatientPaidAmount += diff;

                    RecalculateBillPaymentStatus(bill);
                }

                extPayment.Amount = dto.Amount;
                extPayment.PaymentDate = dto.PaymentDate;
                extPayment.PayType = dto.PayType;
                extPayment.PaymentStatusId = dto.PaymentStatusId;
                extPayment.CashReferenceNumber = dto.CashReferenceNumber;
                extPayment.CashReceivedBy = dto.CashReceivedBy;
                extPayment.ChequeNumber = dto.ChequeNumber;
                extPayment.ChequeBank = dto.ChequeBank;
                extPayment.ChequeAccName = dto.ChequeAccName;
                extPayment.ChequeAccNumber = dto.ChequeAccNumber;
                extPayment.WireReferenceNo = dto.WireReferenceNo;
                extPayment.WireTransferDate = dto.WireTransferDate;
                extPayment.WireSourceAcc = dto.WireSourceAcc;
                extPayment.WireSourceBank = dto.WireSourceBank;
                extPayment.CardHolder = dto.CardHolder;
                extPayment.CardNumber = dto.CardNumber;
                extPayment.CardOfType = dto.CardOfType;
                extPayment.MNONumber = dto.MNONumber;
                extPayment.MNOReference = dto.MNOReference;
                extPayment.MNOOperator = dto.MNOOperator;
                extPayment.InsurerSettlementReference = dto.InsurerSettlementReference;
                extPayment.SettlementBatchNumber = dto.SettlementBatchNumber;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating payment: {ex}");
                return false;
            }
        }

        public async Task<List<InsuranceSchemeDto>> GetSchemesByInsuranceIdAsync(int insuaranceId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.InsuranceSchemes
                .AsNoTracking()
                .Where(s => s.InsuaranceId == insuaranceId && s.IsActive)
                .Select(s => new InsuranceSchemeDto
                {
                    Id = s.Id,
                    InsuaranceId = s.InsuaranceId,
                    SchemeName = s.SchemeName,
                    SchemeCode = s.SchemeCode,
                    DefaultCoveragePercent = s.DefaultCoveragePercent,
                    DefaultCopayPercent = s.DefaultCopayPercent,
                    AnnualLimit = s.AnnualLimit,
                    PerVisitLimit = s.PerVisitLimit,
                    IsActive = s.IsActive,
                    InsuranceProviderName = s.Insuarance != null ? s.Insuarance.InsuaranceName : string.Empty
                })
                .ToListAsync();
        }

        public async Task<List<InsurerTariffDto>> GetTariffsByInsuranceAsync(int insuaranceId, int? schemeId = null)
        {
            using var context = _contextFactory.CreateDbContext();
            var query = context.InsurerTariffs
                .AsNoTracking()
                .Where(t => t.InsuaranceId == insuaranceId);

            if (schemeId.HasValue)
                query = query.Where(t => t.InsuranceSchemeId == schemeId || t.InsuranceSchemeId == null);

            return await query
                .Select(t => new InsurerTariffDto
                {
                    Id = t.Id,
                    ServiceType = t.ServiceType,
                    ServiceItemId = t.ServiceItemId,
                    InsurerItemCode = t.InsurerItemCode,
                    InsurerItemName = t.InsurerItemName,
                    AgreedPrice = t.AgreedPrice,
                    CoveragePercent = t.CoveragePercent,
                    IsCovered = t.IsCovered,
                    RequiresPreApproval = t.RequiresPreApproval,
                    InsuaranceId = t.InsuaranceId,
                    InsuranceSchemeId = t.InsuranceSchemeId,
                    InsuranceProviderName = t.Insuarance != null ? t.Insuarance.InsuaranceName : string.Empty,
                    InsuranceSchemeName = t.InsuranceScheme != null ? t.InsuranceScheme.SchemeName : "All Schemes"
                })
                .ToListAsync();
        }

        public async Task<int> AppendServiceToVisitBillAsync(int visitId, BillItemDto item, string staffId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                // Look for an existing open Interim Bill (BillTypeId = 2) for this visit
                var openBill = await context.Bills
                    .Include(b => b.BillItems)
                    .Where(b => b.VisitId == visitId && b.BillTypeId == 2 && b.PaymentStatusId != 3)
                    .OrderByDescending(b => b.BillDate)
                    .FirstOrDefaultAsync();

                if (openBill != null)
                {
                    //await CalculateItemInternalAsync(context, item, openBill.PaymentModeId, openBill.PatientInsuaranceId);
                    await CalculateItemInternalAsync(item, openBill.PaymentModeId, openBill.PatientInsuaranceId);

                    var newBillItem = new BillItem
                    {
                        BillId = openBill.Id,
                        ItemName = item.ItemName,
                        SourceType = item.SourceType,
                        SourceItemId = item.SourceItemId,
                        InsurerItemCode = item.InsurerItemCode,
                        Quantity = item.Quantity > 0 ? item.Quantity : 1,
                        UnitPrice = item.UnitPrice,
                        IsInsuranceCovered = item.IsInsuranceCovered,
                        InsuranceCoveragePercent = item.InsuranceCoveragePercent,
                        InsuranceCoveredAmount = item.InsuranceCoveredAmount,
                        PatientPayableAmount = item.PatientPayableAmount,
                        ClaimItemStatus = item.ClaimItemStatus
                    };

                    openBill.BillItems.Add(newBillItem);

                    openBill.TotalAmount = openBill.BillItems.Sum(i => i.TotalAmount);
                    openBill.TotalInsuranceAmount = openBill.BillItems.Sum(i => i.InsuranceCoveredAmount);
                    openBill.TotalPatientAmount = openBill.BillItems.Sum(i => i.PatientPayableAmount);

                    RecalculateBillPaymentStatus(openBill);

                    await context.SaveChangesAsync();
                    return newBillItem.Id;
                }
                else
                {
                    // No open Interim Bill yet -> create one via AddBillAsync
                    var created = await AddBillAsync(new BillDto
                    {
                        BillDate = DateTime.UtcNow,
                        BillNumber = $"NCMH-BN-{DateTime.UtcNow:yyMMddHHmmss}",
                        BillDescription = $"Visit #{visitId} Services",
                        VisitId = visitId,
                        BillTypeId = 2, // Interim Bill
                        StaffId = staffId,
                        BillItems = new List<BillItemDto> { item }
                    });

                    return created.BillItems.FirstOrDefault()?.Id ?? 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error appending service to visit bill: {ex.Message}");
                return 0;
            }
        }

    }
}
