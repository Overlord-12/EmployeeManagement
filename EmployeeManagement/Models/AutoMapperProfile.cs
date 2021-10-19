using AutoMapper;
using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<DepartmentViewModel, Department>();
            CreateMap<Department, DepartmentViewModel>();
            CreateMap<Parameter, ParametrViewModel>();
            CreateMap<ParametrViewModel, Parameter>();
            CreateMap<EvaluationViewModel, Evaluation>();
            CreateMap<Evaluation, EvaluationViewModel>();
            CreateMap<Selection, SelectionViewModel>();
            CreateMap<SelectionViewModel, Selection>();
        }
    }
}
