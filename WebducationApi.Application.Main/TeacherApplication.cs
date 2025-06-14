using AutoMapper;
using LibraryProject.Transversal.Common;
using WebducationApi.Application.Interface;
using WebducationApi.Domain.Entities; // Assuming TeacherDTO and TeacherME are defined here
using WebducationApi.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebducationApi.Application.Main
{
    // Implementation of the ITeacherApplication interface
    public class TeacherApplication : ITeacherApplication
    {
        private readonly ITeacherDomain _teacherDomain; // Interface for domain-specific operations for teachers
        private readonly IMapper _mapper; // AutoMapper for object mapping between DTOs and domain entities

        // Constructor for dependency injection
        public TeacherApplication(ITeacherDomain teacherDomain, IMapper mapper)
        {
            _teacherDomain = teacherDomain;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all teachers.
        /// </summary>
        /// <returns>A response containing a collection of TeacherDTOs.</returns>
        public async Task<Response<IEnumerable<TeacherDTO>>> GetAllTeachersAsync()
        {
            var response = new Response<IEnumerable<TeacherDTO>>();
            try
            {
                // Call the domain layer to get all teacher entities
                var teachers = await _teacherDomain.GetAllTeachersAsync();
                // Map the domain entities to DTOs for the application layer
                response.Data = _mapper.Map<IEnumerable<TeacherDTO>>(teachers);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while retrieving all teachers: {ex.Message}";
            }
            return response;
        }

        /// <summary>
        /// Retrieves a specific teacher by their ID.
        /// </summary>
        /// <param name="teacherId">The ID of the teacher to retrieve.</param>
        /// <returns>A response containing the TeacherDTO.</returns>
        public async Task<Response<TeacherDTO>> GetTeacherByIdAsync(int teacherId)
        {
            var response = new Response<TeacherDTO>();
            try
            {
                // Call the domain layer to get a specific teacher entity by ID
                var teacher = await _teacherDomain.GetTeacherByIdAsync(teacherId);
                // Map the domain entity to a DTO
                response.Data = _mapper.Map<TeacherDTO>(teacher);
                response.IsSuccess = response.Data != null; // Success if data is not null
                if (!response.IsSuccess)
                {
                    response.Message = $"Teacher with ID {teacherId} not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while retrieving teacher with ID {teacherId}: {ex.Message}";
            }
            return response;
        }

        /// <summary>
        /// Creates a new teacher.
        /// </summary>
        /// <param name="dto">The TeacherDTO containing the new teacher's data.</param>
        /// <returns>A generic response containing the ID of the newly created teacher.</returns>
        public async Task<ResponseGeneric<int>> CreateTeacherAsync(TeacherDTO dto)
        {
            var response = new ResponseGeneric<int>();
            try
            {
                // Map the incoming DTO to the domain entity
                var teacherME = _mapper.Map<TeacherME>(dto);
                // Call the domain layer to create the teacher and get the inserted ID
                var insertedId = await _teacherDomain.CreateTeacherAsync(teacherME);

                response.Data = insertedId;
                response.IsSuccess = insertedId > 0; // Success if an ID was returned

                if (!response.IsSuccess)
                {
                    response.Message = "Teacher could not be created.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An unexpected error occurred while creating the teacher: {ex.Message}";
            }
            return response;
        }

        /// <summary>
        /// Updates an existing teacher.
        /// </summary>
        /// <param name="dto">The TeacherDTO containing the updated teacher's data.</param>
        /// <returns>A response containing the updated TeacherDTO.</returns>
        public async Task<Response<TeacherDTO>> UpdateTeacherAsync(TeacherDTO dto)
        {
            var response = new Response<TeacherDTO>();
            try
            {
                // Map the incoming DTO to the domain entity
                var teacherME = _mapper.Map<TeacherME>(dto);
                // Call the domain layer to update the teacher
                var updated = await _teacherDomain.UpdateTeacherAsync(teacherME);
                // Map the updated domain entity back to a DTO
                response.Data = _mapper.Map<TeacherDTO>(updated);
                response.IsSuccess = response.Data != null; // Success if data is not null

                if (!response.IsSuccess)
                {
                    response.Message = $"Teacher with ID {dto.TeacherId} not found for update or no changes were made.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while updating teacher with ID {dto.TeacherId}: {ex.Message}";
            }
            return response;
        }

        /// <summary>
        /// Deletes a teacher by their ID.
        /// </summary>
        /// <param name="teacherId">The ID of the teacher to delete.</param>
        /// <returns>A response indicating whether the deletion was successful.</returns>
        public async Task<Response<bool>> DeleteTeacherAsync(int teacherId)
        {
            var response = new Response<bool>();
            try
            {
                // Call the domain layer to delete the teacher
                response.Data = await _teacherDomain.DeleteTeacherAsync(teacherId);
                response.IsSuccess = response.Data; // Success if deletion was true

                if (!response.IsSuccess)
                {
                    response.Message = $"Teacher with ID {teacherId} could not be deleted or was not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while deleting teacher with ID {teacherId}: {ex.Message}";
            }
            return response;
        }
    }
}
