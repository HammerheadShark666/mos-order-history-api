﻿// <auto-generated />
using System;
using Microservice.Order.History.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Microservice.Order.History.Api.Migrations
{
    [DbContext(typeof(OrderHistoryDbContext))]
    [Migration("20240830142102_default-data")]
    partial class defaultdata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microservice.Order.History.Api.Domain.OrderHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressForename")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressLine3")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AddressSurname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("County")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("OrderPlaced")
                        .HasColumnType("date");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postcode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(19, 2)");

                    b.Property<string>("TownCity")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("MSOS_OrderHistory");

                    b.HasData(
                        new
                        {
                            Id = new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"),
                            AddressForename = "Test_Forename",
                            AddressLine1 = "AddressLine1",
                            AddressLine2 = "AddressLine2",
                            AddressLine3 = "AddressLine3",
                            AddressSurname = "Test_Surname",
                            Country = "England",
                            County = "West Yorkshire",
                            Created = new DateTime(2024, 8, 30, 15, 21, 2, 212, DateTimeKind.Local).AddTicks(1031),
                            CustomerId = new Guid("6c84d0a3-0c0c-435f-9ae0-4de09247ee15"),
                            OrderNumber = "000000001",
                            OrderPlaced = new DateOnly(2024, 8, 30),
                            OrderStatus = "Completed",
                            Postcode = "HD6 TRF4",
                            Total = 19.98m,
                            TownCity = "Leeds"
                        });
                });

            modelBuilder.Entity("Microservice.Order.History.Api.Domain.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(19, 2)");

                    b.HasKey("OrderId", "ProductId");

                    b.ToTable("MSOS_OrderHistory_OrderItem");

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"),
                            ProductId = new Guid("29a75938-ce2d-473b-b7fe-2903fe97fd6e"),
                            Name = "Infinity Kings",
                            ProductType = "Book",
                            Quantity = 1,
                            UnitPrice = 9.99m
                        },
                        new
                        {
                            OrderId = new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"),
                            ProductId = new Guid("37544155-da95-49e8-b7fe-3c937eb1de98"),
                            Name = "Wild Love",
                            ProductType = "Book",
                            Quantity = 1,
                            UnitPrice = 9.99m
                        });
                });

            modelBuilder.Entity("Microservice.Order.History.Api.Domain.OrderItem", b =>
                {
                    b.HasOne("Microservice.Order.History.Api.Domain.OrderHistory", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Microservice.Order.History.Api.Domain.OrderHistory", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
