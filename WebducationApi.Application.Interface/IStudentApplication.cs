using LibraryProject.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebducationApi.Domain.Entities;

namespace WebducationApi.Application.Interface
{
    public interface IStudentApplication
    {
        Task<Response<IEnumerable<StudentDTO>>> GetAllStudentsAsync();
        Task<Response<IEnumerable<StudentDTO?>>> GetStudentByIdAsync(int studentId);
        Task<ResponseGeneric<int>> CreateStudentAsync(StudentDTO dto);
        Task<Response<StudentDTO?>> UpdateStudentAsync(StudentDTO dto);
        Task<Response<bool>> DeleteStudentAsync(int studentId);
    }
}
