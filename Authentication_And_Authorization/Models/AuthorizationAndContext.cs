using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Authentication_And_Authorization.Models;

public partial class AuthorizationAndContext : DbContext
{
    public AuthorizationAndContext()
    {
    }

    public AuthorizationAndContext(DbContextOptions<AuthorizationAndContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = MOBACK; Database=AuthorizationAnd; Integrated Security=true; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB99BF6FC18B");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmpPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EmpRole)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
