using AutoMapper;
using WebducationApi.Domain.Entities; // Add this namespace for StudentME

namespace WebducationApi.Tranversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<StudentME, StudentDTO>().ReverseMap();
            CreateMap<TeacherME, TeacherDTO>().ReverseMap();
        }
    }
}