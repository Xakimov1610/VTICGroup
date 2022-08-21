using Microsoft.AspNetCore.Identity;

namespace VTICGroup.Controllers;

public partial class AccountController
{
    private readonly UserManager<IdentityUser> _userM;
    private readonly SignInManager<IdentityUser> _signInM;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<AccountController> logger)
    {
        _userM = userManager;
        _signInM = signInManager;
        _logger = logger;
    }
}