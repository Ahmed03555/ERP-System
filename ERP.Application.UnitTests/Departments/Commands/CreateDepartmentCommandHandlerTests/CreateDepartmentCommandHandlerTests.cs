using ERP.Application.Common.Interfaces;
using ERP.Application.Common.Models;
using ERP.Application.Common.Models.CreateDepartment.Commands;
using ERP.Domain.Entities.HR;
using ERP.Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ERP.Application.UnitTests.Departments.Commands;

public class CreateDepartmentCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IRepository<Domain.Entities.HR.Departments>> _departmentRepoMock;
    private readonly Mock<ICacheService> _cacheServiceMock;   
    private readonly CreateDepartmentCommandHandler _handler;

    public CreateDepartmentCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _departmentRepoMock = new Mock<IRepository<Domain.Entities.HR.Departments>>();
        _cacheServiceMock = new Mock<ICacheService>();  

        _unitOfWorkMock
            .Setup(u => u.GetRepository<Domain.Entities.HR.Departments>())
            .Returns(_departmentRepoMock.Object);

        _handler = new CreateDepartmentCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);   
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesDepartmentSuccessfully()
    {
   
        var command = new CreateDepartmentCommand("IT Department", null);

        _departmentRepoMock
            .Setup(r => r.Query())
            .Returns(new List<Domain.Entities.HR.Departments>().AsQueryable());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _departmentRepoMock.Verify(
            r => r.AddAsync(It.IsAny<Domain.Entities.HR.Departments>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            c => c.RemoveByPrefixAsync("departments:", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateName_ReturnsFailure()
    {
  
        var existingDepartments = new List<Domain.Entities.HR.Departments>
        {
            new() { Id = 1, Name = "IT Department" }
        };

        _departmentRepoMock
            .Setup(r => r.Query())
            .Returns(existingDepartments.AsQueryable());

        var command = new CreateDepartmentCommand("IT Department", null);

   
        var result = await _handler.Handle(command, CancellationToken.None);

  
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("A department with this name already exists.");
        _departmentRepoMock.Verify(
            r => r.AddAsync(It.IsAny<Domain.Entities.HR.Departments>(), It.IsAny<CancellationToken>()),
            Times.Never);

        _cacheServiceMock.Verify(
            c => c.RemoveByPrefixAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}