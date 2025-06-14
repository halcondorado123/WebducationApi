using Dapper;
using System.Data;
using WebducationApi.Domain.Entities;
using WebducationApi.Infraestructure.Data;
using WebducationApi.Infraestructure.Interface;

namespace WebducationApi.Infraestructure.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApiDbContext _context;

        public StudentRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentME>> GetAllAsync()
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_GET_STUDENTS]";

            return await connection.QueryAsync<StudentME>(
                procedure,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<StudentME?> GetByIdAsync(int id)
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_GET_STUDENT_BY_ID]";

            var result = await connection.QueryFirstOrDefaultAsync<StudentME>(
                procedure,
                new { StudentId = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<int> CreateStudentAsync(StudentME student)
        {
            student.UpdatedBy = student.CreatedBy;

            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_INSERT_STUDENT]";

            var result = await connection.QuerySingleOrDefaultAsync<int>(
                procedure,
                new
                {
                    student.FirstName,
                    student.LastName,
                    student.DateOfBirth,
                    student.Email,
                    student.CreatedBy,
                    student.UpdatedBy
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }



        public async Task<StudentME> UpdateAsync(StudentME student)
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_UPDATE_STUDENT]";

            await connection.ExecuteAsync(
                procedure,
                new
                {
                    student.StudentId,
                    student.FirstName,
                    student.LastName,
                    student.DateOfBirth,
                    student.Email,
                    student.UpdatedBy
                },
                commandType: CommandType.StoredProcedure);

            return student;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_DELETE_STUDENT]";

            var status = await connection.QuerySingleAsync<int>(
                procedure,
                new { StudentId = id },
                commandType: CommandType.StoredProcedure);

            return status == 1;
        }
    }
}