using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Accounts.Queries;

public readonly struct FindAccountsWithOnlyOneRoleByRoleQuery
{
    public long RoleId { get; init; }
}

public class FindAccountsWithOnlyOneRoleByRoleQueryHandler : IQueryHandler<FindAccountsWithOnlyOneRoleByRoleQuery, IEnumerable<AccountWithOnlyOneRoleDto>>
{
    private readonly IAccountsRepository _accountsRepository;

    public FindAccountsWithOnlyOneRoleByRoleQueryHandler(IAccountsRepository accountsRepository)
    {
        _accountsRepository = accountsRepository;
    }

    public async Task<IEnumerable<AccountWithOnlyOneRoleDto>> HandleAsync(FindAccountsWithOnlyOneRoleByRoleQuery query)
    {
        var accounts = await _accountsRepository.FindWhereOnlyOneRoleAsync(query.RoleId);
        return accounts.Select(x => new AccountWithOnlyOneRoleDto(x));
    }
}