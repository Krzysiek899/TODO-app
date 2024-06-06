using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TODOapp.Models;

namespace TODOapp.Areas.Identity.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public RegisterModel(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    [BindProperty]
    public InputModel Input { get; set; }   
    
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var identity = new User { UserName = Input.User};
            var result = await _userManager.CreateAsync(identity, Input.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(identity, isPersistent: false);
                return LocalRedirect("~/");
            }
        }

        return Page();
    }
}