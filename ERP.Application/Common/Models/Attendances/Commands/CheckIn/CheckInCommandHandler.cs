using ERP.Application.Common.Interfaces;
using ERP.Domain.Entities.Enums;
using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Attendances.Commands.CheckIn
{
    public class CheckInCommandHandler : IRequestHandler<CheckInCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTime _dateTime;
        private static readonly TimeSpan WorkStartTime = new(9, 0, 0);
        private static readonly TimeSpan LateThreshold = new(9, 15, 0);
        public CheckInCommandHandler(IUnitOfWork unitOfWork, IDateTime dateTime)
        {
            _unitOfWork=unitOfWork;
            _dateTime = dateTime;
        }
        public async Task<Result<int>> Handle(CheckInCommand request, CancellationToken cancellationToken)
        {
            var EmployeeExists = await _unitOfWork.GetRepository<Employees>().ExistsAsync(request.EmployeeId, cancellationToken);

            if (!EmployeeExists)
                return Result<int>.Failure("Employee not found.");

            var now = _dateTime.UtcNow;
            var today = DateOnly.FromDateTime(now);

            var attendanceRepository = _unitOfWork.GetRepository<Attendance>();

            var alreadyCheckedIn = await attendanceRepository
                 .Query()
                 .AnyAsync(a => a.EmployeeId == request.EmployeeId && a.Date == today, cancellationToken);

            if (alreadyCheckedIn)
                return Result<int>.Failure("Employee has already checked in today.");

            var Status = now.TimeOfDay <= LateThreshold
                ? AttendanceStatus.Present : AttendanceStatus.Late;

            var attendance = new Attendance { 
                EmployeeId = request.EmployeeId,
                Date = today,

                CheckIn = now,
                Status = Status
            };

            await attendanceRepository.AddAsync(attendance, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(attendance.Id);
        }
    }
}
