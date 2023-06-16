using Core.Contracts;
using Core.Entities;
using Core.Exceptions;
using Core.Models;
using DataAccess;
using DataAccess.Repositories;
using Moq;
using Tests.TestsData;

namespace Tests.Roles;

public class RoleRemovingTests
{
    private readonly Mock<IAccountsRepository> _accountsRepositoryMock = new();
    private readonly Role _testRole = new(BaseRoleNames.Ceo, TestData.ValidPermissions);

    [Fact]
    public async Task CantRemoveRoleIfSeveralAccountsWithOnlyThisRole()
    {
        _accountsRepositoryMock
            .Setup(x => x.FindWhereOnlyOneRoleAsync(It.IsAny<long>()))
            .ReturnsAsync(new List<Account>
                    {
                        new Account("test1@tourmalinecore.com",
                                "test",
                                "test",
                                "test",
                                new List<Role>
                                {
                                    _testRole,
                                }
                            ),
                        new Account("test2@tourmalinecore.com",
                                "test",
                                "test",
                                "test",
                                new List<Role>
                                {
                                    _testRole,
                                }
                            ),
                    }
                );

        var rolesRepository = new RolesRepository(new Mock<AccountsDbContext>().Object, _accountsRepositoryMock.Object);
        var exception = await Assert.ThrowsAsync<RoleOperationException>(() => rolesRepository.RemoveAsync(_testRole));
        Assert.Equal("Can't remove a role because some accounts have only this role", exception.Message);
    }

    [Fact]
    public async Task CanRemoveRoleIfNoAccountsRelatedToThisRole()
    {
        _accountsRepositoryMock
            .Setup(x => x.FindWhereOnlyOneRoleAsync(It.IsAny<long>()))
            .ReturnsAsync(new List<Account>());

        var rolesRepository = new RolesRepository(new Mock<AccountsDbContext>().Object, _accountsRepositoryMock.Object);
        var exception = await Record.ExceptionAsync(() => rolesRepository.RemoveAsync(_testRole));
        Assert.Null(exception);
    }
}