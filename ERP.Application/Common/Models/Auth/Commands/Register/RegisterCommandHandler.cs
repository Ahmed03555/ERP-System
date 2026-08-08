using ERP.Domain.Entities.Auth___User;
using ERP.Domain.Interfaces;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ERP.Application.Common.Models.Auth.Commands.Register
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userRepository =  _unitOfWork.GetRepository<Users>();
            var emailExists = await userRepository
                .Query()
                .AnyAsync(u => u.Email == request.Email, cancellationToken);

            if (emailExists) 
                return Result<int>.Failure("Email is already registered.");
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new Users
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PhoneNumber = request.PhoneNumber,
                IsActive = true
            };
            await userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<int>.Success(user.Id);

        }
    }
}
