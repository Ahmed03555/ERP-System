using AutoMapper;
using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById;
using ERP.Application.Common.Models.Employee.Queries.GetEmployeeById;
using ERP.Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Mappings
{
    public class EmployeesMappingProfile :Profile
    {
        public EmployeesMappingProfile()
        {
            CreateMap<Employees, EmployeeDto>()
                .ConstructUsing(src => new EmployeeDto(
                    src.Id,
                src.FullName,
                src.JobTitle,
                src.Salary,
                src.HireDate,
                src.DepartmentId,
                src.Departments != null ? src.Departments.Name : null,
                src.ManagerId,
                src.Manager != null ? src.Manager.FullName : null
                    ));
        }
    }
}
