using System;
using System.Collections.Generic;
using APIProduct.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Data.Context;

public partial class MyAppDbContext : DbContext
{
    public MyAppDbContext()
    {
    }

    public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD4A567A2D");

            entity.ToTable("Product");

            entity.HasIndex(e => e.ProductName, "UQ__Product__DD5A978ABDAF84D2").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Created__7D439ABD");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A3C69593A");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B61600289303B").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C640EF043");

            entity.ToTable("User");

            entity.HasIndex(e => e.IdentityDocument, "UQ__User__9ACBD5A0FA5E7C98").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__User__A9D1053403E0E687").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IdentityDocument).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleId__797309D9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
