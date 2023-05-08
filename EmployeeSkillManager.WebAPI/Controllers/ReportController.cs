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
        private readonly IEmployeeSkillService _employeeSkillService;
        public ReportController(IEmployeeSkillService employeeSkillService) 
        {
            _employeeSkillService = employeeSkillService;
        }

        [HttpGet("{id}")]
        public IActionResult GeneratePDF(string id)
        {
            try
            {
                var document = new PdfDocument();

                EmployeeSkillDTO detail = _employeeSkillService.GetEmployeeSkills(id);
                List<SkillExpertiseDTO> SortedList = detail.EmployeeSkills.OrderBy(o => o.Expertise).ToList();
                detail.EmployeeSkills = SortedList;

                string htmlcontent = "<div style='width:100%; text-align:center'>";
                htmlcontent += "<h2>Employee Skill Report</h2>";

                htmlcontent += "<h2> Employee Name: " + detail.EmployeeName + "</h2>";
                htmlcontent += "<table style ='width:100%; border: 1px solid #000'>";
                htmlcontent += "<thead style='font-weight:bold'>";
                htmlcontent += "<tr>";
                htmlcontent += "<td style='border:1px solid #000'> Skill No. </td>";
                htmlcontent += "<td style='border:1px solid #000'> Skill </td>";
                htmlcontent += "<td style='border:1px solid #000'> Expertise </td>";
                htmlcontent += "</tr>";
                htmlcontent += "</thead >";

                htmlcontent += "<tbody>";
                if (detail != null)
                {
                    int i = 1;
                    detail.EmployeeSkills.ForEach(item =>
                    {
                        htmlcontent += "<tr>";
                        htmlcontent += "<td>" + i + "</td >";
                        htmlcontent += "<td>" + item.SkillName + "</td>";
                        htmlcontent += "<td> " + item.Expertise + "</td >";
                        htmlcontent += "</tr>";
                        i++;
                    });
                }
                htmlcontent += "</tbody>";
                htmlcontent += "</table>";
                htmlcontent += "</div>";

                PdfGenerator.AddPdfPages(document, htmlcontent, PageSize.A4);

                byte[]? response = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms);
                    response = ms.ToArray();
                }
                string Filename = "SkillReport_" + detail.EmployeeName + ".pdf";
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
