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
    public class StudentDomain : IStudentDomain
    {
            private readonly IStudentRepository _repository;

            public StudentDomain(IStudentRepository repository)
            {
                _repository = repository;
            }
            public Task<IEnumerable<StudentME>> GetAllStudentsAsync() => _repository.GetAllAsync();
            public Task<StudentME?> GetStudentByIdAsync(int id) => _repository.GetByIdAsync(id);
            public Task<int> CreateStudentAsync(StudentME student) =>_repository.CreateStudentAsync(student);
            public Task<StudentME> UpdateStudentAsync(StudentME student) => _repository.UpdateAsync(student);
            public Task<bool> DeleteStudentAsync(int id) => _repository.DeleteAsync(id);
    }
}