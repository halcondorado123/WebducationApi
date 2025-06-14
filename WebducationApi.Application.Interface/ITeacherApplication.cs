using LibraryProject.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;

namespace WebducationApi.Application.Interface
{
    public interface ITeacherApplication
    {
        Task<Response<IEnumerable<TeacherDTO>>> GetAllTeachersAsync();
        Task<Response<TeacherDTO>> GetTeacherByIdAsync(int teacherId);
        Task<ResponseGeneric<int>> CreateTeacherAsync(TeacherDTO dto);
        Task<Response<TeacherDTO>> UpdateTeacherAsync(TeacherDTO dto);
        Task<Response<bool>> DeleteTeacherAsync(int teacherId);
    }
}
