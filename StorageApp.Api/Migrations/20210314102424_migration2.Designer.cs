// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StorageApp.Entities;

namespace StorageApp.Api.Migrations
{
    [DbContext(typeof(StorageContext))]
    [Migration("20210314102424_migration2")]
    partial class migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StorageApp.Entities.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Container");
                });

            modelBuilder.Entity("StorageApp.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContainerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("StorageApp.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("StorageApp.Entities.UserContainer", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ContainerId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ContainerId");

                    b.HasIndex("ContainerId");

                    b.ToTable("UserContainer");
                });

            modelBuilder.Entity("StorageApp.Entities.Item", b =>
                {
                    b.HasOne("StorageApp.Entities.Container", "Container")
                        .WithMany("Items")
                        .HasForeignKey("ContainerId");

                    b.Navigation("Container");
                });

            modelBuilder.Entity("StorageApp.Entities.UserContainer", b =>
                {
                    b.HasOne("StorageApp.Entities.Container", "Container")
                        .WithMany("UserContainers")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StorageApp.Entities.User", "User")
                        .WithMany("UserContainers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Container");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StorageApp.Entities.Container", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("UserContainers");
                });

            modelBuilder.Entity("StorageApp.Entities.User", b =>
                {
                    b.Navigation("UserContainers");
                });
#pragma warning restore 612, 618
        }
    }
}
