using Core.Contracts;
using Core.Entities;
using Core.Exceptions;
using Core.Models;
using DataAccess;
using DataAccess.Repositories;
using Moq;
using Tests.TestsData;

namespace Tests.Roles;

public class AdminRoleTests
{
    private readonly Role _adminRole = new(BaseRoleNames.Admin, TestData.ValidPermissions);
    private readonly RolesRepository _rolesRepository;

    public AdminRoleTests()
    {
        _rolesRepository = new RolesRepository(new Mock<AccountsDbContext>().Object, new Mock<IAccountsRepository>().Object);
    }

    [Fact]
    public async Task CannotCreateOneMoreAdmin()
    {
        var exception = await Assert.ThrowsAsync<RoleOperationException>(() => _rolesRepository.CreateAsync(_adminRole));
        Assert.Equal("Can't create one more admin role", exception.Message);
    }

    [Fact]
    public async Task CannotEditAdmin()
    {
        var exception = await Assert.ThrowsAsync<RoleOperationException>(() => _rolesRepository.UpdateAsync(_adminRole));
        Assert.Equal("Can't update admin role", exception.Message);
    }

    [Fact]
    public async Task CannotRemoveAdmin()
    {
        var exception = await Assert.ThrowsAsync<RoleOperationException>(() => _rolesRepository.RemoveAsync(_adminRole));
        Assert.Equal("Can't remove admin role", exception.Message);
    }
}