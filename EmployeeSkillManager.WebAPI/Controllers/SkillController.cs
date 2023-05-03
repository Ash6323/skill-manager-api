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
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        // GET: api/<SkillController>
        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Get()
        {
            List<SkillDTO> result = _skillService.GetSkills();
            if (result != null)
            {
                Response response = new
                    Response(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                return Ok(response);
            }
            return NoContent();
        }

        // GET api/<SkillController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Get(int id)
        {
            SkillDTO result = _skillService.GetSkill(id);
            if (result != null)
            {
                Response foundResponse = new(StatusCodes.Status200OK, ConstantMessages.DataRetrievedSuccessfully, result);
                return Ok(foundResponse);
            }
            Response notFoundResponse = new(StatusCodes.Status404NotFound, ConstantMessages.SkillNotFound, null);
            return NotFound(notFoundResponse);
        }

        // POST api/<SkillController>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(SkillDTO skill)
        {
            int result = _skillService.AddSkill(skill);

            Response response = new(StatusCodes.Status200OK, ConstantMessages.DataAddedSuccessfully, result);
            return Ok(response);
        }

        // PUT api/<SkillController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] SkillDTO updatedSkill)
        {
            int result = _skillService.UpdateSkill(id, updatedSkill);
            if (result.Equals(0))
            {
                Response response = new
                    (StatusCodes.Status404NotFound, ConstantMessages.SkillNotFound, ConstantMessages.SkillNotFound);
                return NotFound(response);
            }
            else
            {
                Response response = new(StatusCodes.Status200OK, ConstantMessages.DataUpdatedSuccessfully, result);
                return Ok(response);
            }
        }

        // DELETE api/<SkillController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            int result = _skillService.DeleteSkill(id);
            if (result.Equals(0))
            {
                Response response =
                    new(StatusCodes.Status400BadRequest, ConstantMessages.SkillNotFound, null);
                return BadRequest(response);
            }
            else
            {
                Response response = new(StatusCodes.Status200OK, ConstantMessages.DataDeletedSuccessfully, result);
                return Ok(response);
            }
        }
    }
}
