using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Services.Interfaces;
using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.Models;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{id}")]
        public IActionResult GeneratePDF(string id)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                ReportDTO data = _reportService.GetReportContent(id);
                PdfGenerator.AddPdfPages(document, data.Content, PageSize.A4);

                byte[]? response = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms);
                    response = ms.ToArray();
                }
                string Filename = "SkillReport_" + data.EmployeeName + ".pdf";
                return File(response, "application/pdf", Filename);
            }
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }
    }
}
