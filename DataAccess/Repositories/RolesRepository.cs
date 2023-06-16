using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Entities;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly AccountsDbContext _context;
    private readonly IAccountsRepository _accountsRepository;

    public RolesRepository(AccountsDbContext context, IAccountsRepository accountsRepository)
    {
        _context = context;
        _accountsRepository = accountsRepository;
    }

    public async Task<long> CreateAsync(Role role)
    {
        if (role.IsAdmin)
        {
            throw new RoleOperationException("Can't create one more admin role");
        }

        await _context.AddAsync(role);
        await _context.SaveChangesAsync();

        return role.Id;
    }

    public Task<Role> GetByIdAsync(long id)
    {
        return _context
            .Queryable<Role>()
            .GetByIdAsync(id);
    }

    public Task<Role?> FindByIdAsync(long id)
    {
        return _context
            .Queryable<Role>()
            .FindByIdAsync(id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        var roles = await _context
            .Roles
            .Include(x => x.AccountRoles)
            .ToListAsync();

        return roles.Where(role => !role.IsAdmin);
    }

    public async Task RemoveAsync(Role role)
    {
        if (role.IsAdmin)
        {
            throw new RoleOperationException("Can't remove admin role");
        }

        var accountsWhereOnlyOneCurrentRole = await _accountsRepository.FindWhereOnlyOneRoleAsync(role.Id);

        if (accountsWhereOnlyOneCurrentRole.Count() != 0)
        {
            throw new RoleOperationException("Can't remove a role because some accounts have only this role");
        }

        _context.Remove(role);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Role role)
    {
        if (role.IsAdmin)
        {
            throw new RoleOperationException("Can't update admin role");
        }

        _context.Update(role);
        return _context.SaveChangesAsync();
    }

    public Task<List<Role>> FindAsync(IEnumerable<long> roleIds)
    {
        return _context
            .Roles
            .Where(x => roleIds.Contains(x.Id))
            .ToListAsync();
    }
}