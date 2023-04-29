using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
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

        [HttpPost]
        [Route("RegisterEmployee")]
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
    }
}
