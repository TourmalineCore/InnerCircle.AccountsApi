using System;
using System.Collections.Generic;
using System.Linq;
using Accounts.Application.Contracts;
using Accounts.Core.Contracts;
using System.Threading.Tasks;
using Accounts.Core.Entities;

namespace Accounts.Application.Roles.Commands
{
    public class RoleCreationCommand
    {
        public string Name { get; set; }

        public List<string> Permissions { get; set; }
    }

    public class RoleCreationCommandHandler : ICommandHandler<RoleCreationCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleCreationCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task Handle(RoleCreationCommand command)
        {
            var roles = await _roleRepository.GetRolesAsync();
            var rolesNames = roles.Select(x => x.Name).ToList();

            if (rolesNames.Contains(command.Name))
            {
                throw new ArgumentException($"Role with name [{command.Name}] already exists");
            }

            var permissions = command.Permissions.Select(permissionName => new Permission(permissionName));
            await _roleRepository.CreateAsync(new Role(command.Name, permissions));
        }
    }
}
