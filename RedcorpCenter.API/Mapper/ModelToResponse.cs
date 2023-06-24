using AutoMapper;
using RedcorpCenter.API.Response;
using RedcorpCenter.Infraestructure.Models;

namespace RedcorpCenter.API.Mapper
{
    public class ModelToResponse :  Profile
    {
        public ModelToResponse()
        {
            CreateMap<Employee, EmployeeResponse>();
        }
    }
}
