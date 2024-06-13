using TODOapp.api.Settings;
using TODOapp.Models;

namespace TODOapp.api.Services;

public interface IAccountService
{
    Task<string> RegisterAsync(RegisterModel model);
    Task<AuthenticationModel> LoginAsync(TokenRequestModel model);
    void SetTokensInsideCookie(AuthenticationModel auth, HttpContext context);
}