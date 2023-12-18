﻿// <auto-generated />
using System;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.DataAccess.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20231218031304_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library.DataAccess.Entities.AdminEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("admins");
                });

            modelBuilder.Entity("Library.DataAccess.Entities.BookEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PublicationYear")
                        .HasColumnType("datetime2");

                    b.Property<int>("TakeBookId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.HasIndex("TakeBookId");

                    b.ToTable("books");
                });

            modelBuilder.Entity("Library.DataAccess.Entities.LibraryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("libraries");
                });

            modelBuilder.Entity("Library.DataAccess.Entities.TakeBookEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Overdue")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Refund")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.ToTable("takebooks");
                });

            modelBuilder.Entity("Library.DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LibraryId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TakeBookId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId")
                        .IsUnique();

                    b.HasIndex("LibraryId");

                    b.HasIndex("TakeBookId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Library.DataAccess.Entities.BookEntity", b =>
                {
                    b.HasOne("Library.DataAccess.Entities.TakeBookEntity", "TakeBook")
                        .WithMany("Books")
                        .HasForeignKey("TakeBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TakeBook");
                });

            modelBuilder.Entity("Library.DataAccess.Entities.UserEntity", b =>
                {
                    b.HasOne("Library.DataAccess.Entities.LibraryEntity", "Library")
                        .WithMany("Users")
                        .HasForeignKey("LibraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.DataAccess.Entities.TakeBookEntity", "TakeBook")
                        .WithMany("Users")
                        .HasForeignKey("TakeBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Library");

                    b.Navigation("TakeBook");
                });

            modelBuilder.Entity("Library.DataAccess.Entities.LibraryEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Library.DataAccess.Entities.TakeBookEntity", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
