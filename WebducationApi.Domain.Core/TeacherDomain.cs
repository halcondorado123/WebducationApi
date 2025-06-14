using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities; 
using WebducationApi.Domain.Interface; 
using WebducationApi.Infraestructure.Data; 
using WebducationApi.Infraestructure.Interface;

namespace WebducationApi.Domain.Core
{
    public class TeacherDomain : ITeacherDomain
    {
        private readonly ITeacherRepository _repository;

        public TeacherDomain(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TeacherME>> GetAllTeachersAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<TeacherME> GetTeacherByIdAsync(int id)
        {
            // Delegates the call to the underlying repository.
            return _repository.GetByIdAsync(id);
        }

        public Task<int> CreateTeacherAsync(TeacherME teacher)
        {
            return _repository.CreateTeacherAsync(teacher);
        }

        public Task<TeacherME> UpdateTeacherAsync(TeacherME teacher)
        {
            // Delegates the call to the underlying repository.
            return _repository.UpdateAsync(teacher);
        }

        public Task<bool> DeleteTeacherAsync(int id)
        {
            // Delegates the call to the underlying repository.
            return _repository.DeleteAsync(id);
        }
    }
}
