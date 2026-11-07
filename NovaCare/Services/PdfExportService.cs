using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.CodeAnalysis;
using MudBlazor;
using NovaCare.Interfaces;
using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using static System.Net.Mime.MediaTypeNames;

namespace NovaCare.Services
{
    public class PdfExportService : IPdfExportService
    {
        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PdfExportService(IConverter converter, IWebHostEnvironment webHostEnvironment)
        {
            _converter = converter;
            _webHostEnvironment = webHostEnvironment;
        }

        public byte[] CreatePdf(string htmlContent, string? footerName = null)
        {
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = new WebSettings { DefaultEncoding = "utf-8", EnableIntelligentShrinking = false, PrintMediaType = true, EnableJavascript = false, LoadImages = true },
                FooterSettings = new FooterSettings { FontName = "Arial", FontSize = 9, Line = false, Right = "Page [page] of [toPage]", Spacing = 0 }
            };

            if (!string.IsNullOrEmpty(footerName))
            {
                objectSettings.FooterSettings.HtmUrl = footerName;
            }

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                        ColorMode = ColorMode.Color,
                        Orientation = DinkToPdf.Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings { Top = 5, Bottom = 10, Left = 5, Right = 5 }
                    },
                Objects = { objectSettings }
            };

            var result = _converter.Convert(doc);
            return result;
        }

        public string GenerateStaffPreview<T>(ReportPreviewDto<T> dto, StaffDto? currentUser)
        {
            try
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/logo2.png");
                string footerHtmlPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.html");
                string imgSrc = "";

                if (File.Exists(imagePath))
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    imgSrc = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                }

                string footerContent = $@" 
                            <!DOCTYPE html> 
                            <html> 
                            <head> 
                                <meta charset='UTF-8'> 
                                <style> 
                                    body {{ 
                                        margin: 0; 
                                        padding: 15px 20px;
                                        text-align: center; 
                                        font-family: 'Helvetica Neue', Arial, sans-serif; 
                                        font-size: 9pt; 
                                        color: #64748b;
                                        border-top: 1px solid #e2e8f0;
                                    }} 
                                    .footer-container {{
                                        display: flex;
                                        align-items: center;
                                        justify-content: center;
                                        gap: 8px;
                                    }}
                                    img {{ 
                                        height: 20px; 
                                        vertical-align: middle; 
                                    }} 
                                    .hospital-name {{
                                        font-weight: 600;
                                        color: #1e293b;
                                    }}
                                </style> 
                            </head> 
                            <body> 
                                <div class='footer-container'> 
                                    <img src='{imgSrc}' alt='Hospital Logo' /> 
                                    <span>&copy; {DateTime.Now.Year}</span> 
                                    <span class='hospital-name'>NovaCare Medical Hospital</span> 
                                </div>
                            </body> 
                            </html>";


                File.WriteAllText(footerHtmlPath, footerContent, System.Text.Encoding.UTF8);
                var Dto= dto;
                string html = DesignHtml(Dto, imgSrc, currentUser);

                byte[] pdfBytes = CreatePdf(html, footerHtmlPath);

                if (File.Exists(footerHtmlPath)) File.Delete(footerHtmlPath);
                var pdfBase64 = Convert.ToBase64String(pdfBytes);
                return pdfBase64;
            }
            catch (Exception ex)
            {
                var editMessage = $"PDF Generation Error: {ex.Message}";
                Console.WriteLine(editMessage);
                return string.Empty;
            }
        }

        public string DesignHtml<T>(ReportPreviewDto<T> dto, string imgSrc, StaffDto? currentUser)
        {
            var html = new System.Text.StringBuilder();

            string userTitle = currentUser?.ClinicalTitleName ?? "";
            string userFirst = currentUser?.FisrtName ?? ""; // Kept your property typo 'FisrtName'
            string userMiddle = !string.IsNullOrEmpty(currentUser?.MiddleName) ? $"{currentUser.MiddleName.Substring(0, 1).ToUpper()}." : "";
            string userLast = currentUser?.LastName ?? "";
            string preparedBy = $"{userTitle} {userFirst} {userMiddle} {userLast}".Trim();

            html.Append($@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                            font-size: 14px;
                            color: #334155;
                            margin: 0;
                            padding: 0;
                            background-color: #f8fafc;
                        }}
                        .report-container {{
                            max-width: 1000px;
                            margin: 15px auto;
                            padding: 20px 25px;
                            background: #ffffff;
                            border-radius: 6px;
                            box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.05);
                        }}
                        /* Compact Horizontal Header Layout */
                        .header {{
                            display: flex;
                            align-items: center;
                            justify-content: space-between;
                            padding-bottom: 15px;
                            border-bottom: 2px solid #f1f5f9;
                            margin-bottom: 15px;
                        }}
                        .brand-section {{
                            display: flex;
                            align-items: center;
                            gap: 15px;
                        }}
                        .brand-section img {{
                            height: 50px; /* Reduced height */
                            object-fit: contain;
                        }}
                        .hospital-title {{
                            color: #1e3a8a;
                            margin: 0;
                            font-size: 20px;
                            font-weight: 700;
                            letter-spacing: -0.02em;
                        }}
                        .report-details {{
                            text-align: right;
                        }}
                        .report-details h3 {{
                            color: #475569;
                            margin: 0 0 4px 0;
                            font-size: 14px;
                            font-weight: 600;
                        }}
                        .meta-info {{
                            font-size: 12px;
                            color: #64748b;
                        }}
                        .table-wrapper {{
                            margin-top: 10px;
                            overflow-x: auto;
                        }}
                        .table-border {{
                            width: 100%;
                            border-collapse: collapse;
                            text-align: left;
                        }}
                        .table-border th {{
                            background-color: #1e3a8a;
                            color: #ffffff;
                            font-weight: 600;
                            padding: 10px 14px;
                            font-size: 12px;
                            text-transform: uppercase;
                            letter-spacing: 0.05em;
                        }}
                        .table-border td {{
                            padding: 10px 14px;
                            border-bottom: 1px solid #e2e8f0;
                            color: #475569;
                            font-size: 13px;
                        }}
                        .table-border tbody tr:nth-child(even) {{
                            background-color: #f8fafc;
                        }}
                        .table-border tbody tr:hover {{
                            background-color: #f1f5f9;
                        }}
                        .sn-col {{
                            width: 45px;
                            font-weight: 600;
                            color: #94a3b8;
                        }}
                    </style>
                </head>
                <body>
                    <div class='report-container'>
                        <div class='header'>
                            <div class='brand-section'>
                                <img src='{imgSrc}' />
                                <h2 class='hospital-title'>NovaCare Medical Hospital</h2>
                            </div>
                            <div class='report-details'>
                                <h3>Staff Report: {dto.FromDate?.ToShortDateString()} – {dto.ToDate?.ToShortDateString()}</h3>
                                <span class='meta-info'>Prepared By: <strong>{preparedBy}</strong></span>
                            </div>
                        </div>
                        <div class='table-wrapper'>
                            <table class='table-border'>
                                <thead>
                                    <tr>");

                            if (dto.Data != null)
                            {
                                if (dto.Data is IEnumerable<StaffDto>)
                                { html.Append("<th class='sn-col'>Sn.</th><th>Full Name</th><th>Employee Id</th><th>Title</th><th>Availability</th>"); }
                                else if (dto.Data is IEnumerable<PatientDto>)
                                { html.Append("<th class='sn-col'>Sn.</th><th>Full Name</th><th>Patient Type</th><th>Phone Number</th>"); }
                                else if (dto.Data is IEnumerable<LabTestDto>)
                                { html.Append("<th class='sn-col'>Sn.</th><th>Disease Name</th><th>Patient Full Name</th><th>Date</th>"); }
                                else if (dto.Data is IEnumerable<MedicineDto>)
                                { html.Append("<th class='sn-col'>Sn.</th><th>Medicine Name</th><th>Quantity</th><th>Price</th>"); }
                            }

                            html.Append($@" </tr>
                                </thead>
                                <tbody>");

                            if (dto.Data != null)
                            {
                                int sn = 1;
                                if (dto.Data is IEnumerable<StaffDto> staffList)
                                {
                                    foreach (StaffDto staff in staffList)
                                    {
                                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td><strong>{staff.FisrtName} {staff.MiddleName} {staff.LastName}</strong></td><td>{staff.EmployeeId}</td><td>{staff.AdministrativeTitleName} / {staff.ClinicalTitleName}</td><td>{staff.StaffStatusName}</td></tr>");
                                    }
                                }
                                else if (dto.Data is IEnumerable<PatientDto> patientList)
                                {
                                    foreach (PatientDto patient in patientList)
                                    {
                                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td><strong>{patient.FisrtName} {patient.MiddleName} {patient.LastName}</strong></td><td>{patient.PatientTypeName}</td><td>{patient.PhoneNumber}</td></tr>");
                                    }
                                }
                                else if (dto.Data is IEnumerable<LabTestDto> labList)
                                {
                                    foreach (LabTestDto lab in labList)
                                    {
                                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td>{lab.DiseaseName}</td><td><strong>{lab.PatientFullName}</strong></td><td>{lab.CreatedDate:yyyy-MM-dd}</td></tr>");
                                    }
                                }
                                else if (dto.Data is IEnumerable<MedicineDto> medList)
                                {
                                    foreach (MedicineDto med in medList)
                                    {
                                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td>{med.MedicineName}</td><td>{med.MedicineQty}</td><td>${med.SellingPrice:N2}</td></tr>");
                                    }
                                }
                            }

                            html.AppendLine("</tbody></table></div></div></body></html>");


            return html.ToString();
        }


    }
}
