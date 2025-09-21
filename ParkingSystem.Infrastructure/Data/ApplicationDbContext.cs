using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingSystem.Domain.Entities;
using ParkingSystem.Infrastructure.Identity;

namespace ParkingSystem.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<
    ApplicationUser, ApplicationRole, Guid,
    IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    public DbSet<ParkingSlots> ParkingSlots { get; set; }
    public DbSet<Bookings> Bookings { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<Fines> Fines { get; set; }
    public DbSet<SignalRConnection> SignalRConnections { get; set; }
}
