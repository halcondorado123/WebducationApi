using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;

namespace WebducationApi.Domain.Interface
{
    public interface IStudentDomain
    {
        Task<IEnumerable<StudentME>> GetAllStudentsAsync();
        Task<StudentME> GetStudentByIdAsync(int id);
        Task<int> CreateStudentAsync(StudentME student);
        Task<StudentME> UpdateStudentAsync(StudentME student);
        Task<bool> DeleteStudentAsync(int id);
    }
}
