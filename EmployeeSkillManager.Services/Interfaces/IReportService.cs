using EmployeeSkillManager.Data.DTOs;

namespace EmployeeSkillManager.Services.Interfaces
{
    public interface IReportService
    {
        ReportDTO GetReportContent(string id);
    }
}
