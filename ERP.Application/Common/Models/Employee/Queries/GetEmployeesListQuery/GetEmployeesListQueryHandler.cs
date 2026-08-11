using AutoMapper;
using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById;
using ERP.Application.Common.Models.Employee.Queries.GetEmployeeById;
using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Employee.Queries.GetEmployeesListQuery
{
    public class GetEmployeesListQueryHandler : IRequestHandler<GetEmployeesListQuery, Result<List<EmployeeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployeesListQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }
        public async Task<Result<List<EmployeeDto>>> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork
            .GetRepository<Employees>()
            .Query()
            .Include(e => e.Departments)
            .Include(e => e.Manager)
            .ToListAsync(cancellationToken);

            if (employees is null || !employees.Any())
                return Result<List<EmployeeDto>>.Success(new List<EmployeeDto>());

            var mapping =  _mapper.Map<List<EmployeeDto>>(employees);

             return Result<List<EmployeeDto>>.Success(mapping);
        }
    }
}
