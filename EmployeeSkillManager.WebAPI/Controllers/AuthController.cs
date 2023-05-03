﻿using EmployeeSkillManager.Data.Models;
using EmployeeSkillManager.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using EmployeeSkillManager.Services.Interfaces;
using EmployeeSkillManager.Data.Constants;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeSkillManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("AdminRegistration")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminRegistration([FromBody] UserRegistrationDTO inputModel)
        {
            string result = await _authService.RegisterAdmin(inputModel);

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
        [HttpPost]
        [Route("EmployeeRegistration")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> EmployeeRegistration([FromBody] UserRegistrationDTO inputModel)
        {
            string result = await _authService.RegisterEmployee(inputModel);

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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO loginModel)
        {
            AuthBody authBody = await _authService.LoginAuth(loginModel);
            if (authBody != null)
            {
                return Ok(authBody);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
