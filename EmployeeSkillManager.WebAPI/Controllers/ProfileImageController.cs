using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.Context;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IProfileImageService _profileImageService;
        public ProfileImageController(IWebHostEnvironment environment, IProfileImageService profileImageService)
        {
            _environment = environment;
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
        [HttpGet]
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
    }
}
