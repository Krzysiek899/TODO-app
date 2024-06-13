using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TODOapp.Models;

namespace TODOapp.Areas.Identity.Pages.Account;

public class RegisterPageModel : PageModel
{
    private readonly HttpClient _httpClient;
    public RegisterPageModel(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    [BindProperty]
    public RegisterModel Input { get; set; }   
    
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            HttpResponseMessage message = await _httpClient.PostAsJsonAsync("api/account/register", Input);

            if (message.IsSuccessStatusCode)
            { 
                return LocalRedirect("~/Identity/Account/Login");
            } //todo: obsługa błedów
        }

        return Page();
    }
}