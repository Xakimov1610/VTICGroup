using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VTICGroup.Models;

namespace VTICGroup.Controllers;

public partial class AccountController
{
    public IActionResult Login(string returnUrl)
    {
        return View(new Login() { ReturnUrl  = returnUrl });
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(Login model)
    {

        var user = await _userM.FindByNameAsync(model.Username);
        if (user is null)
        {
            return BadRequest();
        }
        
        var result = await _signInM.PasswordSignInAsync(user, model.Password, false, false);

        return LocalRedirect($"{model.ReturnUrl ?? "/"}");
    }
}