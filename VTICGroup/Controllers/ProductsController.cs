using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VTICGroup.Data;

namespace VTICGroup.Controllers;

public partial class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userM;


    public ProductsController(ApplicationDbContext context,IConfiguration configuration,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _configuration = configuration;
        _userM = userManager;
    }
}