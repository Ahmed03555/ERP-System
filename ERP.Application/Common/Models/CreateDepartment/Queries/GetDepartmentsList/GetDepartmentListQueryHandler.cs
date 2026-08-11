using AutoMapper;
using ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentById;
using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.CreateDepartment.Queries.GetDepartmentsList
{
    public class GetDepartmentListQueryHandler : IRequestHandler<GetDepartmentsListQuery, Result<List<DepartmentDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetDepartmentListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
             _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<DepartmentDto>>> Handle(GetDepartmentsListQuery request, CancellationToken cancellationToken)
        {
            var depatmentRepo = await _unitOfWork.GetRepository<Departments>().GetAllAsync(cancellationToken);
            if (depatmentRepo is null || !depatmentRepo.Any())
            {
                return Result<List<DepartmentDto>>.Success(new List<DepartmentDto>());
            }

            var departmentDtos = _mapper.Map<List<DepartmentDto>>(depatmentRepo);

            return Result<List<DepartmentDto>>.Success(departmentDtos);


        }
    }
}
