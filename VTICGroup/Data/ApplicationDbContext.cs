using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VTICGroup.Entities;

namespace VTICGroup.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Product> Product { get; set; } = default!;
}
