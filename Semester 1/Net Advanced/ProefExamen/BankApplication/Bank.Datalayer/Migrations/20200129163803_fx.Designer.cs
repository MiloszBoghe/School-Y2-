﻿// <auto-generated />
using Bank.Datalayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.Datalayer.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20200129163803_fx")]
    partial class fx
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bank.DomainClasses.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<int>("AccountType");

                    b.Property<decimal>("Balance");

                    b.Property<int>("CustomerId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Bank.DomainClasses.City", b =>
                {
                    b.Property<int>("ZipCode")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ZipCode");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Bank.DomainClasses.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CellPhone");

                    b.Property<string>("FirstName");

                    b.Property<string>("Name");

                    b.Property<int>("ZipCode");

                    b.HasKey("CustomerId");

                    b.HasIndex("ZipCode");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Bank.DomainClasses.Account", b =>
                {
                    b.HasOne("Bank.DomainClasses.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bank.DomainClasses.Customer", b =>
                {
                    b.HasOne("Bank.DomainClasses.City", "City")
                        .WithMany("Customers")
                        .HasForeignKey("ZipCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}