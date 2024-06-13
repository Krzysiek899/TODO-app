using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TODOapp.Models;

namespace TODOapp.Areas.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<User> _signInManager;

    public LoginModel(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }
    [BindProperty] public InputModel Input { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(Input.User, Input.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return LocalRedirect("~/dashboard");
            }
        }

        return Page();
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