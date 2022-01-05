using DataEntities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DataAccessLayer.Mock
{
    public class MockMyTutoringContext : IMyTutoringContext
    {
        private readonly Mock<IMyTutoringContext> _mockDbContext;

        public DbSet<Activity> Activities 
        { 
            get => _mockDbContext.Object.Activities; 
            set => throw new NotImplementedException(); 
        }
        public DbSet<Homework> Homeworks
        {
            get => _mockDbContext.Object.Homeworks;
            set => throw new NotImplementedException();
        }
        public DbSet<Material> Materials
        {
            get => _mockDbContext.Object.Materials;
            set => throw new NotImplementedException();
        }
        public DbSet<MaterialsGroup> MaterialsGroups
        {
            get => _mockDbContext.Object.MaterialsGroups;
            set => throw new NotImplementedException();
        }
        public DbSet<MaterialsGroupVisibility> MaterialsGroupVisibilities
        {
            get => _mockDbContext.Object.MaterialsGroupVisibilities;
            set => throw new NotImplementedException();
        }
        public DbSet<MaterialType> MaterialTypes
        {
            get => _mockDbContext.Object.MaterialTypes;
            set => throw new NotImplementedException();
        }
        public DbSet<Student> Students
        {
            get => _mockDbContext.Object.Students;
            set => throw new NotImplementedException();
        }
        public DbSet<TaskSolution> TaskSolutions
        {
            get => _mockDbContext.Object.TaskSolutions;
            set => throw new NotImplementedException();
        }
        public DbSet<Tutor> Tutors
        {
            get => _mockDbContext.Object.Tutors;
            set => throw new NotImplementedException();
        }
        public DbSet<UserIdentity> UserIdentities { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<UserRefreshToken> UserRefreshTokens { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<UserRole> UserRoles
        {
            get => _mockDbContext.Object.UserRoles;
            set => throw new NotImplementedException();
        }
        public DbSet<User> Users
        {
            get => _mockDbContext.Object.Users;
            set => throw new NotImplementedException();
        }

        public MockMyTutoringContext()
        {
            _mockDbContext = new Mock<IMyTutoringContext>();

            #region UserRole
            HashSet<UserRole> userRoles = new HashSet<UserRole>()
            {
                new UserRole()
                {
                    Id = 1,
                    Name = "admin"
                },
                new UserRole()
                {
                    Id = 2,
                    Name = "tutor"
                },
                new UserRole()
                {
                    Id = 3,
                    Name = "student"
                }
            };
            #endregion

            #region Users
            HashSet<User> users = new HashSet<User>()
            {                
                new User()
                {
                    Id = Guid.Parse("74730f7a-0247-4b0a-bd32-cfd34746f9e8"),
                    RoleId = 2,
                    Email = "tutor",
                    Password = "tutor",
                    EmailConfirmation = true,
                    CreationDate = DateTime.Now,
                },
                new User()
                {
                    Id = Guid.Parse("912b19f6-462b-4aed-ace0-2dd2c2150ae5"),
                    RoleId = 1,
                    Email = "admin",
                    Password = "admin",
                    EmailConfirmation = true,
                    CreationDate = DateTime.Now,
                },
                new User()
                {
                    Id = Guid.Parse("b31da87c-2091-4347-ab70-a037d02410be"),
                    RoleId = 3,
                    Email = "student",
                    Password = "student",
                    EmailConfirmation = true,
                    CreationDate = DateTime.Now,
                }
            };
            #endregion

            #region Tutors
            HashSet<Tutor> tutors = new HashSet<Tutor>()
            {
                new Tutor()
                {
                    UserId = Guid.Parse("74730f7a-0247-4b0a-bd32-cfd34746f9e8"),
                    FirstName = "tutor",
                    LastName = "tutor",
                    PhoneNumber = 123456789,
                    User = users.First()
                }
            };
            #endregion

            #region Students
            HashSet<Student> students = new HashSet<Student>()
            {
                new Student()
                {
                    UserId = Guid.Parse("b31da87c-2091-4347-ab70-a037d02410be"),
                    FirstName = "student",
                    LastName = "student",
                    PhoneNumber = 123456789,
                    User = users.Last()
                }
            };
            #endregion

            #region Activities
            HashSet<Activity> activities = new HashSet<Activity>()
            {
                new Activity()
                {
                    Id = 1,
                    StudentId = students.First().UserId,
                    TutorId = tutors.First().UserId,
                    Name = "first",
                    DayOfWeek = 1,
                    StartTime = 10.00f,
                    EndTime = 11.00f
                },
                new Activity()
                {
                    Id = 2,
                    StudentId = students.First().UserId,
                    TutorId = tutors.First().UserId,
                    Name = "second",
                    DayOfWeek = 2,
                    StartTime = 10.00f,
                    EndTime = 11.00f
                },
                new Activity()
                {
                    Id = 3,
                    StudentId = students.First().UserId,
                    TutorId = tutors.First().UserId,
                    Name = "third",
                    DayOfWeek = 3,
                    StartTime = 10.00f,
                    EndTime = 11.00f
                }
            };
            #endregion

            #region MaterialGroups 
            HashSet<MaterialsGroup> materialsGroups = new HashSet<MaterialsGroup>()
            {
                new MaterialsGroup()
                {
                    Id = 1,
                    Name = "firstGroup"
                },
                new MaterialsGroup()
                {
                    Id = 2,
                    Name = "secondGroup"
                }
            };
            #endregion

            #region MaterialType
            HashSet<MaterialType> materialTypes = new HashSet<MaterialType>()
            {
                new MaterialType()
                {
                    Id = 1,
                    Name = "firstType"
                },
                new MaterialType()
                {
                    Id = 2,
                    Name = "secondType"
                }
            };
            #endregion

            #region Materials
            HashSet<Material> materials = new HashSet<Material>()
            {
                new Material()
                {
                    Id = 1,
                    HomeworkId = 1,
                    MaterialTypeId = 1,
                    MaterialGroupId = 1,
                    Name = "firstMaterial",
                    FileName = "firstFileName",
                    Description = "firstDescription",
                    MaterialType = materialTypes.First(),
                    MaterialGroup = materialsGroups.First()
                },
                new Material()
                {
                    Id = 2,
                    MaterialTypeId = 1,
                    MaterialGroupId = 2,
                    Name = "secondMaterial",
                    FileName = "secondFileName",
                    Description = "secondDescription",
                    MaterialType = materialTypes.First(),
                    MaterialGroup = materialsGroups.Last()
                },
                new Material()
                {
                    Id = 3,
                    MaterialTypeId = 2,
                    MaterialGroupId = 1,
                    Name = "thirdMaterial",
                    FileName = "thirdFileName",
                    Description = "thirdDescription",
                    MaterialType = materialTypes.Last(),
                    MaterialGroup = materialsGroups.First()
                },
                new Material()
                {
                    Id = 4,
                    HomeworkId = 2,
                    MaterialTypeId = 2,
                    MaterialGroupId = 2,
                    Name = "fourthMaterial",
                    FileName = "fourthFileName",
                    Description = "fourthDescription",
                    MaterialType = materialTypes.Last(),
                    MaterialGroup = materialsGroups.Last()
                },
            };
            #endregion

            #region MaterialsGroupVisibility
            HashSet<MaterialsGroupVisibility> materialsGroupVisibilities = new HashSet<MaterialsGroupVisibility>()
            {
                new MaterialsGroupVisibility()
                {
                    StudentId = students.First().UserId,
                    MaterialsGroupId = materialsGroups.First().Id,
                    IsVisible = true,
                    Student = students.First(),
                    MaterialsGroup = materialsGroups.First()
                },
                new MaterialsGroupVisibility()
                {
                    StudentId = students.First().UserId,
                    MaterialsGroupId = materialsGroups.Last().Id,
                    IsVisible = true,
                    Student = students.First(),
                    MaterialsGroup = materialsGroups.Last()
                }
            };
            #endregion

            #region Homeworks
            HashSet<Homework> homeworks = new HashSet<Homework>()
            {
                new Homework()
                {
                    Id = 1,
                    TutorId = tutors.First().UserId,
                    StudentId = students.First().UserId,
                    Name = "first",
                    Status = false,
                    Grade = 0,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    Student = students.First(),
                    Tutor = tutors.First(),
                    TaskSolutions = new List<TaskSolution>(),
                    Materials = new HashSet<Material> () { materials.First()}
                },
                new Homework()
                {
                    Id = 2,
                    TutorId = tutors.First().UserId,
                    StudentId = students.First().UserId,
                    Name = "second",
                    Status = true,
                    Grade = 4,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    Student = students.First(),
                    Tutor = tutors.First(),
                    Materials = new HashSet<Material> () { materials.Last()}
                }
            };
            #endregion

            #region TaskSolutions
            HashSet<TaskSolution> taskSolutions = new HashSet<TaskSolution>()
            {
                new TaskSolution()
                {
                    Id=1,
                    HomeworkId = 2,
                    FileName = "first",
                    Homework = homeworks.First()
                }
            };
            #endregion

            students.First().Activities = activities;
            students.First().Homeworks = homeworks;
            students.First().MaterialsGroupVisibilities = materialsGroupVisibilities;

            tutors.First().Activities = activities;
            tutors.First().Homeworks = homeworks;

            materialsGroups.First().Materials = new HashSet<Material>() { materials.First(), materials.SingleOrDefault(m => m.Id == 3) };
            materialsGroups.First().MaterialsGroupVisibilities = new HashSet<MaterialsGroupVisibility>() { materialsGroupVisibilities.First() };
            materialsGroups.Last().Materials = new HashSet<Material>() { materials.SingleOrDefault(m => m.Id == 2), materials.Last() };
            materialsGroups.Last().MaterialsGroupVisibilities = new HashSet<MaterialsGroupVisibility>() { materialsGroupVisibilities.Last() };

            materialTypes.First().Materials = new HashSet<Material>() { materials.First(), materials.SingleOrDefault(m => m.Id == 2) };
            materialTypes.Last().Materials = new HashSet<Material>() { materials.SingleOrDefault(m => m.Id == 3), materials.Last() };

            homeworks.Last().TaskSolutions = taskSolutions;

            materials.First().Homework = homeworks.First();
            materials.Last().Homework = homeworks.Last();

            _mockDbContext.Setup(db => db.UserRoles).Returns(GetQueryableMockDbSet(userRoles));
            _mockDbContext.Setup(db => db.Users).Returns(GetQueryableMockDbSet(users));
            _mockDbContext.Setup(db => db.Tutors).Returns(GetQueryableMockDbSet(tutors));
            _mockDbContext.Setup(db => db.Students).Returns(GetQueryableMockDbSet(students));
            _mockDbContext.Setup(db => db.Activities).Returns(GetQueryableMockDbSet(activities));
            _mockDbContext.Setup(db => db.MaterialsGroups).Returns(GetQueryableMockDbSet(materialsGroups));
            _mockDbContext.Setup(db => db.MaterialTypes).Returns(GetQueryableMockDbSet(materialTypes));
            _mockDbContext.Setup(db => db.Materials).Returns(GetQueryableMockDbSet(materials));
            _mockDbContext.Setup(db => db.MaterialsGroupVisibilities).Returns(GetQueryableMockDbSet(materialsGroupVisibilities));
            _mockDbContext.Setup(db => db.Homeworks).Returns(GetQueryableMockDbSet(homeworks));
            _mockDbContext.Setup(db => db.TaskSolutions).Returns(GetQueryableMockDbSet(taskSolutions));
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(HashSet<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IAsyncEnumerable<T>>()
                .Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));

            // this line is updated
            dbSet.As<IQueryable<T>>()
                .Setup(x => x.Provider)
                .Returns(new TestAsyncQueryProvider<T>(queryable.Provider));

            dbSet.As<IQueryable<T>>()
                .Setup(x => x.Expression)
                .Returns(queryable.Expression);

            dbSet.As<IQueryable<T>>()
                .Setup(x => x.ElementType)
                .Returns(queryable.ElementType);

            dbSet.As<IQueryable<T>>()
                .Setup(x => x.GetEnumerator())
                .Returns(queryable.GetEnumerator());

            return dbSet.Object;
        }

        public void Dispose()
        { }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Task.Delay(0, cancellationToken);
            return 0;
        }
    }
}
