using DataEntities;

namespace DataAccessLayer
{
    public interface IUnitOfWork
    {
        IRepository<Activity> ActivityRepo { get; }
        IRepository<Homework> HomeworkRepo { get; }
        IRepository<Material> MaterialRepo { get; }
        IRepository<MaterialsGroup> MaterialsGroupRepo { get; }
        IRepository<MaterialsGroupVisibility> MaterialsGroupVisibilityRepo { get; }
        IRepository<MaterialType> MaterialTypeRepo { get; }
        IRepository<Student> StudentRepo { get; }
        IRepository<TaskSolution> TaskSolutionRepo { get; }
        IRepository<Tutor> TutorRepo { get; }
        IRepository<UserIdentity> UserIdentityRepo { get; }
        IRepository<UserRefreshToken> UserRefreshTokenRepo { get; }
        IRepository<User> UserRepo { get; }
        IRepository<UserRole> UserRoleRepo { get; }

        Task<int> CompleteAsync();
        void Dispose();
    }
}