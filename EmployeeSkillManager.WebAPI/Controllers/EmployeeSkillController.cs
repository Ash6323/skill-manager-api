using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
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
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // GET api/<EmployeeSkillController>/5
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin, Employee")]
        public IActionResult Get(string id)
        {
            try
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
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // POST api/<EmployeeSkillController>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Post(EmployeeAddSkillDTO employeeSkill)
        {
            try
            {
                int result = _employeeSkillService.AddEmployeeSkill(employeeSkill);

                if (result.Equals(0))
                {

                    Response response = new(StatusCodes.Status400BadRequest, ConstantMessages.DuplicateSkill, null);
                    return BadRequest(response);
                }
                else if (result.Equals(1))
                {

                    Response response = new(StatusCodes.Status400BadRequest, ConstantMessages.InvalidDetails, null);
                    return BadRequest(response);
                }
                else
                {
                    Response response = new Response
                        (StatusCodes.Status200OK, ConstantMessages.DataAddedSuccessfully, ConstantMessages.DataAddedSuccessfully);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
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
