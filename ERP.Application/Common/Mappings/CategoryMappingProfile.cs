using AutoMapper;
using ERP.Application.Common.Models.Categories.Queries.GetCategoryById;
using ERP.Domain.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Mappings
{
    public class CategoryMappingProfile: Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Categories, CategoryDto>()
                .ConstructUsing(src => new CategoryDto(
                    src.Id,
                    src.Name,
                    src.ParentCategoryId !=null ? src.ParentCategoryId : null,
                    src.ParentCategory != null ? src.ParentCategory.Name : null
                    ));
        }
    }
}
