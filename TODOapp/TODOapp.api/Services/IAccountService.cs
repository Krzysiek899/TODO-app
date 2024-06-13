using TODOapp.Models;

namespace TODOapp.api.Services;

public interface IAccountService
{
    Task<string> RegisterAsync(RegisterModel model);
    Task<AuthenticationModel> LoginAsync(TokenRequestModel model);
}