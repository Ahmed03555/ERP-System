using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Common.Models.Attendances.Queries.GetAttendanceByEmployee
{
    public record GetAttendanceByEmployeeQuery(int EmployeeId) : IRequest<Result<List<AttendanceDto>>>;
}
