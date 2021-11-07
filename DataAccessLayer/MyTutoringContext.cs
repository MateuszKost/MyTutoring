using DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DataAccessLayer
{
    public class MyTutoringContext : DbContext, IMyTutoringContext
    {
        public MyTutoringContext()
        { }
        public MyTutoringContext(DbContextOptions Options) : base(Options)
        { }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Homework> Homeworks { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MaterialsGroup> MaterialsGroups { get; set; }
        public virtual DbSet<MaterialsGroupVisibility> MaterialsGroupVisibilities { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentTeacher> StudentsTeachers { get; set; }
        public virtual DbSet<TaskSolution> TaskSolutions { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserIdentity> UserIdentities { get; set; }
        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                string connectionString = builder.Build().GetSection("ConnectionStrings").GetSection("MyTutoringDb").Value;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.HasAnnotation("Relational:Collation", "C.UTF-8");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.StartTime)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.EndTime)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.DayOfWeek)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.Activities)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_StudentId");

                entity.HasOne(e => e.Teacher)
                    .WithMany(t => t.Activities)
                    .HasForeignKey(e => e.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_TeacherId");
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.ToTable("Homework");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.StartTime)
                    .IsRequired();

                entity.Property(e => e.EndTime)
                    .IsRequired();

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.Homeworks)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Homework_StudentId");

                entity.HasOne(e => e.Teacher)
                    .WithMany(t => t.Homeworks)
                    .HasForeignKey(e => e.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Homework_TeacherId");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Description)
                     .IsRequired()
                     .HasMaxLength(500);

                entity.Property(e => e.FileSha1)
                    .IsRequired();

                entity.HasOne(e => e.MaterialType)
                    .WithMany(m => m.Materials)
                    .HasForeignKey(e => e.MaterialTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_MaterialTypeId");

                entity.HasOne(e => e.MaterialGroup)
                    .WithMany(m => m.Materials)
                    .HasForeignKey(e => e.MaterialGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_MaterialGroupId");
            });

            modelBuilder.Entity<MaterialsGroup>(entity =>
            {
                entity.ToTable("MaterialsGroup");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<MaterialsGroupVisibility>(entity =>
            {
                entity.ToTable("MaterialsGroupVisibility");

                entity.HasKey(e => new { e.StudentId, e.MaterialsGroupId });

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.MaterialsGroupVisibilities)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaterialsGroupVisibility_StudentId");

                entity.HasOne(e => e.MaterialsGroup)
                    .WithMany(m => m.MaterialsGroupVisibilities)
                    .HasForeignKey(e => e.MaterialsGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaterialsGroupVisibility_MaterialsGroupId");
            });

            modelBuilder.Entity<MaterialType>(entity =>
            {
                entity.ToTable("MaterialType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<StudentTeacher>(entity =>
            {
                entity.ToTable("StudentTeacher");

                entity.HasKey(e => new { e.StudentId, e.TeacherId });

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.StudentsTeachers)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentTeacher_StudentId");

                entity.HasOne(e => e.Teacher)
                    .WithMany(t => t.StudentsTeachers)
                    .HasForeignKey(e => e.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentTeacher_TeacherId");
            });

            modelBuilder.Entity<TaskSolution>(entity =>
            {
                entity.ToTable("TaskSolution");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.FileSha1)
                    .IsRequired();

                entity.HasOne(e => e.Homework)
                    .WithMany(h => h.TaskSolutions)
                    .HasForeignKey(e => e.HomeworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskSolution_HomeworkId");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.CreationDate)
                    .IsRequired();

                entity.HasOne(e => e.UserRole)
                    .WithMany(u => u.Users)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRoleId");
            });

            modelBuilder.Entity<UserIdentity>(entity =>
            {
                entity.ToTable("UserIdentity");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Salt)
                    .IsRequired();
            });

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.ToTable("UserRefreshToken");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Token)
                    .IsRequired();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);
            });
        }
    }
}
