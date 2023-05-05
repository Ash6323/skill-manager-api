using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Enums;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            try
            {
                List<UserDTO> result = _employeeService.GetEmployees();
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

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Get(string id)
        {
            try
            {
                UserDTO result = _employeeService.GetEmployee(id);
                if (result != null)
                {
                    Response foundResponse = new(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                    return Ok(foundResponse);
                }
                Response nullResponse = new(StatusCodes.Status404NotFound, ConstantMessages.UserNotFound, null);
                return NotFound(nullResponse);
            }
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin, Employee")]
        public IActionResult Put(string id, [FromBody] EmployeeUpdateDTO updatedEmployee)
        {
            try
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
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            try
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
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpGet("Gender")]
        public IActionResult GetGender()
        {
            try
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
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
