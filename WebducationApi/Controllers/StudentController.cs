using Microsoft.AspNetCore.Mvc;
using WebducationApi.Application.Interface;
using WebducationApi.Domain.Entities;
using WebducationApi.Domain.Interface;

namespace WebducationApi.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class StudentController : Controller
    {
        private readonly IStudentApplication _studentApplication;
       
        public StudentController(IStudentApplication studentApplication)
        {
            _studentApplication = studentApplication;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var users = await _studentApplication.GetAllStudentsAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("GetStudentById")]
        public async Task<IActionResult> GetStudentByIdAsync(int studentId)
        {
            try
            {
                var client = await _studentApplication.GetStudentByIdAsync(studentId);
                if (client == null)
                {
                    return NotFound("Cliente no encontrado.");
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDTO student)
        {
            if (student == null)
            {
                return BadRequest("Los datos del cliente son inválidos.");
            }

            try
            {
                var studentClient = await _studentApplication.CreateStudentAsync(student);
                return Ok(studentClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentDTO student)
        {
            if (student == null)
            {
                return BadRequest("Los datos del cliente son inválidos (el cliente está vacío).");
            }

            if (student.StudentId <= 0)
            {
                return BadRequest("El ID del cliente no es válido.");
            }

            try
            {
                var updatedClient = await _studentApplication.UpdateStudentAsync(student);
                if (updatedClient == null)
                {
                    return NotFound("Cliente no encontrado para actualizar.");
                }

                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int userId)
        {
            try
            {
                var deletedClient = await _studentApplication.DeleteStudentAsync(userId);
                if (deletedClient == null)
                {
                    return NotFound("Cliente no encontrado para eliminar.");
                }
                return Ok(deletedClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
