using Library.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace Library.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<LibraryEntity> Libraries { get; set; }
        public DbSet<TakeBookEntity> TakeBooks { get; set; }
        public DbSet<UserEntity> Users { get; set; }


        public LibraryDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Admins
            modelBuilder.Entity<AdminEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AdminEntity>().HasIndex(x => x.ExternalId).IsUnique();

            //Books
            modelBuilder.Entity<BookEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<BookEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<BookEntity>().HasOne(x => x.TakeBook)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.TakeBookId);

            //Libraries
            modelBuilder.Entity<LibraryEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<LibraryEntity>().HasIndex(x => x.ExternalId).IsUnique();

            //TakeBooks
            modelBuilder.Entity<TakeBookEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<TakeBookEntity>().HasIndex(x => x.ExternalId).IsUnique();

            //Users
            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<UserEntity>().HasOne(x => x.Library)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.LibraryId);
            modelBuilder.Entity<UserEntity>().HasOne(x => x.TakeBook)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.TakeBookId);
        }
    }
}
