using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebducationApi.Application.Interface;
using WebducationApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebducationApi.Controllers
{
    [Authorize]
    [ApiController] 
    [Route("api/courses")]
    public class CourseController : Controller
    {
        private readonly ICourseApplication _courseApplication;

        public CourseController(ICourseApplication courseApplication)
        {
            _courseApplication = courseApplication;
        }

        [HttpGet("GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            try
            {
                var courses = await _courseApplication.GetAllCoursesAsync();
                return Ok(courses.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("GetCourseById")]
        public async Task<IActionResult> GetCourseByIdAsync(int courseId)
        {
            try
            {
                var response = await _courseApplication.GetCourseByIdAsync(courseId);
                if (!response.IsSuccess)
                {
                    return NotFound(response.Message);
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CourseDTO course)
        {
            if (course == null)
            {
                return BadRequest("Course data is invalid.");
            }

            try
            {
                var response = await _courseApplication.CreateCourseAsync(course);
                if (!response.IsSuccess)
                {
                    return StatusCode(500, response.Message);
                }
                return Ok(new { CourseId = response.Data, Message = "Course created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPut("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseDTO course)
        {
            if (course == null)
            {
                return BadRequest("Course data is invalid (course is empty).");
            }

            if (course.CourseId <= 0)
            {
                return BadRequest("Course ID is invalid.");
            }

            try
            {
                var response = await _courseApplication.UpdateCourseAsync(course);
                if (!response.IsSuccess)
                {
                    return NotFound(response.Message);
                }
                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            try
            {
                var response = await _courseApplication.DeleteCourseAsync(courseId);
                if (!response.IsSuccess)
                {
                    return NotFound(response.Message);
                }
                return Ok(new { Message = "Course deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}