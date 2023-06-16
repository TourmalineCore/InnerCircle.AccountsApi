using System;
using System.Collections.Generic;
using System.Linq;
using Core.Contracts;
using Core.Exceptions;
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

    public bool IsAdmin => AccountRoles.Count != 0 && AccountRoles.Exists(x => x.Role.IsAdmin);

    public Account(string corporateEmail, string firstName, string lastName, string? middleName, IEnumerable<Role> roles)
    {
        CorporateEmail = corporateEmail;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        IsBlocked = false;
        CreatedAt = SystemClock.Instance.GetCurrentInstant();

        ValidateRoles(roles);

        AccountRoles = roles
            .Select(role => new AccountRole
                    {
                        RoleId = role.Id,
                        Role = role,
                    }
                )
            .ToList();
    }

    public void Update(string firstName, string lastName, string? middleName, List<Role> roles, string callerCorporateEmail)
    {
        ValidateIsNotSelfOperation(callerCorporateEmail);
        ValidateRoles(roles);

        if (roles.Exists(role => role.IsAdmin))
        {
            throw new AccountOperationException("Can't set admin role");
        }

        if (IsAdmin)
        {
            throw new AccountOperationException("Can't edit admin");
        }

        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;

        AccountRoles = roles
            .Select(role => new AccountRole
                    {
                        RoleId = role.Id,
                        Role = role,
                    }
                )
            .ToList();
    }

    public void Block(string callerCorporateEmail)
    {
        ValidateIsNotSelfOperation(callerCorporateEmail);
        IsBlocked = true;
    }

    public void Unblock(string callerCorporateEmail)
    {
        ValidateIsNotSelfOperation(callerCorporateEmail);
        IsBlocked = false;
    }

    public string GetFullName()
    {
        return MiddleName != null 
            ? $"{LastName} {FirstName} {MiddleName}" 
            : $"{LastName} {FirstName}";
    }

    private void ValidateIsNotSelfOperation(string callerCorporateEmail)
    {
        if (CorporateEmail == callerCorporateEmail)
        {
            throw new AccountOperationException("The operation cannot be executed with own account");
        }
    }

    private static void ValidateRoles(IEnumerable<Role> roles)
    {
        if (!roles.Any())
        {
            throw new ArgumentException("Account roles can't be empty");
        }
    }

    private Account()
    {
    }
}