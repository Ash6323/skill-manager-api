using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSkillController : ControllerBase
    {
        private readonly IEmployeeSkillService _employeeSkillService;
        public EmployeeSkillController(IEmployeeSkillService employeeSkillService)
        {
            _employeeSkillService = employeeSkillService;
        }
        // GET: api/<EmployeeSkillController>
        [HttpGet]
        public IActionResult Get()
        {
            List<EmployeeSkillDTO> result = _employeeSkillService.GetAllEmployeeSkills();
            if (result != null)
            {
                Response response = new
                    Response(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                return Ok(response);
            }
            return NoContent();
        }

        // GET api/<EmployeeSkillController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            EmployeeSkillDTO result = _employeeSkillService.GetEmployeeSkills(id);
            if (result != null)
            {
                Response foundResponse = new(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                return Ok(foundResponse);
            }
            Response notFoundResponse = new(StatusCodes.Status404NotFound, ConstantMessages.SkillNotFound, null);
            return NotFound(notFoundResponse);
        }

        // POST api/<EmployeeSkillController>
        [HttpPost]
        public IActionResult Post(EmployeeAddSkillDTO employeeSkill)
        {
            int result = _employeeSkillService.AddEmployeeSkill(employeeSkill);

            if (result.Equals(1))
            {
                Response response = new Response
                    (StatusCodes.Status200OK, ConstantMessages.DataAddedSuccessfully, ConstantMessages.DataAddedSuccessfully);
                return Ok(response);
            }
            else
            {
                Response response = new(StatusCodes.Status400BadRequest, ConstantMessages.ErrorOccurred, null);
                return Ok(response);
            }
        }

        // PUT api/<EmployeeSkillController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeSkillController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
