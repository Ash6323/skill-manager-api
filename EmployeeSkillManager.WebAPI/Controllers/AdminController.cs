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
    //[Authorize(Roles = "Admin")]
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
            try
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
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
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
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AdminUpdateDTO updatedAdmin)
        {
            try
            {
                string result = _adminService.UpdateAdmin(id, updatedAdmin);
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
        public IActionResult Delete(string id)
        {
            try
            {
                string result = _adminService.DeleteAdmin(id);
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
    }
}
