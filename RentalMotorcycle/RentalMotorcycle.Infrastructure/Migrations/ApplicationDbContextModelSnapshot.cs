﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RentalMotorcycle.Infrastructure.DataContext;

#nullable disable

namespace RentalMotorcycle.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RentalMotorcycle.Domain.Models.Rental", b =>
                {
                    b.Property<int>("Identificador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Identificador"));

                    b.Property<DateTime?>("DataDevolucao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataPrevisaoTermino")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataTermino")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EntregadorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MotoId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Plano")
                        .HasColumnType("integer");

                    b.Property<bool>("Rented")
                        .HasColumnType("boolean");

                    b.Property<float?>("ValorDiaria")
                        .HasColumnType("real");

                    b.Property<decimal?>("ValorTotal")
                        .HasColumnType("numeric");

                    b.HasKey("Identificador");

                    b.ToTable("Rental");
                });
#pragma warning restore 612, 618
        }
    }
}
