using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities; // Assuming TeacherME is defined here

namespace WebducationApi.Domain.Interface
{
    // Defines the contract for data access operations related to teachers.
    public interface ITeacherDomain
    {
        Task<IEnumerable<TeacherME>> GetAllTeachersAsync();
        Task<TeacherME> GetTeacherByIdAsync(int id);
        Task<int> CreateTeacherAsync(TeacherME teacher);
        Task<TeacherME> UpdateTeacherAsync(TeacherME teacher);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
