using NovaCare.Shared.DTOs.DiseaseDTOs;

namespace NovaCare.Interfaces
{
    public interface IDiseaseService
    {
        Task<bool> AddDiseaseAsync(DiseaseDto roomDto);
        Task<bool> UpdateDiseaseAsync(DiseaseDto roomDto, int romId);
        Task<List<DiseaseDto>> GetAllDiseasesAsync();
        Task<List<DiseaseTypeDto>> GetAllDiseaseTypesAsync();
        Task<DiseaseDto> GetDiseaseByIdAsync(int diseaseId);
        Task<DiseaseDto> GetDiseaseByNameAsync(string diseaseName);
    }
}
