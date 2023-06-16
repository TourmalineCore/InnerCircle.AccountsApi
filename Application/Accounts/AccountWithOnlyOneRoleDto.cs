using Core.Entities;

namespace Application.Accounts;

public readonly struct AccountWithOnlyOneRoleDto
{
    public AccountWithOnlyOneRoleDto(Account account)
    {
        Id = account.Id;
        FullName = account.GetFullName();
    }

    public long Id { get; }

    public string FullName { get; }
}