using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;

namespace WebducationApi.Infraestructure.Interface
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentME>> GetAllAsync();
        Task<StudentME?> GetByIdAsync(int id);
        Task<int> CreateStudentAsync(StudentME student);
        Task<StudentME> UpdateAsync(StudentME student);
        Task<bool> DeleteAsync(int id);
    }
}
