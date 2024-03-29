using System.Threading.Tasks;

namespace Application.HttpClients;

public interface IHttpClient
{
    Task SendRequestToRegisterNewAccountAsync(long accountId, string corporateEmail);

    Task SendRequestToCreateNewEmployeeAsync(string corporateEmail, string firstName, string lastName, string? middleName);

    Task SendRequestToBlockUserAsync(long accountId);

    Task SendRequestToUnblockUserAsync(long accountId);
}