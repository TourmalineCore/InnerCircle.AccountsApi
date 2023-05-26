using System.Collections.Generic;
using System.Linq;
using Core.Contracts;
using NodaTime;

namespace Core.Entities;

public class Account : IEntity
{
    public long Id { get; private set; }

    public string CorporateEmail { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string? MiddleName { get; private set; }

    public bool IsBlocked { get; private set; }

    public Instant CreatedAt { get; init; }

    public List<AccountRole> AccountRoles { get; private set; } = new();

    public Instant? DeletedAtUtc { get; private set; }

    public Account(string corporateEmail, string firstName, string lastName, string? middleName, IEnumerable<Role> roles)
    {
        CorporateEmail = corporateEmail;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        IsBlocked = false;
        CreatedAt = SystemClock.Instance.GetCurrentInstant();

        AccountRoles = roles
            .Select(role => new AccountRole
                    {
                        RoleId = role.Id,
                        Role = role,
                    }
                )
            .ToList();
    }

    public void Update(string firstName, string lastName, string? middleName, IEnumerable<long> roleIds)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;

        AccountRoles = roleIds
            .Select(roleId => new AccountRole
                    {
                        RoleId = roleId,
                    }
                )
            .ToList();
    }

    public void Block()
    {
        IsBlocked = true;
    }

    public void Unblock()
    {
        IsBlocked = false;
    }

    private Account()
    {
    }
}