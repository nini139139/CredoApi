using System;
using Domain.Entity.LoanEntity;
using Domain.Entity.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repositor
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoanType> LoanType { get; set; }
        public virtual DbSet<Loans> Loans { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DEV;Database=Data;Trusted_Connection=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Loans>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.LoanType)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.LoanTypeId)
                    .HasConstraintName("FK_Loans_LoanType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Loans_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.DateOfBirthday).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.LastUpdateUser)
                    .WithMany(p => p.InverseLastUpdateUser)
                    .HasForeignKey(d => d.LastUpdateUserId)
                    .HasConstraintName("FK_Users_Users1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
