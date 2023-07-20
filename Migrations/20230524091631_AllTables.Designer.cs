﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WEBAPI_E2.Data;

#nullable disable

namespace WEBAPI_E2.Migrations
{
    [DbContext(typeof(WEBAPI_E2DbContext))]
    [Migration("20230524091631_AllTables")]
    partial class AllTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WEBAPI_E2.Models.CategoryModel", b =>
                {
                    b.Property<long>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CategoryId"));

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

                    b.Property<long>("LastModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("Varchar(50)");

                    b.Property<long>("Parent")
                        .HasColumnType("bigint");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.LocationModel", b =>
                {
                    b.Property<long>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("LocationId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.ProductModel", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<long>("Category")
                        .HasColumnType("bigint");

                    b.Property<long?>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<DateTime?>("ExpDate")
                        .HasColumnType("Date")
                        .HasColumnName("ExpiryDate");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long>("LastModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("Date");

                    b.Property<string>("Made")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)")
                        .HasColumnName("Make");

                    b.Property<DateTime?>("ManDate")
                        .HasColumnType("Date")
                        .HasColumnName("ManufactureDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.StockModel", b =>
                {
                    b.Property<long>("StockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("StockId"));

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("Date");

                    b.Property<long>("LastModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("Date");

                    b.Property<long>("LocationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.HasKey("StockId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.UserModel", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserId"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.ProductModel", b =>
                {
                    b.HasOne("WEBAPI_E2.Models.CategoryModel", "Cat")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Cat");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.StockModel", b =>
                {
                    b.HasOne("WEBAPI_E2.Models.LocationModel", "Location")
                        .WithMany("Stocks")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WEBAPI_E2.Models.ProductModel", "Product")
                        .WithMany("Stock")
                        .HasForeignKey("ProductId");

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.CategoryModel", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.LocationModel", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("WEBAPI_E2.Models.ProductModel", b =>
                {
                    b.Navigation("Stock");
                });
#pragma warning restore 612, 618
        }
    }
}
