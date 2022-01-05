using DataEntities;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMyTutoringContext _myTutoringContext;

        private IRepository<Activity>? _activityRepo;
        public IRepository<Activity> ActivityRepo
        {
            get
            {
                if (_activityRepo == null)
                {
                    _activityRepo = new Repository<Activity>(_myTutoringContext.Activities);
                }
                return _activityRepo;
            }
        }

        private IRepository<Homework>? _homeworkRepo;
        public IRepository<Homework> HomeworkRepo
        {
            get
            {
                if (_homeworkRepo == null)
                {
                    _homeworkRepo = new Repository<Homework>(_myTutoringContext.Homeworks);
                }
                return _homeworkRepo;
            }
        }

        private IRepository<Material>? _materialRepo;
        public IRepository<Material> MaterialRepo
        {
            get
            {
                if (_materialRepo == null)
                {
                    _materialRepo = new Repository<Material>(_myTutoringContext.Materials);
                }
                return _materialRepo;
            }
        }

        private IRepository<MaterialsGroup>? _materialsGroupRepo;
        public IRepository<MaterialsGroup> MaterialsGroupRepo
        {
            get
            {
                if (_materialsGroupRepo == null)
                {
                    _materialsGroupRepo = new Repository<MaterialsGroup>(_myTutoringContext.MaterialsGroups);
                }
                return _materialsGroupRepo;
            }
        }

        private IRepository<MaterialsGroupVisibility>? _materialsGroupVisibilityRepo;
        public IRepository<MaterialsGroupVisibility> MaterialsGroupVisibilityRepo
        {
            get
            {
                if (_materialsGroupVisibilityRepo == null)
                {
                    _materialsGroupVisibilityRepo = new Repository<MaterialsGroupVisibility>(_myTutoringContext.MaterialsGroupVisibilities);
                }
                return _materialsGroupVisibilityRepo;
            }
        }

        private IRepository<MaterialType>? _materialTypeRepo;
        public IRepository<MaterialType> MaterialTypeRepo
        {
            get
            {
                if (_materialTypeRepo == null)
                {
                    _materialTypeRepo = new Repository<MaterialType>(_myTutoringContext.MaterialTypes);
                }
                return _materialTypeRepo;
            }
        }

        private IRepository<Student>? _studentRepo;
        public IRepository<Student> StudentRepo
        {
            get
            {
                if (_studentRepo == null)
                {
                    _studentRepo = new Repository<Student>(_myTutoringContext.Students);
                }
                return _studentRepo;
            }
        }               

        private IRepository<TaskSolution>? _taskSolutionRepo;
        public IRepository<TaskSolution> TaskSolutionRepo
        {
            get
            {
                if (_taskSolutionRepo == null)
                {
                    _taskSolutionRepo = new Repository<TaskSolution>(_myTutoringContext.TaskSolutions);
                }
                return _taskSolutionRepo;
            }
        }

        private IRepository<Tutor>? _tutorRepo;
        public IRepository<Tutor> TutorRepo
        {
            get
            {
                if (_tutorRepo == null)
                {
                    _tutorRepo = new Repository<Tutor>(_myTutoringContext.Tutors);
                }
                return _tutorRepo;
            }
        }

        private IRepository<User>? _userRepo;
        public IRepository<User> UserRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new Repository<User>(_myTutoringContext.Users);
                }
                return _userRepo;
            }
        }

        private IRepository<UserIdentity>? _userIdentityRepo;
        public IRepository<UserIdentity> UserIdentityRepo
        {
            get
            {
                if (_userIdentityRepo == null)
                {
                    _userIdentityRepo = new Repository<UserIdentity>(_myTutoringContext.UserIdentities);
                }
                return _userIdentityRepo;
            }
        }

        private IRepository<UserRefreshToken>? _userRefreshTokenRepo;
        public IRepository<UserRefreshToken> UserRefreshTokenRepo
        {
            get
            {
                if (_userRefreshTokenRepo == null)
                {
                    _userRefreshTokenRepo = new Repository<UserRefreshToken>(_myTutoringContext.UserRefreshTokens);
                }
                return _userRefreshTokenRepo;
            }
        }

        private IRepository<UserRole>? _userRoleRepo;
        public IRepository<UserRole> UserRoleRepo
        {
            get
            {
                if (_userRoleRepo == null)
                {
                    _userRoleRepo = new Repository<UserRole>(_myTutoringContext.UserRoles);
                }
                return _userRoleRepo;
            }
        }

        public UnitOfWork(IMyTutoringContext myTutoringContext)
        {
            _myTutoringContext = myTutoringContext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _myTutoringContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _myTutoringContext.Dispose();
        }
    }
}
