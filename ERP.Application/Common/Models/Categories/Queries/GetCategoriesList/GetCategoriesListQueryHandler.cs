using AutoMapper;
using ERP.Application.Common.Models.Categories.Queries.GetCategoryById;
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

namespace ERP.Application.Common.Models.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IRequestHandler<GetCategoriesListQuery, Result<List<CategoryDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        public GetCategoriesListQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.GetRepository<Domain.Entities.Inventory.Categories>()
                .GetAllAsync(cancellationToken);


            if (category is null || !category.Any())
                return Result<List<CategoryDto>>.Success(new List<CategoryDto>());

            var categoryDto =   _mapper.Map<List<CategoryDto>>(category);

            return Result<List<CategoryDto>>.Success(categoryDto);
        }
    }
}
