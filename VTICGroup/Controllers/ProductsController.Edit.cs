using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VTICGroup.Entities;

namespace VTICGroup.Controllers;

public partial class ProductsController
{
    // GET: Products/Edit
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Product == null)
        {
            return NotFound();
        }

        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    
    // POST: Products/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Quantiy,Price")] Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var username = _context.Users.FirstOrDefault(p => p.Id == userId).UserName;
                decimal vatAmount = decimal.Parse(_configuration.GetSection("VATAmount").Value);
                product.TotalPrice = (product.Price * decimal.Parse(product.Quantiy)) * (1 + vatAmount);
                product.UpdatedDate = DateTime.UtcNow;
                product.UpdatedBy = username;
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
    
    private bool ProductExists(int id)
    {
        return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}