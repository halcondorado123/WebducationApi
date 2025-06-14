using System.Collections.Generic;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;

namespace WebducationApi.Domain.Interface
{
    public interface ICourseDomain
    {
        Task<IEnumerable<CourseME>> GetAllCoursesAsync();
        Task<CourseME> GetCourseByIdAsync(int id);
        Task<int> CreateCourseAsync(CourseME course);
        Task<CourseME> UpdateCourseAsync(CourseME course);
        Task<bool> DeleteCourseAsync(int id);
    }
}
