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
            CreateMap<Contato, CriarContatoVM>().ReverseMap();
            CreateMap<Contato, ContatoDTO>().ReverseMap();
            CreateMap<Contato, AtualizarContatoVM>().ReverseMap();
        }
    }
}
