using AutoMapper;
using RedcorpCenter.API.Request;
using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.API.Mapper
{
    public class RequestToModel :Profile
    {
        public RequestToModel()
        {
            CreateMap<EmployeeRequest, Employee>();
        }
    }
}
