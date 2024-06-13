using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using TODOapp.Authentication;
using TODOapp.Models;

namespace TODOapp.Areas.Identity.Pages.Account;

public class LoginPageModel : PageModel
{
    private readonly HttpClient _httpClient;
    private readonly CustomAuthenticationStateProvider _stateProvider;

    public LoginPageModel(HttpClient httpClient, CustomAuthenticationStateProvider stateProvider)
    {
        _httpClient = httpClient;
        _stateProvider = stateProvider;
    }
    
    [BindProperty]
    public LoginModel Input { get; set; }   
    
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            HttpResponseMessage message = await _httpClient.PostAsJsonAsync("api/account/login", Input);

            if (message.IsSuccessStatusCode)
            {
                AuthenticationModel authResponse = JsonConvert.DeserializeObject<AuthenticationModel>(message.Content.ToString());
                string token = authResponse.Token;
                _stateProvider.MarkUserAsAuthenticated(token);
                return LocalRedirect("~/Dashboard");
            } //todo: obsługa błedów
        }

        return Page();
    }
}