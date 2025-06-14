using AutoMapper;
using LibraryProject.Transversal.Common;
using WebducationApi.Application.Interface;
using WebducationApi.Domain.Entities;
using WebducationApi.Domain.Interface;

public class StudentApplication : IStudentApplication
{
    private readonly IStudentDomain _studentDomain;
    private readonly IMapper _mapper;

    public StudentApplication(IStudentDomain studentDomain, IMapper mapper)
    {
        _studentDomain = studentDomain;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<StudentDTO>>> GetAllStudentsAsync()
    {
        var response = new Response<IEnumerable<StudentDTO>>();
        try
        {
            var students = await _studentDomain.GetAllStudentsAsync();
            response.Data = _mapper.Map<IEnumerable<StudentDTO>>(students);
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<Response<IEnumerable<StudentDTO?>>> GetStudentByIdAsync(int studentId)
    {
        var response = new Response<IEnumerable<StudentDTO?>>();
        try
        {
            var student = await _studentDomain.GetStudentByIdAsync(studentId);
            response.Data = new List<StudentDTO?> { _mapper.Map<StudentDTO?>(student) };
            response.IsSuccess = true;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<ResponseGeneric<int>> CreateStudentAsync(StudentDTO dto)
    {
        var response = new ResponseGeneric<int>();
        try
        {
            var studentME = _mapper.Map<StudentME>(dto);
            var insertedId = await _studentDomain.CreateStudentAsync(studentME);

            response.Data = insertedId;
            response.IsSuccess = insertedId > 0;

            if (!response.IsSuccess)
            {
                response.Message = "Student could not be created.";
            }
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "An unexpected error occurred: " + ex.Message;
        }

        return response;
    }


    public async Task<Response<StudentDTO?>> UpdateStudentAsync(StudentDTO dto)
    {
        var response = new Response<StudentDTO?>();
        try
        {
            var me = _mapper.Map<StudentME>(dto);
            var updated = await _studentDomain.UpdateStudentAsync(me);
            response.Data = _mapper.Map<StudentDTO?>(updated);
            response.IsSuccess = response.Data != null;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }

    public async Task<Response<bool>> DeleteStudentAsync(int studentId)
    {
        var response = new Response<bool>();
        try
        {
            response.Data = await _studentDomain.DeleteStudentAsync(studentId);
            response.IsSuccess = response.Data;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
        }
        return response;
    }
}
