using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebducationApi.Application.Interface;
using WebducationApi.Domain.Entities;
using WebducationApi.Domain.Interface;

namespace WebducationApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/teachers")]
    public class TeacherController : Controller
    {
        private readonly ITeacherApplication _teacherApplication;

        public TeacherController(ITeacherApplication teacherApplication)
        {
            _teacherApplication = teacherApplication;
        }

        [HttpGet("GetTeachers")]
        public async Task<IActionResult> GetTeachers()
        {
            try
            {
                var teachers = await _teacherApplication.GetAllTeachersAsync();
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("GetTeacherById")] 
        public async Task<IActionResult> GetTeacherByIdAsync(int teacherId)
        {
            try
            {
                var teacher = await _teacherApplication.GetTeacherByIdAsync(teacherId);
                if (teacher == null)
                {
                    return NotFound("Teacher not found.");
                }
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost("CreateTeacher")]
        public async Task<IActionResult> CreateTeacher([FromBody] TeacherDTO teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher data is invalid.");
            }

            try
            {
                var newTeacher = await _teacherApplication.CreateTeacherAsync(teacher);
                return Ok(newTeacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPut("UpdateTeacher")]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherDTO teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher data is invalid (teacher is empty).");
            }

            if (teacher.TeacherId <= 0)
            {
                return BadRequest("Teacher ID is invalid.");
            }

            try
            {
                var updatedTeacher = await _teacherApplication.UpdateTeacherAsync(teacher);
                if (updatedTeacher == null)
                {
                    return NotFound("Teacher not found for update.");
                }

                return Ok(updatedTeacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteTeacher")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            try
            {
                var deletedTeacher = await _teacherApplication.DeleteTeacherAsync(teacherId);
                if (deletedTeacher == null)
                {
                    return NotFound("Teacher not found for deletion.");
                }
                return Ok(deletedTeacher);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
