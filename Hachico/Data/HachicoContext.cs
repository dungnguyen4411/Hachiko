using Hachico.Models;
using Microsoft.EntityFrameworkCore;

namespace Hachico.Data
{
    public class HachicoContext : DbContext
    {
        public HachicoContext(DbContextOptions<HachicoContext> options)
            : base(options)
        { }
        public HachicoContext()
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetPermission>()
                .HasKey(c => new { c.PetId, c.UserId });
        }
        public DbSet<DeviceDetail> DeviceDetails { get; set; }
        public DbSet<PetStatus> PetStatuses { get; set; }
        public DbSet<Login> Logins {get; set;}
        public DbSet<Phone> Phones { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TypeAnimal> TypeAnimals { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PetPermission> PetPermissions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ImagePet> ImagePets { get; set; }

    }
}
