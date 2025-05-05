using Entity.DTOs;
using Entity.Models;
using AutoMapper;


namespace Business
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper() 
        {
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
