using ERP.Application.Common.Interfaces;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Attendances.Commands.CheckOut
{
    public class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;

        public CheckOutCommandHandler(IUnitOfWork unitOfWork,IDateTime dateTime)
        {
            _unitOfWork=unitOfWork;
            _dateTime=dateTime;
        }
        public async Task<Result<bool>> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            var now = _dateTime.UtcNow;
            var today = DateOnly.FromDateTime(now);

            var attendanceRepository = _unitOfWork.GetRepository<Domain.Entities.HR.Attendance>();

            var attendance = await attendanceRepository
                .Query()
                .FirstOrDefaultAsync(a => a.EmployeeId == request.EmployeeId && a.Date == today, cancellationToken);

            if (attendance is null)
                return Result<bool>.Failure("No check-in record found for today. Please check in first.");

            if (attendance.CheckOut is not null)
                return Result<bool>.Failure("Employee has already checked out today.");

            attendance.CheckOut = now;

            attendanceRepository.UpdateAsync(attendance);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
