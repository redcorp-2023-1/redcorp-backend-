using RedcorpCenter.API.Request;
using RedcorpCenter.Infraestructure.Models;
using AutoMapper;
namespace RedcorpCenter.API.Mapper
{
    public class RequestToModelSection :Profile
    {
        public RequestToModelSection()
        {
            CreateMap<SectionResquest, Section>();
        }
    }
}
