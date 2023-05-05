using EmployeeSkillManager.Data.Constants;
using EmployeeSkillManager.Data.DTOs;
using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly IProfileImageService _profileImageService;

        public ProfileImageController(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        [HttpPost]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> SubmitPost([FromForm] ProfileImageUploadDTO request)
        {
            if (request == null)
            {
                Response response = new Response (StatusCodes.Status400BadRequest, 
                                                    ConstantMessages.ErrorOccurred, ConstantMessages.ErrorOccurred);
                return BadRequest();
            }
            if (request.Image != null)
            {
                await _profileImageService.UploadImageAsync(request);
            }
            var postResponse = await _profileImageService.SaveImageAsync(request);
            //if (!postResponse)
            //{
            //    return NotFound(postResponse);
            //}
            return Ok(postResponse);
        }
    }
}
