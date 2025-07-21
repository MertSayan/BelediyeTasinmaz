using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class HobiContext:DbContext
    {
        public HobiContext(DbContextOptions<HobiContext> options) :base(options)
        {
                
        }

        public DbSet<PaymentInstallment> PaymentInstallments {  get; set; }
        public DbSet<Property> Properties {  get; set; }
        public DbSet<Rental> Rentals {  get; set; }
        public DbSet<User> Users {  get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>()
            .HasOne(r => r.CreatedByUser)
            .WithMany(u => u.RentalsCreated)
            .HasForeignKey(r => r.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
            .HasOne(p => p.CreatedByUser)
            .WithMany(u => u.PropertiesCreated)
            .HasForeignKey(p => p.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
            .Property(p => p.Type)
            .HasConversion<string>();

            modelBuilder.Entity<Property>()
            .Property(p => p.Status)
            .HasConversion<string>();
        }

    }
}
