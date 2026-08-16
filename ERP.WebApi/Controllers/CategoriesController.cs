using ERP.Application.Common.Models;
using ERP.Application.Common.Models.Categories.Commands.CreateCategory;
using ERP.Application.Common.Models.Categories.Commands.DeleteCategory;
using ERP.Application.Common.Models.Categories.Commands.UpdateCategory;
using ERP.Application.Common.Models.Categories.Queries.GetCategoriesList;
using ERP.Application.Common.Models.Categories.Queries.GetCategoryById;
using ERP.Application.Common.Models.Employee.Queries.GetEmployeeById;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ISender _sender;
        public CategoriesController(ISender sender)
        {
            _sender= sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetCategoryByIdQuery(id));
            return result.IsSuccess ? Ok(result) : NotFound(result.Errors);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var result = await _sender.Send(new GetCategoriesListQuery());
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if (id != command.Id)
                return BadRequest("Route ID and body ID do not match.");
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sender.Send(new DeleteCategoryCommand(id));
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

    }
}

