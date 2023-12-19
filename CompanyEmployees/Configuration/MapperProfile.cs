using AutoMapper;
using Entities;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<Company, CompanyDto>()
                .ForCtorParam("fullAddress",
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<Employee, EmployeeDto>();
        }
    }
}
