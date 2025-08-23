using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentACar.DataContext.Entities;


namespace RentACar.DataContext;

public class AppDbContext:IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<CarImage> CarImages { get; set; }
    public DbSet<HeaderInfo> HeaderInfos { get; set; }
    public DbSet<FooterInfo> FooterInfos { get; set; }
    public DbSet<FooterLink> FooterLinks { get; set; }
    public DbSet<HomeHeroSection> HomeHeroSections { get; set; }
    public DbSet<HomeFeature> HomeFeatures { get; set; }
    public DbSet<HomeVehicle> HomeVehicles { get; set; } 
    public DbSet<HomeFaq> HomeFaqs { get; set; }
    public DbSet<HomeFeatureSection> HomeFeatureSections { get; set; }  
    public DbSet<TimelineStep> TimelineSteps { get; set; }
    public DbSet<VehicleFleetSetting> VehicleFleetSettings { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>().ToTable("AppUsers");
    }
}

