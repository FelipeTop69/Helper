using AutoMapper;
using Entity.DTOs;
using Entity.DTOs.UserDTOs;
using Entity.DTOs.UserRoleDTOs;
using Entity.Models;


namespace Business
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper() 
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();


            CreateMap<UserRole, UserRoleDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ReverseMap();
            CreateMap<UserRole, UserRoleOptionsDTO>().ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => "🤡"))
                .ReverseMap();
            CreateMap<User, UserOptionsDTO>().ReverseMap();

        }
    }
}
