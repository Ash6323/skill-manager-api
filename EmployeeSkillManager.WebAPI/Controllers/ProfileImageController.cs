﻿using EmployeeSkillManager.Data.Constants;
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
            Task<int> result = _profileImageService.UploadImageAsync(model);
            if (model == null)
            {
                return BadRequest(new Response(StatusCodes.Status400BadRequest, ConstantMessages.ErrorOccurred, null));
            }

            Response response = new
                        Response(StatusCodes.Status200OK, ConstantMessages.UploadSuccessful, ConstantMessages.UploadSuccessful);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Get(string id)
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
    }
}
