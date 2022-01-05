using DataEntities;

namespace DataAccessLayer.Mock
{
    public class MockUnitOfWork : IUnitOfWork
    {
        private readonly IMyTutoringContext _context;

        public MockUnitOfWork(IMyTutoringContext context)
        {
            _context = context;
        }

        public IRepository<Activity> ActivityRepo => new Repository<Activity>(_context.Activities);

        public IRepository<Homework> HomeworkRepo => new Repository<Homework>(_context.Homeworks);

        public IRepository<Material> MaterialRepo => new Repository<Material>(_context.Materials);

        public IRepository<MaterialsGroup> MaterialsGroupRepo => new Repository<MaterialsGroup>(_context.MaterialsGroups);

        public IRepository<MaterialsGroupVisibility> MaterialsGroupVisibilityRepo => new Repository<MaterialsGroupVisibility>(_context.MaterialsGroupVisibilities);

        public IRepository<MaterialType> MaterialTypeRepo => new Repository<MaterialType>(_context.MaterialTypes);

        public IRepository<Student> StudentRepo => new Repository<Student>(_context.Students);

        public IRepository<TaskSolution> TaskSolutionRepo => new Repository<TaskSolution>(_context.TaskSolutions);

        public IRepository<Tutor> TutorRepo => new Repository<Tutor>(_context.Tutors);

        public IRepository<UserIdentity> UserIdentityRepo => throw new NotImplementedException();

        public IRepository<UserRefreshToken> UserRefreshTokenRepo => throw new NotImplementedException();

        public IRepository<User> UserRepo => new Repository<User>(_context.Users);

        public IRepository<UserRole> UserRoleRepo => new Repository<UserRole>(_context.UserRoles);

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
