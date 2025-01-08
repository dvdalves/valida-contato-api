using AutoMapper;
using ValidaContatoApi.Business.DTO;
using ValidaContatoApi.Business.ViewModels;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Configurations
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Contact, CreateContactVM>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Contact, UpdateContactVM>().ReverseMap();
        }
    }
}
