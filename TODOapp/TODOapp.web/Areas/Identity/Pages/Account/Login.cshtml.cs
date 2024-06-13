using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TODOapp.Models;

namespace TODOapp.Areas.Identity.Pages.Account;


private LoginModel loginModel = new LoginModel();

private async Task HandleLogin()
{
    var response = await WebRequestMethods.Http.PostAsJsonAsync("api/auth/login", loginModel);

    if (response.IsSuccessStatusCode)
    {
        var token = await response.Content.ReadAsStringAsync();
        await AuthenticationStateProvider.MarkUserAsAuthenticated(token);
        Navigation.NavigateTo("/");
    }
    else
    {
        // Obsługa błędów
    }
}

public class InputModel
{
    [Required]
    public string User { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}