using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FoodHubLogic.Models;

public partial class FoodhubContext : DbContext
{
    private readonly string connectionString;

    public FoodhubContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public FoodhubContext(DbContextOptions<FoodhubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<PromotionTicket> PromotionTickets { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=foodhub; Username=testuser; Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("promotions_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TotalQuota).HasDefaultValue(0);

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Promotions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("promotions_restaurant_id_fkey");
        });

        modelBuilder.Entity<PromotionTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("promotion_tickets_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PurchaseDate).HasDefaultValueSql("now()");
            entity.Property(e => e.Status).HasComment("Active, Used, Expired");

            entity.HasOne(d => d.Promotion).WithMany(p => p.PromotionTickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("promotion_tickets_promotion_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.PromotionTickets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("promotion_tickets_user_id_fkey");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("restaurants_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Manager).WithMany(p => p.Restaurants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("restaurants_manager_id_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reviews_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Rating).HasComment("1 to 5");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_restaurant_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");
            entity.Property(e => e.Role).HasComment("client, manager");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
