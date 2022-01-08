using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.ViewModels;
using MyTutoring.Server.Controllers;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    public class ActivityControllerTests
    {
        [SetUp]
        public void Setup()
        {
            DataAccessLayerFactory.BuildContainerForTests();
        }

        [Test]
        public async Task GetAllActivities_ByRightUserInfoWithTutorId_RightActivitiesList()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            UserInfo userInfo = new UserInfo()
            {
                Id = "74730f7a-0247-4b0a-bd32-cfd34746f9e8",
                Role = "tutor"
            };
            ActivityController activityController = new ActivityController(unitOfWork);
            #endregion
            #region Act
            ActionResult<ActivityViewModel> activityViewModel = await activityController.GetAll(userInfo);
            #endregion
            #region Assert
            Assert.AreEqual(3, activityViewModel.Value.Activities.Count());
            Assert.AreEqual(1, activityViewModel.Value.Activities.First().Id);
            Assert.AreEqual("first", activityViewModel.Value.Activities.First().Name);
            Assert.AreEqual("Poniedziałek", activityViewModel.Value.Activities.First().DayOfWeek);
            Assert.AreEqual("student student", activityViewModel.Value.Activities.First().UserName);
            Assert.AreEqual(3, activityViewModel.Value.Activities.Last().Id);
            Assert.AreEqual("third", activityViewModel.Value.Activities.Last().Name);
            Assert.AreEqual("Środa", activityViewModel.Value.Activities.Last().DayOfWeek);
            Assert.AreEqual("student student", activityViewModel.Value.Activities.Last().UserName);
            #endregion
        }

        [Test]
        public async Task GetAllActivities_ByRightUserInfoWithStudentId_RightActivitiesList()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            UserInfo userInfo = new UserInfo()
            {
                Id = "b31da87c-2091-4347-ab70-a037d02410be",
                Role = "student"
            };
            ActivityController activityController = new ActivityController(unitOfWork);
            #endregion
            #region Act
            ActionResult<ActivityViewModel> activityViewModel = await activityController.GetAll(userInfo);
            #endregion
            #region Assert
            Assert.AreEqual(3, activityViewModel.Value.Activities.Count());
            Assert.AreEqual(1, activityViewModel.Value.Activities.First().Id);
            Assert.AreEqual("first", activityViewModel.Value.Activities.First().Name);
            Assert.AreEqual("Poniedziałek", activityViewModel.Value.Activities.First().DayOfWeek);
            Assert.AreEqual("tutor tutor", activityViewModel.Value.Activities.First().UserName);
            Assert.AreEqual(3, activityViewModel.Value.Activities.Last().Id);
            Assert.AreEqual("third", activityViewModel.Value.Activities.Last().Name);
            Assert.AreEqual("Środa", activityViewModel.Value.Activities.Last().DayOfWeek);
            Assert.AreEqual("tutor tutor", activityViewModel.Value.Activities.Last().UserName);
            #endregion
        }

        [Test]
        public async Task GetAllActivities_ByNullUserInfo_ActivityListAsNull()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            UserInfo userInfo = null;
            ActivityController activityController = new ActivityController(unitOfWork);
            #endregion
            #region Act
            ActionResult<ActivityViewModel> activityViewModel = await activityController.GetAll(userInfo);
            #endregion
            #region Assert
            Assert.AreEqual(null, activityViewModel.Value.Activities);
            #endregion
        }

        [Test]
        public async Task GetAllActivities_ByUserInfoWithWrongId_EmptyActivitiesList()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            UserInfo userInfo = new UserInfo()
            {
                Id = "b31da87c-2091-4347-ab70-a037d02410aa",
                Role = "student"
            };
            ActivityController activityController = new ActivityController(unitOfWork);
            #endregion
            #region Act
            ActionResult<ActivityViewModel> activityViewModel = await activityController.GetAll(userInfo);
            #endregion
            #region Assert
            Assert.AreEqual(0, activityViewModel.Value.Activities.Count());
            #endregion
        }
    }
}
