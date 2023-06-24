using AutoMapper;
using RedcorpCenter.API.Request;
using RedcorpCenter.Infra.Models;

namespace RedcorpCenter.API.Mapper
{
    public class RequestToModelSection : Profile
    {
        public RequestToModelSection()
        {
            CreateMap<SectionRequest, Section>();
        }
    }
}   
