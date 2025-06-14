using Dapper;
using System.Data;
using WebducationApi.Domain.Entities;
using WebducationApi.Infraestructure.Data;
using WebducationApi.Infraestructure.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebducationApi.Infraestructure.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApiDbContext _context; 
        public TeacherRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherME>> GetAllAsync()
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_GET_TEACHERS]";

            return await connection.QueryAsync<TeacherME>(
                procedure,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<TeacherME> GetByIdAsync(int id)
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_GET_TEACHER_BY_ID]";

            var result = await connection.QueryFirstOrDefaultAsync<TeacherME>(
                procedure,
                new { TeacherId = id },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<int> CreateTeacherAsync(TeacherME teacher)
        {
            teacher.UpdatedBy = teacher.CreatedBy;
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_INSERT_TEACHER]";

            var result = await connection.QuerySingleOrDefaultAsync<int>(
                procedure,
                new
                {
                    teacher.FirstName,
                    teacher.LastName,
                    teacher.SubjectArea, 
                    teacher.Email,
                    teacher.CreatedBy,
                    teacher.UpdatedBy
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<TeacherME> UpdateAsync(TeacherME teacher)
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_UPDATE_TEACHER]";

            await connection.ExecuteAsync(
                procedure,
                new
                {
                    teacher.TeacherId, 
                    teacher.LastName,
                    teacher.SubjectArea,
                    teacher.Email,
                    teacher.UpdatedBy
                },
                commandType: CommandType.StoredProcedure);

            return teacher;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_DELETE_TEACHER]";

            var status = await connection.QuerySingleAsync<int>(
                procedure,
                new { TeacherId = id },
                commandType: CommandType.StoredProcedure);

            return status == 1;
        }
    }
}
