using ClaimService.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClaimService.Infrastructure;

public class ClaimServiceDbContext : IdentityDbContext<IdentityUser>
{
    public ClaimServiceDbContext(DbContextOptions<ClaimServiceDbContext> options)
        : base(options) { }

    public DbSet<UserDbModel> Users { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<ClaimDbModel> Claims { get; set; }

    public DbSet<ReviewDbModel> Reviews { get; set; }
}
