// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(MyTutoringContext))]
    [Migration("20211228203056_TaskSolutionUpdate")]
    partial class TaskSolutionUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataEntities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TutorId");

                    b.ToTable("Activity", (string)null);
                });

            modelBuilder.Entity("DataEntities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<float>("Grade")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TutorId");

                    b.ToTable("Homework", (string)null);
                });

            modelBuilder.Entity("DataEntities.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HomeworkId")
                        .HasColumnType("int");

                    b.Property<int?>("MaterialGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("MaterialTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("MaterialGroupId");

                    b.HasIndex("MaterialTypeId");

                    b.ToTable("Material", (string)null);
                });

            modelBuilder.Entity("DataEntities.MaterialsGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("MaterialsGroup", (string)null);
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

                    b.ToTable("MaterialsGroupVisibility", (string)null);
                });

            modelBuilder.Entity("DataEntities.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("MaterialType", (string)null);
                });

            modelBuilder.Entity("DataEntities.Student", b =>
                {
                    b.Property<Guid>("UserId")
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

                    b.HasKey("UserId");

                    b.ToTable("Student", (string)null);
                });

            modelBuilder.Entity("DataEntities.StudentTutor", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StudentId", "TutorId");

                    b.HasIndex("TutorId");

                    b.ToTable("StudentTutor", (string)null);
                });

            modelBuilder.Entity("DataEntities.TaskSolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.ToTable("TaskSolution", (string)null);
                });

            modelBuilder.Entity("DataEntities.Tutor", b =>
                {
                    b.Property<Guid>("UserId")
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

                    b.HasKey("UserId");

                    b.ToTable("Tutor", (string)null);
                });

            modelBuilder.Entity("DataEntities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<bool>("EmailConfirmation")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("DataEntities.UserIdentity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserIdentity", (string)null);
                });

            modelBuilder.Entity("DataEntities.UserRefreshToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserRefreshToken", (string)null);
                });

            modelBuilder.Entity("DataEntities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("UserRole", (string)null);
                });

            modelBuilder.Entity("DataEntities.Activity", b =>
                {
                    b.HasOne("DataEntities.Student", "Student")
                        .WithMany("Activities")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_Activity_StudentId");

                    b.HasOne("DataEntities.Tutor", "Tutor")
                        .WithMany("Activities")
                        .HasForeignKey("TutorId")
                        .IsRequired()
                        .HasConstraintName("FK_Activity_TutorId");

                    b.Navigation("Student");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("DataEntities.Homework", b =>
                {
                    b.HasOne("DataEntities.Student", "Student")
                        .WithMany("Homeworks")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_Homework_StudentId");

                    b.HasOne("DataEntities.Tutor", "Tutor")
                        .WithMany("Homeworks")
                        .HasForeignKey("TutorId")
                        .IsRequired()
                        .HasConstraintName("FK_Homework_TutorId");

                    b.Navigation("Student");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("DataEntities.Material", b =>
                {
                    b.HasOne("DataEntities.Homework", "Homework")
                        .WithMany("Materials")
                        .HasForeignKey("HomeworkId")
                        .HasConstraintName("FK_Material_HomeworkId");

                    b.HasOne("DataEntities.MaterialsGroup", "MaterialGroup")
                        .WithMany("Materials")
                        .HasForeignKey("MaterialGroupId")
                        .HasConstraintName("FK_Material_MaterialGroupId");

                    b.HasOne("DataEntities.MaterialType", "MaterialType")
                        .WithMany("Materials")
                        .HasForeignKey("MaterialTypeId")
                        .HasConstraintName("FK_Material_MaterialTypeId");

                    b.Navigation("Homework");

                    b.Navigation("MaterialGroup");

                    b.Navigation("MaterialType");
                });

            modelBuilder.Entity("DataEntities.MaterialsGroupVisibility", b =>
                {
                    b.HasOne("DataEntities.MaterialsGroup", "MaterialsGroup")
                        .WithMany("MaterialsGroupVisibilities")
                        .HasForeignKey("MaterialsGroupId")
                        .IsRequired()
                        .HasConstraintName("FK_MaterialsGroupVisibility_MaterialsGroupId");

                    b.HasOne("DataEntities.Student", "Student")
                        .WithMany("MaterialsGroupVisibilities")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_MaterialsGroupVisibility_StudentId");

                    b.Navigation("MaterialsGroup");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataEntities.Student", b =>
                {
                    b.HasOne("DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataEntities.StudentTutor", b =>
                {
                    b.HasOne("DataEntities.Student", "Student")
                        .WithMany("StudentsTutors")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK_StudentTutor_StudentId");

                    b.HasOne("DataEntities.Tutor", "Tutor")
                        .WithMany("StudentsTutors")
                        .HasForeignKey("TutorId")
                        .IsRequired()
                        .HasConstraintName("FK_StudentTutor_TutorId");

                    b.Navigation("Student");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("DataEntities.TaskSolution", b =>
                {
                    b.HasOne("DataEntities.Homework", "Homework")
                        .WithMany("TaskSolutions")
                        .HasForeignKey("HomeworkId")
                        .IsRequired()
                        .HasConstraintName("FK_TaskSolution_HomeworkId");

                    b.Navigation("Homework");
                });

            modelBuilder.Entity("DataEntities.Tutor", b =>
                {
                    b.HasOne("DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataEntities.User", b =>
                {
                    b.HasOne("DataEntities.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_User_UserRoleId");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("DataEntities.UserIdentity", b =>
                {
                    b.HasOne("DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataEntities.UserRefreshToken", b =>
                {
                    b.HasOne("DataEntities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataEntities.Homework", b =>
                {
                    b.Navigation("Materials");

                    b.Navigation("TaskSolutions");
                });

            modelBuilder.Entity("DataEntities.MaterialsGroup", b =>
                {
                    b.Navigation("Materials");

                    b.Navigation("MaterialsGroupVisibilities");
                });

            modelBuilder.Entity("DataEntities.MaterialType", b =>
                {
                    b.Navigation("Materials");
                });

            modelBuilder.Entity("DataEntities.Student", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Homeworks");

                    b.Navigation("MaterialsGroupVisibilities");

                    b.Navigation("StudentsTutors");
                });

            modelBuilder.Entity("DataEntities.Tutor", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Homeworks");

                    b.Navigation("StudentsTutors");
                });

            modelBuilder.Entity("DataEntities.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
