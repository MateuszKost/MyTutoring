using DataEntities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public interface IMyTutoringContext
    {
        DbSet<Activity> Activities { get; set; }
        DbSet<Homework> Homeworks { get; set; }
        DbSet<Material> Materials { get; set; }
        DbSet<MaterialsGroup> MaterialsGroups { get; set; }
        DbSet<MaterialsGroupVisibility> MaterialsGroupVisibilities { get; set; }
        DbSet<MaterialType> MaterialTypes { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<TaskSolution> TaskSolutions { get; set; }
        DbSet<Tutor> Tutors { get; set; }
        DbSet<UserIdentity> UserIdentities { get; set; }
        DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Dispose();
    }
}