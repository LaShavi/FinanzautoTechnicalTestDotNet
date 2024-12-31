using AutoMapper;
using TechnicalTestDotNet.Core.DTOs.Teachers;

namespace TechnicalTestDotNet.Core.Helpers
{
    public class AutoMapperProfile : Profile
    {
        //public AutoMapperProfile()
        //{
        //    CreateMap<InputTeacherDTO, Teacher>()
        //    .ForMember(dest => dest.UserCreated, opt => opt.MapFrom(src => src.User))
        //    .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(_ => DateTime.Now)); // Captura la fecha actual      
        //}
    }
}
