using Microsoft.AspNetCore.Mvc;
using TODOapp.api.Services;
using TODOapp.Models;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _userService;
    public AccountController(IAccountService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterModel model)
    {

        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
    {
        var result = await _userService.LoginAsync(model);
        return Ok(result);
    }
}
