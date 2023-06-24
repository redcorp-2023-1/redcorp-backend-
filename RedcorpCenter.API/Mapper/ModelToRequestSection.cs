using AutoMapper;
using RedcorpCenter.API.Response;
using RedcorpCenter.Infra.Models;

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
