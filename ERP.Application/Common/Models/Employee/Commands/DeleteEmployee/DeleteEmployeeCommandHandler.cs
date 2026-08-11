using ERP.Application.Common.Models;
using ERP.Application.Common.Models.Employee.Commands.DeleteEmployee;
using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using EmployeeEntity = ERP.Domain.Entities.HR.Employees; 

namespace ERP.Application.Modules.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeRepository = _unitOfWork.GetRepository<EmployeeEntity>();

      
        var employee = await employeeRepository.GetByIdAsync(request.Id, cancellationToken);

        if (employee is null)
            return Result<bool>.Failure("Employee not found.");

      
        var hasSubordinates = await employeeRepository
            .Query()
            .AnyAsync(e => e.ManagerId == request.Id, cancellationToken);

        if (hasSubordinates)
            return Result<bool>.Failure("Cannot delete an employee who is a manager to other employees. Reassign their subordinates first.");

    
        var isDepartmentManager = await _unitOfWork
            .GetRepository<Departments>()
            .Query()
            .AnyAsync(d => d.ManagerId == request.Id, cancellationToken);

        if (isDepartmentManager)
            return Result<bool>.Failure("Cannot delete an employee who is managing a department. Assign a new manager first.");

     
        employeeRepository.RemoveAsync(employee);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}