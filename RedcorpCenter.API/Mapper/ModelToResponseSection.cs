using RedcorpCenter.API.Response;
using RedcorpCenter.Infraestructure.Models;
using AutoMapper;
namespace RedcorpCenter.API.Mapper
{
    public class ModelToResponseSection : Profile
    {
        public ModelToResponseSection()
        {
            CreateMap<Section, SectionResponse>();

        }
    }
}
