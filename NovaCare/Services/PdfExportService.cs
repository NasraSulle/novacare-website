using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.Options;
using NovaCare.Interfaces;
using NovaCare.Shared.DTOs.LabTestDTOs;
using NovaCare.Shared.DTOs.MedicineDTOs;
using NovaCare.Shared.DTOs.PatientDTOs;
using NovaCare.Shared.DTOs.ReportDTOs;
using NovaCare.Shared.DTOs.StaffDTOs;
using NovaCare.Shared.HospitalCredentials;

namespace NovaCare.Services
{
    public class PdfExportService : IPdfExportService
    {
        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly HospitalInfoSettings _hospitalInfo;

        public PdfExportService(
            IConverter converter,
            IWebHostEnvironment webHostEnvironment,
            IOptions<HospitalInfoSettings> hospitalInfoOptions)
        {
            _converter = converter;
            _webHostEnvironment = webHostEnvironment;
            _hospitalInfo = hospitalInfoOptions.Value;
        }

        public byte[] CreatePdf(string htmlContent, string? footerName = null)
        {
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = new WebSettings
                {
                    DefaultEncoding = "utf-8",
                    EnableIntelligentShrinking = false,
                    PrintMediaType = true,
                    EnableJavascript = false,
                    LoadImages = true
                },
                FooterSettings = new FooterSettings
                {
                    FontName = "Arial",
                    FontSize = 9,
                    Line = false,
                    Right = "Page [page] of [toPage]",
                    Spacing = 0
                }
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
                    Margins = new MarginSettings { Top = 8, Bottom = 14, Left = 8, Right = 8 }
                },
                Objects = { objectSettings }
            };

            return _converter.Convert(doc);
        }

        public string GenerateStaffPreview<T>(ReportPreviewDto<T> dto, StaffDto? currentUser)
        {
            try
            {
                var logoPath = string.IsNullOrWhiteSpace(_hospitalInfo.LogoPath)
                    ? "Images/logo2.png"
                    : _hospitalInfo.LogoPath;

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, logoPath);
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
                                padding: 10px 20px;
                                text-align: center;
                                font-family: 'Helvetica Neue', Arial, sans-serif;
                                font-size: 8.5pt;
                                color: #64748b;
                                border-top: 1px solid #cbd5e1;
                            }}
                            .footer-container {{
                                display: -webkit-box;
                                display: flex;
                                -webkit-box-align: center;
                                align-items: center;
                                -webkit-box-pack: center;
                                justify-content: center;
                                gap: 8px;
                            }}
                            img {{
                                height: 18px;
                                vertical-align: middle;
                                margin-right: 6px;
                            }}
                            .hospital-name {{
                                font-weight: 600;
                                color: #1e293b;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='footer-container'>
                            {(string.IsNullOrEmpty(imgSrc) ? "" : $"<img src='{imgSrc}' alt='Hospital Logo' />")}
                            <span>{_hospitalInfo.FooterCopyright} ({DateTime.Now.Year}) &mdash; </span>
                            <span class='hospital-name'>{_hospitalInfo.Name}</span>
                        </div>
                    </body>
                    </html>";

                File.WriteAllText(footerHtmlPath, footerContent, System.Text.Encoding.UTF8);
                string html = DesignHtml(dto, imgSrc, currentUser);

                byte[] pdfBytes = CreatePdf(html, footerHtmlPath);

                if (File.Exists(footerHtmlPath)) File.Delete(footerHtmlPath);
                return Convert.ToBase64String(pdfBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PDF Generation Error: {ex.Message}");
                return string.Empty;
            }
        }

        public string DesignHtml<T>(ReportPreviewDto<T> dto, string imgSrc, StaffDto? currentUser)
        {
            var html = new System.Text.StringBuilder();

            string userTitle = currentUser?.ClinicalTitleName ?? "";
            string userFirst = currentUser?.FisrtName ?? "";
            string userMiddle = !string.IsNullOrEmpty(currentUser?.MiddleName) ? $"{currentUser.MiddleName.Substring(0, 1).ToUpper()}." : "";
            string userLast = currentUser?.LastName ?? "";
            string preparedBy = $"{userTitle} {userFirst} {userMiddle} {userLast}".Trim();
            if (string.IsNullOrWhiteSpace(preparedBy)) preparedBy = "System Administrator";

            bool isInsuranceClaim = dto.Data is IEnumerable<BillItemDto>;

            string reportTitle = !string.IsNullOrWhiteSpace(dto.ReportType)
                ? dto.ReportType.ToUpper()
                : (isInsuranceClaim ? "INSURANCE CLAIM & BILLING STATEMENT" : "OFFICIAL HOSPITAL REPORT");

            string datePeriod = (dto.FromDate.HasValue && dto.ToDate.HasValue)
                ? $"{dto.FromDate.Value:dd MMM yyyy} – {dto.ToDate.Value:dd MMM yyyy}"
                : DateTime.Now.ToString("dd MMM yyyy");

            string docRefNumber = $"NC-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";

            html.Append($@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                            font-size: 13px;
                            color: #334155;
                            margin: 0;
                            padding: 0;
                            background-color: #ffffff;
                        }}
                        .report-container {{
                            width: 100%;
                            margin: 0 auto;
                            padding: 10px 15px;
                            box-sizing: border-box;
                            background: #ffffff;
                        }}
                        /* Official Hospital Letterhead */
                        .letterhead {{
                            display: -webkit-box;
                            display: flex;
                            -webkit-box-pack: justify;
                            justify-content: space-between;
                            -webkit-box-align: center;
                            align-items: center;
                            padding-bottom: 14px;
                            border-bottom: 3px solid #1e3a8a;
                        }}
                        .brand-left {{
                            display: -webkit-box;
                            display: flex;
                            -webkit-box-align: center;
                            align-items: center;
                            width: 62%;
                        }}
                        .brand-left img {{
                            height: 68px;
                            width: auto;
                            object-fit: contain;
                            margin-right: 16px;
                        }}
                        .hospital-identity h1 {{
                            color: #1e3a8a;
                            margin: 0 0 3px 0;
                            font-size: 20px;
                            font-weight: 800;
                            letter-spacing: 0.02em;
                        }}
                        .hospital-tagline {{
                            color: #0284c7;
                            font-size: 11.5px;
                            font-weight: 600;
                            font-style: italic;
                            margin: 0 0 5px 0;
                        }}
                        .hospital-address {{
                            color: #475569;
                            font-size: 11px;
                            line-height: 1.4;
                            margin: 0;
                        }}
                        .contact-right {{
                            width: 36%;
                            text-align: right;
                            font-size: 11px;
                            color: #334155;
                            line-height: 1.6;
                            padding-left: 14px;
                        }}
                        .contact-label {{
                            font-weight: 700;
                            color: #1e3a8a;
                        }}
                        .emergency-badge {{
                            display: inline-block;
                            color: #b91c1c;
                            padding: 2px 6px;
                            font-weight: 700;
                            font-size: 10.5px;
                            margin-top: 2px;
                        }}

                        /* Document Metadata Banner */
                        .doc-meta-banner {{
                            display: -webkit-box;
                            display: flex;
                            -webkit-box-pack: justify;
                            justify-content: space-between;
                            -webkit-box-align: center;
                            align-items: center;
                            background-color: #f1f5f9;
                            border-left: 4px solid #1e3a8a;
                            padding: 10px 14px;
                            margin: 14px 0 12px 0;
                            border-radius: 0 4px 4px 0;
                        }}
                        .doc-title-block h2 {{
                            margin: 0 0 2px 0;
                            font-size: 15px;
                            font-weight: 700;
                            color: #0f172a;
                            letter-spacing: 0.03em;
                        }}
                        .doc-period {{
                            font-size: 11.5px;
                            color: #475569;
                        }}
                        .doc-audit-block {{
                            text-align: right;
                            font-size: 11.5px;
                            color: #475569;
                            line-height: 1.5;
                        }}

                        /* Claim / Patient Details Grid (for BillItemDto) */
                        .claim-details-box {{
                            border: 1px solid #cbd5e1;
                            border-radius: 4px;
                            padding: 10px 14px;
                            margin-bottom: 16px;
                            background-color: #f8fafc;
                        }}
                        .claim-details-table {{
                            width: 100%;
                            border-collapse: collapse;
                            font-size: 12px;
                        }}
                        .claim-details-table td {{
                            padding: 4px 6px;
                            color: #334155;
                        }}
                        .claim-field-label {{
                            font-weight: 700;
                            color: #1e3a8a;
                            width: 18%;
                        }}
                        .claim-field-value {{
                            width: 32%;
                            border-bottom: 1px dotted #94a3b8;
                        }}

                        /* Data Table */
                        .table-wrapper {{
                            margin-top: 6px;
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
                            padding: 9px 12px;
                            font-size: 11.5px;
                            text-transform: uppercase;
                            letter-spacing: 0.04em;
                        }}
                        .table-border td {{
                            padding: 9px 12px;
                            border-bottom: 1px solid #e2e8f0;
                            color: #334155;
                            font-size: 12.5px;
                        }}
                        .table-border tbody tr:nth-child(even) {{
                            background-color: #f8fafc;
                        }}
                        .sn-col {{
                            width: 40px;
                            font-weight: 600;
                            color: #64748b;
                        }}
                    </style>
                </head>
                <body>
                    <div class='report-container'>
                        <div class='letterhead'>
                            <div class='brand-left'>
                                {(string.IsNullOrEmpty(imgSrc) ? "" : $"<img src='{imgSrc}' alt='Logo' />")}
                                <div class='hospital-identity'>
                                    <h1>{_hospitalInfo.Name}</h1>
                                    <div class='hospital-tagline'>{_hospitalInfo.Tagline}</div>
                                    <p class='hospital-address'>
                                        {_hospitalInfo.AddressLine1}<br/>
                                        {_hospitalInfo.AddressLine2}
                                    </p>
                                </div>
                            </div>
                            <div class='contact-right'>
                                <div><span class='contact-label'>Tel:</span> {_hospitalInfo.Phone}</div>
                                <div><span class='contact-label'>Email:</span> {_hospitalInfo.Email}</div>
                                <div class='emergency-badge'>24/7 Emergency: {_hospitalInfo.EmergencyPhone}</div>
                            </div>
                        </div>

                        <div class='doc-meta-banner'>
                            <div class='doc-title-block'>
                                <h2>{reportTitle}</h2>
                                <div class='doc-period'>
                                    <strong>Ref No:</strong> {docRefNumber} &nbsp;|&nbsp;
                                    <strong>Period:</strong> {datePeriod}
                                </div>
                            </div>
                            <div class='doc-audit-block'>
                                <div><strong>Prepared By:</strong> {preparedBy}</div>
                                <div><strong>Printed On:</strong> {DateTime.Now:dd MMM yyyy, HH:mm}</div>
                            </div>
                        </div>");

            if (isInsuranceClaim)
            {
                html.Append($@"
                        <div class='claim-details-box'>
                            <table class='claim-details-table'>
                                <tr>
                                    <td class='claim-field-label'>Patient Name:</td>
                                    <td class='claim-field-value'>&nbsp;</td>
                                    <td class='claim-field-label'>Insurance Provider:</td>
                                    <td class='claim-field-value'>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class='claim-field-label'>Patient / File ID:</td>
                                    <td class='claim-field-value'>&nbsp;</td>
                                    <td class='claim-field-label'>Policy / Member No:</td>
                                    <td class='claim-field-value'>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class='claim-field-label'>Attending Clinician:</td>
                                    <td class='claim-field-value'>{preparedBy}</td>
                                    <td class='claim-field-label'>Authorization / Claim ID:</td>
                                    <td class='claim-field-value'>{docRefNumber}</td>
                                </tr>
                            </table>
                        </div>");
            }

            html.Append(@"
                        <div class='table-wrapper'>
                            <table class='table-border'>
                                <thead>
                                    <tr>");

            if (dto.Data != null)
            {
                if (dto.Data is IEnumerable<StaffDto>)
                {
                    html.Append("<th class='sn-col'>Sn.</th><th>Full Name</th><th>Employee Id</th><th>Title</th><th>Availability</th>");
                }
                else if (dto.Data is IEnumerable<PatientDto>)
                {
                    html.Append("<th class='sn-col'>Sn.</th><th>Full Name</th><th>Phone Number</th>");
                }
                else if (dto.Data is IEnumerable<LabTestDto>)
                {
                    html.Append("<th class='sn-col'>Sn.</th><th>Disease Name</th><th>Patient Full Name</th><th>Date</th>");
                }
                else if (dto.Data is IEnumerable<MedicineDto>)
                {
                    html.Append("<th class='sn-col'>Sn.</th><th>Medicine Name</th><th>Quantity</th><th>Price</th>");
                }
                else if (dto.Data is IEnumerable<VisitDto>)
                {
                    html.Append("<th class='sn-col'>Sn.</th><th>Patient</th><th>Patient Type</th><th>Visit Type</th><th>Patient Status</th><th>Registered Date</th>");
                }
                else if (dto.Data is IEnumerable<BillItemDto>)
                {
                    html.Append("<th class='sn-col'>Sn.</th><th>Insurer Code</th><th>Service / Item Description</th><th>Qty</th><th>Unit Price</th><th>Covered %</th><th>Claim Amount</th>");
                }
            }

            html.Append(@"
                                    </tr>
                                </thead>
                                <tbody>");

            if (dto.Data != null)
            {
                int sn = 1;
                if (dto.Data is IEnumerable<StaffDto> staffList)
                {
                    foreach (StaffDto staff in staffList)
                    {
                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td>{staff.FisrtName} {staff.MiddleName} {staff.LastName}</td><td>{staff.EmployeeId}</td><td>{staff.AdministrativeTitleName} / {staff.ClinicalTitleName}</td><td>{staff.StaffStatusName}</td></tr>");
                    }
                }
                else if (dto.Data is IEnumerable<PatientDto> patientList)
                {
                    foreach (PatientDto patient in patientList)
                    {
                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td>{patient.FisrtName} {patient.MiddleName} {patient.LastName}</td><td>{patient.PhoneNumber}</td></tr>");
                    }
                }
                else if (dto.Data is IEnumerable<LabTestDto> labList)
                {
                    foreach (LabTestDto lab in labList)
                    {
                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td>{lab.DiseaseName}</td><td>{lab.PatientFullName}</td><td>{lab.CreatedDate:yyyy-MM-dd}</td></tr>");
                    }
                }
                else if (dto.Data is IEnumerable<MedicineDto> medList)
                {
                    foreach (MedicineDto med in medList)
                    {
                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td>{med.MedicineName}</td><td>{med.MedicineQty}</td><td>${med.SellingPrice:N2}</td></tr>");
                    }
                }
                else if (dto.Data is IEnumerable<VisitDto> visList)
                {
                    foreach (VisitDto vis in visList)
                    {
                        html.Append($@"<tr><td class='sn-col'>{sn++}</td><td>{vis.PatientFullName}</td><td>{vis.PatientTypeName}</td><td>{vis.VisitTypeName}</td><td>{vis.PatientStatusName}</td><td>{vis.VisitDate}</td></tr>");
                    }
                }
                else if (dto.Data is IEnumerable<BillItemDto> claimItems)
                {
                    decimal totalClaimed = 0m;
                    foreach (BillItemDto item in claimItems)
                    {
                        totalClaimed += item.InsuranceCoveredAmount;
                        html.Append($@"<tr>
                            <td class='sn-col'>{sn++}</td>
                            <td>{(string.IsNullOrWhiteSpace(item.InsurerItemCode) ? "-" : item.InsurerItemCode)}</td>
                            <td>{item.ItemName} <small>({item.SourceType})</small></td>
                            <td>{item.Quantity}</td>
                            <td>{item.UnitPrice:N2}</td>
                            <td>{item.InsuranceCoveragePercent:0}%</td>
                            <td><strong>{item.InsuranceCoveredAmount:N2}</strong></td>
                        </tr>");
                    }

                    html.Append($@"
                        <tr style='background-color: #eef2ff; font-weight: bold;'>
                            <td colspan='6' style='text-align: right;'>TOTAL CLAIMABLE AMOUNT:</td>
                            <td>{totalClaimed:N2}</td>
                        </tr>");
                }
            }

            html.AppendLine("</tbody></table></div>");

            if (isInsuranceClaim)
            {
                html.Append($@"
                    <div style='margin-top: 35px; display: -webkit-box; display: flex; -webkit-box-pack: justify; justify-content: space-between; font-size: 12px; color: #334155;'>
                        <div style='width: 45%; border-top: 1px solid #94a3b8; padding-top: 6px;'>
                            <strong>Patient / Beneficiary Signature &amp; Thumbprint</strong><br/>
                            Date: ________________________
                        </div>
                        <div style='width: 45%; border-top: 1px solid #94a3b8; padding-top: 6px;'>
                            <strong>Hospital Officer Signature &amp; Official Stamp</strong><br/>
                            Prepared By: {preparedBy}
                        </div>
                    </div>");
            }

            html.AppendLine("</div></body></html>");

            return html.ToString();
        }
    }
}
