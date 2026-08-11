using ERP.Application.Common.Models;
using MediatR;

namespace ERP.Application.Common.Models.CreateDepartment.Commands;

public record CreateDepartmentCommand(
    string Name,
    int? ManagerId
) : IRequest<Result<int>>;