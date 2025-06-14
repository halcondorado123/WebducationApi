using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;

namespace WebducationApi.Infraestructure.Interface
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<TeacherME>> GetAllAsync();
        Task<TeacherME> GetByIdAsync(int id);
        Task<int> CreateTeacherAsync(TeacherME teacher);
        Task<TeacherME> UpdateAsync(TeacherME teacher);
        Task<bool> DeleteAsync(int id);
    }
}
