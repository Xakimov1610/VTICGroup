using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VTICGroup.Models;

namespace VTICGroup.Controllers;

public partial class AccountController
{
    public IActionResult Register(string returnUrl)
    {
        return View(new Register() { ReturnUrl  = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Register(Register model)
    {
        var user = new IdentityUser(model.Username);
        var result = await _userM.CreateAsync(user, model.Password);
        var roleResult = await _userM.AddToRoleAsync(user, "user");

        if(!result.Succeeded)
        {
            return BadRequest();
        }
        return LocalRedirect($"/account/login?returnUrl={model.ReturnUrl}");
    }
}