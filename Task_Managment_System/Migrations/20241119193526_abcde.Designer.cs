﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Task_Managment_System.Data;

#nullable disable

namespace Task_Managment_System.Migrations
{
    [DbContext(typeof(TaskDBContext))]
    [Migration("20241119193526_abcde")]
    partial class abcde
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Task_Managment_System.Model.Entities.Registration", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskItemTask_Id")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("TaskItemTask_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Task_Managment_System.Model.Entities.TaskItem", b =>
                {
                    b.Property<int>("Task_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Task_Id"));

                    b.Property<string>("Assign_To")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Task_Id");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("Task_Managment_System.Model.Entities.Registration", b =>
                {
                    b.HasOne("Task_Managment_System.Model.Entities.TaskItem", "TaskItem")
                        .WithMany()
                        .HasForeignKey("TaskItemTask_Id");

                    b.Navigation("TaskItem");
                });
#pragma warning restore 612, 618
        }
    }
}
