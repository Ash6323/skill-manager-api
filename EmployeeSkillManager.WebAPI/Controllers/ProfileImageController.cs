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
    [Authorize(Roles = "Admin, Employee")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly IProfileImageService _profileImageService;
        public ProfileImageController(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProfileImageUploadDTO model)
        {
            try
            {
                await _profileImageService.UploadImage(model);
                if (model == null)
                {
                    return BadRequest(new Response(StatusCodes.Status400BadRequest, ConstantMessages.ErrorOccurred, null));
                }
                Response response = new
                            Response(StatusCodes.Status200OK, ConstantMessages.UploadSuccessful, ConstantMessages.UploadSuccessful);
                return Ok(response);
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
                string result = _profileImageService.GetImage(id);
                if (result == null)
                {
                    return BadRequest(new Response(StatusCodes.Status400BadRequest, ConstantMessages.ImageNotFound, null));
                }
                Response response = new
                            Response(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Response response = new Response
                    (StatusCodes.Status500InternalServerError, ConstantMessages.ErrorOccurred, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                int result =  _profileImageService.DeleteImage(id);
                if (result.Equals(0))
                {
                    Response response = new Response
                                        (StatusCodes.Status200OK, 
                                        ConstantMessages.DataDeletedSuccessfully, ConstantMessages.DataDeletedSuccessfully);
                    return Ok(response);   
                }
                return BadRequest(new Response(StatusCodes.Status400BadRequest, ConstantMessages.ErrorOccurred, null));
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
