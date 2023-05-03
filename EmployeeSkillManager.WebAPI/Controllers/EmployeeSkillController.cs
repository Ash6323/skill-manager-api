using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        //[Authorize(Roles = "Admin")]
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
        //[Authorize(Roles = "Admin, Employee")]
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
        //[Authorize(Roles = "Admin")]
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
                Response response = new(StatusCodes.Status400BadRequest, ConstantMessages.DuplicateSkill, null);
                return Ok(response);
            }
        }

        // PUT api/<EmployeeSkillController>/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeSkillController>/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public void Delete(int id)
        {
        }
    }
}
