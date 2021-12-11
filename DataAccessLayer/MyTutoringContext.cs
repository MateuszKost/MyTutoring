using DataEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class MyTutoringContext : DbContext, IMyTutoringContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MyTutoringContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MyTutoringContext(DbContextOptions Options) : base(Options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        { }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Homework> Homeworks { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MaterialsGroup> MaterialsGroups { get; set; }
        public virtual DbSet<MaterialsGroupVisibility> MaterialsGroupVisibilities { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentTutor> StudentsTutors { get; set; }
        public virtual DbSet<TaskSolution> TaskSolutions { get; set; }
        public virtual DbSet<Tutor> Tutors { get; set; }
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
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
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

                entity.HasOne(e => e.Tutor)
                    .WithMany(t => t.Activities)
                    .HasForeignKey(e => e.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Activity_TutorId");
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

                entity.HasOne(e => e.Tutor)
                    .WithMany(t => t.Homeworks)
                    .HasForeignKey(e => e.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Homework_TutorId");
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

            modelBuilder.Entity<StudentTutor>(entity =>
            {
                entity.ToTable("StudentTutor");

                entity.HasKey(e => new { e.StudentId, e.TutorId });

                entity.HasOne(e => e.Student)
                    .WithMany(s => s.StudentsTutors)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentTutor_StudentId");

                entity.HasOne(e => e.Tutor)
                    .WithMany(t => t.StudentsTutors)
                    .HasForeignKey(e => e.TutorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentTutor_TutorId");
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

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.ToTable("Tutor");

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

                entity.Property(e => e.Email)
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
