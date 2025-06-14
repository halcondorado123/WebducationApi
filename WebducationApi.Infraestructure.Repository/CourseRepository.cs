using Dapper;
using System.Data;
using WebducationApi.Domain.Entities;
using WebducationApi.Infraestructure.Data;
using WebducationApi.Infraestructure.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System; 

namespace WebducationApi.Infraestructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApiDbContext _context;

        public CourseRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseME>> GetAllAsync()
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_GET_COURSES]";

            var courses = await connection.QueryAsync<CourseME, TeacherME, CourseME>(
                procedure,
                (course, teacher) =>
                {
                    course.Teacher = teacher;
                    return course;
                },
                splitOn: "TeacherFirstName",
                commandType: CommandType.StoredProcedure);

            return courses;
        }

        public async Task<CourseME> GetByIdAsync(int id)
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_GET_COURSE_BY_ID]";

            var result = await connection.QueryAsync<CourseME, TeacherME, CourseME>(
                procedure,
                (course, teacher) =>
                {
                    course.Teacher = teacher;
                    return course;
                },
                new { CourseId = id },
                splitOn: "TeacherFirstName",
                commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
        }

        public async Task<int> CreateCourseAsync(CourseME course)
        {
            course.CreatedAt = DateTime.Now;
            course.UpdatedAt = DateTime.Now;
            course.UpdatedBy = course.CreatedBy;

            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_INSERT_COURSE]";

            var result = await connection.QuerySingleOrDefaultAsync<int>(
                procedure,
                new
                {
                    course.CourseName,
                    course.Credits,
                    course.TeacherId,
                    course.CreatedBy,
                    course.UpdatedBy
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<CourseME> UpdateAsync(CourseME course)
        {
            course.UpdatedAt = DateTime.Now;

            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_UPDATE_COURSE]";

            await connection.ExecuteAsync(
                procedure,
                new
                {
                    course.CourseId,
                    course.CourseName,
                    course.Credits,
                    course.TeacherId,
                    course.UpdatedBy
                },
                commandType: CommandType.StoredProcedure);

            return course;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _context.CreateSqlConnection();
            const string procedure = "[EDU].[SP_DELETE_COURSE]";

            var status = await connection.QuerySingleAsync<int>(
                procedure,
                new { CourseId = id },
                commandType: CommandType.StoredProcedure);

            return status == 1;
        }
    }
}
