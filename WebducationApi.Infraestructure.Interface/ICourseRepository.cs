using System.Collections.Generic;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;

namespace WebducationApi.Infraestructure.Interface
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseME>> GetAllAsync();
        Task<CourseME> GetByIdAsync(int id);
        Task<int> CreateCourseAsync(CourseME course);
        Task<CourseME> UpdateAsync(CourseME course);
        Task<bool> DeleteAsync(int id);
    }
}
