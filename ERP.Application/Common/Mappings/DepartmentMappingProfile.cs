using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById;
using ERP.Domain.Entities.HR;

namespace ERP.Application.Common.Mappings
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<Departments, DepartmentDto>()
                .ConstructUsing(src => new DepartmentDto(
                    src.Id,
                    src.Name,
                    src.Manager != null ? src.Manager.FullName : null,
                    src.ManagerId
                ));
        }
    }
}
