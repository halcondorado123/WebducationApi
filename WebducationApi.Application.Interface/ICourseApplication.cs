using LibraryProject.Transversal.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;

namespace WebducationApi.Application.Interface
{
    public interface ICourseApplication
    {
        Task<Response<IEnumerable<CourseDTO>>> GetAllCoursesAsync();
        Task<Response<CourseDTO>> GetCourseByIdAsync(int courseId);
        Task<ResponseGeneric<int>> CreateCourseAsync(CourseDTO dto);
        Task<Response<CourseDTO>> UpdateCourseAsync(CourseDTO dto);
        Task<Response<bool>> DeleteCourseAsync(int courseId);
    }
}