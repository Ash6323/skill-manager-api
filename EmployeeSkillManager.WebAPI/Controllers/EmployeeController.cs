using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Enums;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<EmployeeDTO> result = _employeeService.GetEmployees();
            if (result != null)
            {
                Response response = new
                    Response(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                return Ok(response);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            EmployeeDTO result = _employeeService.GetEmployee(id);
            if (result != null)
            {
                Response foundResponse = new(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                return Ok(foundResponse);
            }
            Response nullResponse = new(StatusCodes.Status404NotFound, ConstantMessages.UserNotFound, null);
            return NotFound(nullResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegistrationDTO inputModel)
        {
            string result =  await _employeeService.RegisterEmployee(inputModel);

            if(result.Equals("0"))
            {
                Response userExistsResponse = new Response
                    (StatusCodes.Status400BadRequest, ConstantMessages.UserAlreadyExists, ConstantMessages.UserAlreadyExists);
                return BadRequest(userExistsResponse);
            }
            else if(result.Equals("-1")) 
            {
                Response failedResponse = new Response
                    (StatusCodes.Status400BadRequest, ConstantMessages.UserCreationFailed, ConstantMessages.UserCreationFailed);
                return BadRequest(failedResponse);
            }
            else
            {
                Response response = new Response(StatusCodes.Status200OK, ConstantMessages.UserCreated, result);
                return Ok(response);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] EmployeeUpdateDTO updatedEmployee)
        {
            string result = _employeeService.UpdateEmployee(id, updatedEmployee);
            if (result.Equals("0"))
            {
                Response response = new
                    (StatusCodes.Status404NotFound, ConstantMessages.UserNotFound, ConstantMessages.UserNotFound);
                return NotFound(response);
            }
            else
            {
                Response response = new(StatusCodes.Status200OK, ConstantMessages.DataUpdatedSuccessfully, result);
                return Ok(response);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            string result = _employeeService.DeleteEmployee(id);
            if (result.Equals("0"))
            {
                Response response =
                    new(StatusCodes.Status400BadRequest, ConstantMessages.UserNotFound, null);
                return BadRequest(response);
            }
            else
            {
                Response response = new(StatusCodes.Status200OK, ConstantMessages.DataDeletedSuccessfully, result);
                return Ok(response);
            }
        }
        [HttpGet("Gender")]
        public IActionResult GetGender()
        {
            List<string> result = _employeeService.GetGenderEnum();
            if (result != null)
            {
                Response response = new
                    Response(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                return Ok(response);
            }
            return NoContent();
        }
    }
}
