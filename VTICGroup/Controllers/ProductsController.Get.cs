using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VTICGroup.Controllers;

public partial class ProductsController
{
    // GET: Products
    public async Task<IActionResult> Index()
    {
        return _context.Product != null ? 
            View(await _context.Product.ToListAsync()) :
            Problem("Entity set 'WTICGroupContext.Product'  is null.");
    }

    // GET: Products/Details
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Product == null)
        {
            return NotFound();
        }

        var product = await _context.Product
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}