﻿using AutoMapper;
using RedcorpCenter.API.Response;
using RedcorpCenter.Infra.Models;

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
