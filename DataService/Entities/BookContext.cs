using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataService.Entities;

public partial class BookContext : DbContext
{
    public BookContext()
    {
    }

    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Book;User Id=test;Password=Test;TrustServerCertificate=True;Trusted_Connection=true;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.Author).HasMaxLength(200);
            entity.Property(e => e.CategoryId).HasMaxLength(400);
            entity.Property(e => e.Creator).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.PulishDate).HasColumnType("datetime");
            entity.Property(e => e.Pulisher)
                .HasMaxLength(200)
                .HasColumnName("pulisher");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Categories");

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Creator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_User");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.ToTable("Card");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.OrderId).HasMaxLength(400);
            entity.Property(e => e.StudentId).HasMaxLength(400);

            entity.HasOne(d => d.Student).WithMany(p => p.Cards)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Card_Student");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.BookId).HasMaxLength(400);
            entity.Property(e => e.CardId).HasMaxLength(400);
            entity.Property(e => e.DateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Book).WithMany(p => p.Histories)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Histories_Book1");

            entity.HasOne(d => d.Card).WithMany(p => p.Histories)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Histories_Card1");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.CardId).HasMaxLength(400);
            entity.Property(e => e.CitizenId).HasMaxLength(400);
            entity.Property(e => e.DatePay).HasColumnType("datetime");
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.PhoneNum).HasMaxLength(200);
            entity.Property(e => e.StudentCode).HasMaxLength(400);

            entity.HasOne(d => d.Card).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Card");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Username).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasMaxLength(400);
            entity.Property(e => e.Fullname).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(400);
            entity.Property(e => e.Username).HasMaxLength(400);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
