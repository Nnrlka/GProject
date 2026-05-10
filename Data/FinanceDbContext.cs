using GProject.Models;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GProject.Data;

public class FinanceDbContext : DbContext
{
    public DbSet<Transactions> Transactions => Set<Transactions>();

    public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transactions>(entity =>
        {
            entity.ToTable("transactions");
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id).HasColumnName("id");
            entity.Property(t => t.Type).HasColumnName("type").HasMaxLength(50);
            entity.Property(t => t.Description).HasColumnName("description").HasMaxLength(500);
            entity.Property(t => t.amount).HasColumnName("amount").HasColumnType("numeric(18,2)");
            entity.Property(t => t.Date).HasColumnName("date");
            entity.Property(t => t.Category).HasColumnName("category").HasMaxLength(100);
        });
    }
}
