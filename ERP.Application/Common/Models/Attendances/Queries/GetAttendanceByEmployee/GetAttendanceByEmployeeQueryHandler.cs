using ERP.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Attendances.Queries.GetAttendanceByEmployee
{
    public class GetAttendanceByEmployeeQueryHandler : IRequestHandler<GetAttendanceByEmployeeQuery, Result<List<AttendanceDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAttendanceByEmployeeQueryHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<Result<List<AttendanceDto>>> Handle(GetAttendanceByEmployeeQuery request, CancellationToken cancellationToken)
        {
            var records = await _unitOfWork
                .GetRepository<Domain.Entities.HR.Attendance>()
                .Query()
                .Where(a => a.EmployeeId == request.EmployeeId)
                .OrderByDescending(a => a.Date)
                .Select(a => new AttendanceDto(a.Id, a.Date, a.CheckIn, a.CheckOut, a.Status.ToString()))
                .ToListAsync(cancellationToken);

            return Result<List<AttendanceDto>>.Success(records);
        }
    }
}
