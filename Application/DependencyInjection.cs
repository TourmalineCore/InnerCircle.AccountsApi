using Application.Accounts.Commands;
using Application.Accounts.Queries;
using Application.Accounts.Validators;
using Application.HttpClients;
using Application.Roles.Commands;
using Application.Roles.Queries;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<AccountCreationCommandHandler>();
        services.AddTransient<AccountUpdateCommandHandler>();

        services.AddTransient<GetAccountsQueryHandler>();
        services.AddTransient<GetAccountByIdQueryHandler>();

        services.AddTransient<GetRolesQueryHandler>();
        services.AddTransient<GetRoleByIdQueryHandler>();
        services.AddTransient<RoleRemoveCommandHandler>();
        services.AddTransient<RoleCreationCommandHandler>();
        services.AddTransient<RoleUpdateCommandHandler>();

        services.AddTransient<GetPermissionsByAccountIdQueryHandler>();

        services.AddScoped<AccountCreationCommandValidator>();
        services.AddScoped<AccountUpdateCommandValidator>();
        services.AddTransient<IHttpClient, AccountHttpClient>();

        services.AddTransient<AccountBlockCommandHandler>();
        services.AddTransient<AccountUnblockCommandHandler>();

        services.AddTransient<FindAccountsWithOnlyOneRoleByRoleQueryHandler>();

        services.AddTransient<IClock, Clock>();

        return services;
    }
}