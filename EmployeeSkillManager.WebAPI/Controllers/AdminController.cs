using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<AdminDTO> result = _adminService.GetAdmins();
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
            AdminDTO result = _adminService.GetAdmin(id);
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
            string result = await _adminService.RegisterAdmin(inputModel);

            if (result.Equals("0"))
            {
                Response userExistsResponse = new Response
                    (StatusCodes.Status400BadRequest, ConstantMessages.UserAlreadyExists, ConstantMessages.UserAlreadyExists);
                return BadRequest(userExistsResponse);
            }
            else if (result.Equals("-1"))
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
