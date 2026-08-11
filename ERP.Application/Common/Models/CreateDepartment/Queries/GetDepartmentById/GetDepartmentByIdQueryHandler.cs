using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, Result<DepartmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDepartmentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork
             .GetRepository<Departments>()
             .Query()
            .Include(d => d.Manager)
            .Where(d => d.Id == request.Id)
            .Select(d => new DepartmentDto(
             d.Id,
             d.Name,
             d.Manager != null ? d.Manager.JobTitle : null,
             d.ManagerId
              )).FirstOrDefaultAsync(cancellationToken);

            if (department is null)
                return Result<DepartmentDto>.Failure("Department not found.");
            return Result<DepartmentDto>.Success(department);
        }
    }
}
