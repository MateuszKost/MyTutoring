﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(MyTutoringContext))]
    partial class MyTutoringContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataEntities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayOfWeek")
                        .HasMaxLength(1)
                        .HasColumnType("int");

                    b.Property<float>("EndTime")
                        .HasMaxLength(5)
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<float>("StartTime")
                        .HasMaxLength(5)
                        .HasColumnType("real");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("DataEntities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Homework");
                });

            modelBuilder.Entity("DataEntities.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FileSha1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HomeworkId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialGroupId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("MaterialGroupId");

                    b.HasIndex("MaterialTypeId");

                    b.ToTable("Material");
                });

            modelBuilder.Entity("DataEntities.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("MaterialType");
                });

            modelBuilder.Entity("DataEntities.MaterialsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("MaterialsGroup");
                });

            modelBuilder.Entity("DataEntities.MaterialsGroupVisibility", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaterialsGroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.HasKey("StudentId", "MaterialsGroupId");

                    b.HasIndex("MaterialsGroupId");

                    b.ToTable("MaterialsGroupVisibility");
                });

            modelBuilder.Entity("DataEntities.Student", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("DataEntities.StudentTeacher", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StudentId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("StudentTeacher");
                });

            modelBuilder.Entity("DataEntities.TaskSolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileSha1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.ToTable("TaskSolution");
                });

            modelBuilder.Entity("DataEntities.Teacher", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("int");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("DataEntities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailConfirmation")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DataEntities.UserIdentity", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserIdentity");
                });

            modelBuilder.Entity("DataEntities.UserRefreshToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("UserRefreshToken");
                });

            modelBuilder.Entity("DataEntities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("DataEntities.Activity", b =>
                {
                    b.HasOne("DataEntities.Student", "Student")
                        .WithMany("Activities")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_Activity_StudentId")
                        .IsRequired();

                    b.HasOne("DataEntities.Teacher", "Teacher")
                        .WithMany("Activities")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK_Activity_TeacherId")
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("DataEntities.Homework", b =>
                {
                    b.HasOne("DataEntities.Student", "Student")
                        .WithMany("Homeworks")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_Homework_StudentId")
                        .IsRequired();

                    b.HasOne("DataEntities.Teacher", "Teacher")
                        .WithMany("Homeworks")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK_Homework_TeacherId")
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("DataEntities.Material", b =>
                {
                    b.HasOne("DataEntities.Homework", null)
                        .WithMany("Materials")
                        .HasForeignKey("HomeworkId");

                    b.HasOne("DataEntities.MaterialsGroup", "MaterialGroup")
                        .WithMany("Materials")
                        .HasForeignKey("MaterialGroupId")
                        .HasConstraintName("FK_Material_MaterialGroupId")
                        .IsRequired();

                    b.HasOne("DataEntities.MaterialType", "MaterialType")
                        .WithMany("Materials")
                        .HasForeignKey("MaterialTypeId")
                        .HasConstraintName("FK_Material_MaterialTypeId")
                        .IsRequired();

                    b.Navigation("MaterialGroup");

                    b.Navigation("MaterialType");
                });

            modelBuilder.Entity("DataEntities.MaterialsGroupVisibility", b =>
                {
                    b.HasOne("DataEntities.MaterialsGroup", "MaterialsGroup")
                        .WithMany("MaterialsGroupVisibilities")
                        .HasForeignKey("MaterialsGroupId")
                        .HasConstraintName("FK_MaterialsGroupVisibility_MaterialsGroupId")
                        .IsRequired();

                    b.HasOne("DataEntities.Student", "Student")
                        .WithMany("MaterialsGroupVisibilities")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_MaterialsGroupVisibility_StudentId")
                        .IsRequired();

                    b.Navigation("MaterialsGroup");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataEntities.Student", b =>
                {
                    b.HasOne("DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataEntities.StudentTeacher", b =>
                {
                    b.HasOne("DataEntities.Student", "Student")
                        .WithMany("StudentsTeachers")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_StudentTeacher_StudentId")
                        .IsRequired();

                    b.HasOne("DataEntities.Teacher", "Teacher")
                        .WithMany("StudentsTeachers")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK_StudentTeacher_TeacherId")
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("DataEntities.TaskSolution", b =>
                {
                    b.HasOne("DataEntities.Homework", "Homework")
                        .WithMany("TaskSolutions")
                        .HasForeignKey("HomeworkId")
                        .HasConstraintName("FK_TaskSolution_HomeworkId")
                        .IsRequired();

                    b.Navigation("Homework");
                });

            modelBuilder.Entity("DataEntities.Teacher", b =>
                {
                    b.HasOne("DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataEntities.User", b =>
                {
                    b.HasOne("DataEntities.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_User_UserRoleId")
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("DataEntities.UserIdentity", b =>
                {
                    b.HasOne("DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataEntities.UserRefreshToken", b =>
                {
                    b.HasOne("DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataEntities.Homework", b =>
                {
                    b.Navigation("Materials");

                    b.Navigation("TaskSolutions");
                });

            modelBuilder.Entity("DataEntities.MaterialType", b =>
                {
                    b.Navigation("Materials");
                });

            modelBuilder.Entity("DataEntities.MaterialsGroup", b =>
                {
                    b.Navigation("Materials");

                    b.Navigation("MaterialsGroupVisibilities");
                });

            modelBuilder.Entity("DataEntities.Student", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Homeworks");

                    b.Navigation("MaterialsGroupVisibilities");

                    b.Navigation("StudentsTeachers");
                });

            modelBuilder.Entity("DataEntities.Teacher", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Homeworks");

                    b.Navigation("StudentsTeachers");
                });

            modelBuilder.Entity("DataEntities.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
