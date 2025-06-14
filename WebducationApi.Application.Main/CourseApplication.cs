using AutoMapper;
using LibraryProject.Transversal.Common;
using WebducationApi.Application.Interface;
using WebducationApi.Domain.Entities;
using WebducationApi.Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebducationApi.Application.Main
{
    public class CourseApplication : ICourseApplication
    {
        private readonly ICourseDomain _courseDomain;
        private readonly IMapper _mapper;

        // Constructor for dependency injection.
        public CourseApplication(ICourseDomain courseDomain, IMapper mapper)
        {
            _courseDomain = courseDomain;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CourseDTO>>> GetAllCoursesAsync()
        {
            var response = new Response<IEnumerable<CourseDTO>>();
            try
            {
                var courses = await _courseDomain.GetAllCoursesAsync();
                response.Data = _mapper.Map<IEnumerable<CourseDTO>>(courses);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while retrieving all courses: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<CourseDTO>> GetCourseByIdAsync(int courseId)
        {
            var response = new Response<CourseDTO>();
            try
            {
                var course = await _courseDomain.GetCourseByIdAsync(courseId);
                response.Data = _mapper.Map<CourseDTO>(course);
                response.IsSuccess = response.Data != null;
                if (!response.IsSuccess)
                {
                    response.Message = $"Course with ID {courseId} not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while retrieving course with ID {courseId}: {ex.Message}";
            }
            return response;
        }

        public async Task<ResponseGeneric<int>> CreateCourseAsync(CourseDTO dto)
        {
            var response = new ResponseGeneric<int>();
            try
            {
                var courseME = _mapper.Map<CourseME>(dto);
                var insertedId = await _courseDomain.CreateCourseAsync(courseME);

                response.Data = insertedId;
                response.IsSuccess = insertedId > 0;

                if (!response.IsSuccess)
                {
                    response.Message = "Course could not be created.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An unexpected error occurred while creating the course: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<CourseDTO>> UpdateCourseAsync(CourseDTO dto)
        {
            var response = new Response<CourseDTO>();
            try
            {
                var courseME = _mapper.Map<CourseME>(dto);
                var updated = await _courseDomain.UpdateCourseAsync(courseME);
                response.Data = _mapper.Map<CourseDTO>(updated);
                response.IsSuccess = response.Data != null;

                if (!response.IsSuccess)
                {
                    response.Message = $"Course with ID {dto.CourseId} not found for update or no changes were made.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while updating course with ID {dto.CourseId}: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<bool>> DeleteCourseAsync(int courseId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _courseDomain.DeleteCourseAsync(courseId);
                response.IsSuccess = response.Data;

                if (!response.IsSuccess)
                {
                    response.Message = $"Course with ID {courseId} could not be deleted or was not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"An error occurred while deleting course with ID {courseId}: {ex.Message}";
            }
            return response;
        }
    }
}
