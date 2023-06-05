using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Services.Interfaces;

namespace EmployeeSkillManager.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IEmployeeSkillService _employeeSkillService;
        public ReportService(IEmployeeSkillService employeeSkillService)
        {
            _employeeSkillService = employeeSkillService;
        }
        public ReportDTO GetReportContent(string id)
        {
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

            ReportDTO data = new ReportDTO{EmployeeName = detail.EmployeeName, Content =  htmlcontent};
            return data;
        }
    }
}
