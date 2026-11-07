namespace NovaCare.Shared.DTOs.ReportDTOs
{
    public class ReportPreviewDto<T>
    {
        public string ReportType { get; set; } = string.Empty;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public List<T>? Data { get; set; }
    }
}
