﻿// <auto-generated />
using System;
using DotNetCourseNew.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNetCourseNew.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    [Migration("20220629110207_UserAdnRoleAdd")]
    partial class UserAdnRoleAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DotNetCourseNew.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool?>("HasDelivery")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Nationality")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Dish", b =>
                {
                    b.HasOne("DotNetCourseNew.Entities.Restaurant", "Restaurant")
                        .WithMany("Dishes")
                        .HasForeignKey("RestaurantId");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Restaurant", b =>
                {
                    b.HasOne("DotNetCourseNew.Entities.Address", "Address")
                        .WithOne("Restaurant")
                        .HasForeignKey("DotNetCourseNew.Entities.Restaurant", "AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.User", b =>
                {
                    b.HasOne("DotNetCourseNew.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Address", b =>
                {
                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("DotNetCourseNew.Entities.Restaurant", b =>
                {
                    b.Navigation("Dishes");
                });
#pragma warning restore 612, 618
        }
    }
}
