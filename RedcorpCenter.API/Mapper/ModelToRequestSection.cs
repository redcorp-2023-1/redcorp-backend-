using AutoMapper;
using RedcorpCenter.API.Response;
using RedcorpCenter.Infraestructure.Models;

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
