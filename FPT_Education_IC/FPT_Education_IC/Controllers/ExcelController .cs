using FPT_Education_IC.Data;
using FPT_Education_IC.Service;
using FPT_Education_IC.Models;
using FPT_Education_IC.ViewModels;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using FPT_Education_IC.Statics;

namespace FPT_Education_IC.Controllers
{
    [Route("Excel")]
    public class ExcelController : Controller
    {

        private readonly ILogger<ExcelController> _logger;
        private readonly DataContext _context;

        public ExcelController(ILogger<ExcelController> logger, DataContext dataContext)
        {
            this._logger = logger;
            this._context = dataContext;
        }

        [Route("ExportProgramData")]
        [Authorize]
        public ActionResult ExportProgramData(string programId)
        {
            if (!string.IsNullOrEmpty(programId))
            {
                int PrgramID = int.Parse(programId);
                ProgramsService programsService = new ProgramsService(_context);
                RegisterService registerService = new RegisterService(_context);
                Models.Program program = programsService.getProgramById(PrgramID);
                ArrayList listPartnerProgram = programsService.getListPartnerProgram(program.ProgramId);
                ArrayList listAllRegister = registerService.GetAllProgramRegister(program.ProgramId);
                if (listPartnerProgram == null || listPartnerProgram.Count <= 0)
                {
                    listAllRegister = registerService.GetAllProgramNonePartnerRegister(program.ProgramId);
                }
                CountryService countryService = new CountryService(_context);
                string programCountry = countryService.GetCountryName(program.Country);

                // Tạo một package Excel mới
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(program.Title);

                    // Đặt tiêu đề cho tệp Excel
                    package.Workbook.Properties.Title = program.Title;

                    // Điền dữ liệu vào các ô trong tệp Excel
                    worksheet.Cells["A1"].Value = "NO."; // Số thứ tự
                    worksheet.Cells["B1"].Value = "STUDENT ID";
                    worksheet.Cells["C1"].Value = "NAME";
                    worksheet.Cells["D1"].Value = "COUNTRY OF DESTINATION";
                    worksheet.Cells["E1"].Value = "DESTINATION";
                    worksheet.Cells["F1"].Value = "ISSUE DATE";
                    worksheet.Cells["G1"].Value = "EXCHANGE START DATE";
                    worksheet.Cells["H1"].Value = "EXCHANGE END DATE";
                    worksheet.Cells["I1"].Value = "PROGRAM";

                    // Định dạng cột STT (Số thứ tự) và căn giữa dữ liệu
                    var columnSTT = worksheet.Column(1);
                    columnSTT.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    columnSTT.AutoFit();

                    // Định dạng cột tiêu đề (in đậm)
                    var headerStyle = worksheet.Cells["A1:I1"].Style;
                    headerStyle.Font.Bold = true;

                    int count = 2;
                    int stt = 1;
                    // Điền dữ liệu
                    if (listAllRegister != null && listAllRegister.Count > 0)
                    {
                        foreach(ViewRegisterProgram view in listAllRegister)
                        {
                            if (view.RegisterStatus == StaticsData.REGISTER_ACCEPT)
                            {
                                worksheet.Cells["A" + count].Value = stt;
                                worksheet.Cells["B" + count].Value = view.UserRollNumber;
                                worksheet.Cells["C" + count].Value = view.UserName;
                                if (listPartnerProgram != null && listPartnerProgram.Count > 0)
                                {
                                    worksheet.Cells["D" + count].Value = view.PartnerCountry;
                                    worksheet.Cells["E" + count].Value = view.PartnerName;
                                }
                                else
                                {
                                    worksheet.Cells["D" + count].Value = programCountry;
                                    worksheet.Cells["E" + count].Value = "";
                                }
                                worksheet.Cells["F" + count].Value = program.RegisterStartDate.ToString("dd/MM/yyyy");
                                worksheet.Cells["G" + count].Value = program.StartDate.ToString("dd/MM/yyyy");
                                worksheet.Cells["H" + count].Value = program.EndDate.ToString("dd/MM/yyyy");
                                worksheet.Cells["I" + count].Value = program.Title;

                                stt++;
                                count++;
                            }
                        }
                    }
                   

                    // Định dạng border cho dữ liệu
                    var dataStyle = worksheet.Cells["A1:I" + count].Style;
                    dataStyle.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataStyle.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataStyle.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataStyle.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    string excelName = program.Title + ".xlsx";
                    // Lưu tệp Excel vào một MemoryStream
                    using (var memoryStream = new MemoryStream())
                    {
                        package.SaveAs(memoryStream);

                        // Trả về tệp Excel như một tệp tin kết quả để tải xuống
                        return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                    }
                }
            }

            return RedirectToAction("ProgramInformation", "Programs", new { programId = programId });
        }
    }
}
