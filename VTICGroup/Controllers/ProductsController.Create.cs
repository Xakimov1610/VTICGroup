using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTICGroup.Entities;

namespace VTICGroup.Controllers;

public partial class ProductsController
{
    // GET: Products/Create
    [Authorize(Roles = "admin")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    // [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([Bind("Id,Title,Quantiy,Price")] Product product)
    {
        decimal vatAmount = decimal.Parse(_configuration.GetSection("VATAmount").Value);

        if (ModelState.IsValid)
        {
            product.TotalPrice = (product.Price * decimal.Parse(product.Quantiy)) * (1 + vatAmount);
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
}