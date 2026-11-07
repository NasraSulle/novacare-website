using Microsoft.EntityFrameworkCore;
using NovaCare.Data;
using NovaCare.Interfaces;
using NovaCare.Modals.LabTestModals;
using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;

namespace NovaCare.Services
{
    public class LabTestService : ILabTestService
    {
        private readonly IDbContextFactory<NovaCareDbContext> _contextFactory;
        public LabTestService(IDbContextFactory<NovaCareDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> AddLabTestAsync(LabTestDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var labTest = new LabTest
                {
                    DiseaseName = dto.DiseaseName,
                    LabTestStatusname = "New",
                    TestResult = dto.TestResult,
                    ResultAttachment = dto.ResultAttachment,
                    CreatedDate = dto.CreatedDate,
                    //PatientId = dto.PatientId,
                    ConsultationId = dto.ConsultationId,
                    LabTestCost = dto.LabTestCost,
                    RequesteeDoctor = dto.RequesteeDoctor,
                    UpdaterLabTechnician = dto.UpdaterLabTechnician
                };

                context.LabTests.Add(labTest);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return false;
            }
        }

        public async Task<List<LabTestDto>> GetAllLabTestsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var labTests = await context.LabTests
                .AsNoTracking()
                .Select(labTest => new LabTestDto
                {
                    Id = labTest.Id,
                    DiseaseName = labTest.DiseaseName,
                    LabTestStatusName = labTest.LabTestStatusname,
                    TestResult = labTest.TestResult,
                    LabTestCost = labTest.LabTestCost,
                    ResultAttachment = labTest.ResultAttachment,
                    CreatedDate = labTest.CreatedDate,
                    //PatientId = labTest.PatientId,
                    //PatientFullName = $" {labTest.Patient!.FisrtName} {labTest.Patient!.MiddleName} {labTest.Patient!.LastName}",
                    ConsultationId = labTest.ConsultationId,
                    RequesteeDoctor = labTest.RequesteeDoctor,
                    UpdaterLabTechnician = labTest.UpdaterLabTechnician
                    })
                .ToListAsync();

            return labTests;
        }

        public async Task<ReportPreviewDto<LabTestDto>> GetAllLabTestsAsync(DateTime? fromDate, DateTime? toDate)
        {
            using var context = _contextFactory.CreateDbContext();
            var labTests = await context.LabTests
                .Where(x => x.CreatedDate >= fromDate && x.CreatedDate <= toDate)
                .AsNoTracking()
                .Select(labTest => new LabTestDto
                {
                    Id = labTest.Id,
                    DiseaseName = labTest.DiseaseName,
                    LabTestStatusName = labTest.LabTestStatusname,
                    TestResult = labTest.TestResult,
                    LabTestCost = labTest.LabTestCost,
                    ResultAttachment = labTest.ResultAttachment,
                    CreatedDate = labTest.CreatedDate,
                    //PatientId = labTest.PatientId,
                    //PatientFullName = $" {labTest.Patient!.FisrtName} {labTest.Patient!.MiddleName} {labTest.Patient!.LastName}",
                    ConsultationId = labTest.ConsultationId,
                    RequesteeDoctor = labTest.RequesteeDoctor,
                    UpdaterLabTechnician = labTest.UpdaterLabTechnician
                })
                .ToListAsync();

            var result = new ReportPreviewDto<LabTestDto>
            {
                FromDate = fromDate,
                ToDate = toDate,
                ReportType = "LabTest",
                Data = labTests
            };
            return result;
        }

        public async Task<bool> UpdateLabTestAsync(LabTestDto dto, int labTestId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var existingLabTest = await context.LabTests.FirstOrDefaultAsync(x => x.Id == labTestId);

                if (existingLabTest != null)
                {
                    existingLabTest.DiseaseName = dto.DiseaseName;
                    existingLabTest.TestResult = dto.TestResult;
                    existingLabTest.LabTestStatusname = string.IsNullOrWhiteSpace(existingLabTest.TestResult) ? "New" : "Updated";
                    existingLabTest.ResultAttachment = dto.ResultAttachment;
                    existingLabTest.RequesteeDoctor = dto.RequesteeDoctor;
                    existingLabTest.UpdaterLabTechnician = dto.UpdaterLabTechnician;
                    existingLabTest.LabTestCost = existingLabTest.LabTestCost;
                    existingLabTest.LabTestResultCompositions = existingLabTest.LabTestResultCompositions;

                    context.LabTests.Update(existingLabTest);
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

        //====================================

        public async Task<bool> AddLabTestResultCompositionAsync(LabTestResultCompositionDto dto)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var labTestComp = new Modals.LabTestModals.LabTestResultComposition
                {
                    TestComposiiton = dto.TestComposiiton,
                    Value = dto.Value,
                    Unit = dto.Unit,
                    LabTestId = dto.LabTestId
                };

                context.LabTestResultCompositions.Add(labTestComp);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return false;
            }
        }

        public async Task<List<LabTestResultCompositionDto>> GetAllLabTestResultCompositionsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            var labTestComps = await context.LabTestResultCompositions
                .AsNoTracking()
                .Select(labTest => new LabTestResultCompositionDto
                {
                    Id = labTest.Id,
                    TestComposiiton = labTest.TestComposiiton,
                    Value = labTest.Value,
                    Unit = labTest.Unit,
                    LabTestId = labTest.LabTestId
                })
                .ToListAsync();

            return labTestComps;
        }

        
        public async Task<bool> UpdateLabTestResultCompositionAsync(LabTestResultCompositionDto dto, int compositionId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var existingLabTestComp = await context.LabTestResultCompositions.FirstOrDefaultAsync(x => x.Id == compositionId);

                if (existingLabTestComp != null)
                {
                    existingLabTestComp.TestComposiiton = dto.TestComposiiton;
                    existingLabTestComp.Value = dto.Value;
                    existingLabTestComp.Unit = dto.Unit;
                    existingLabTestComp.LabTestId = dto.LabTestId;

                    context.LabTestResultCompositions.Update(existingLabTestComp);
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

        public async Task<LabTestDto> GetLabTestByIdAsync(int testId)
        {
            using var context = _contextFactory.CreateDbContext();
            var labTests = await context.LabTests
                .AsNoTracking()
                .Select(labTest => new LabTestDto
                {
                    Id = labTest.Id,
                    DiseaseName = labTest.DiseaseName,
                    LabTestStatusName = labTest.LabTestStatusname,
                    TestResult = labTest.TestResult,
                    LabTestCost = labTest.LabTestCost,
                    ResultAttachment = labTest.ResultAttachment,
                    CreatedDate = labTest.CreatedDate,
                    //PatientId = labTest.PatientId,
                    //PatientFullName = $" {labTest.Patient!.FisrtName} {labTest.Patient!.MiddleName} {labTest.Patient!.LastName}",
                    ConsultationId = labTest.ConsultationId,
                    RequesteeDoctor = labTest.RequesteeDoctor,
                    UpdaterLabTechnician = labTest.UpdaterLabTechnician,
                    LabTestResultCompositions = labTest.LabTestResultCompositions
                                .Select(x => new LabTestResultCompositionDto
                                {
                                    TestComposiiton = x.TestComposiiton,
                                    Value = x.Value,
                                    Unit = x.Unit,
                                }).ToList()
                })
                .FirstOrDefaultAsync(x => x.Id == testId);

            return labTests;
        }
    }
}
