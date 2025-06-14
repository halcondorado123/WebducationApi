using System.Collections.Generic;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;
using WebducationApi.Domain.Interface;
using WebducationApi.Infraestructure.Interface;

namespace WebducationApi.Domain.Core
{
    public class CourseDomain : ICourseDomain
    {
        private readonly ICourseRepository _repository;
        public CourseDomain(ICourseRepository repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<CourseME>> GetAllCoursesAsync() => _repository.GetAllAsync();
        public Task<CourseME> GetCourseByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<int> CreateCourseAsync(CourseME course) => _repository.CreateCourseAsync(course);
        public Task<CourseME> UpdateCourseAsync(CourseME course) => _repository.UpdateAsync(course);
        public Task<bool> DeleteCourseAsync(int id) => _repository.DeleteAsync(id);
    }
}
