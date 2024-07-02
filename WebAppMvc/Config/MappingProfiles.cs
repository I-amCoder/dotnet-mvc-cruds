using AutoMapper;
using WebAppMvc.Models;
using WebAppMvc.Models.Dtos;

namespace WebAppMvc.Config
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Category, CreateCategory>().ReverseMap();
        }
    }
}
