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
    public class MaterialControllerTests
    {
        [SetUp]
        public void Setup()
        {
            DataAccessLayerFactory.BuildContainerForTests();
        }

        [Test]
        public async Task GetAllMaterialGroups_ByRightUserInfo_RightMaterialGroupsList()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            UserInfo userInfo = new UserInfo()
            {
                Id = "74730f7a-0247-4b0a-bd32-cfd34746f9e8",
                Role = "tutor"
            };
            MaterialsGroupController materialGroupController = new MaterialsGroupController(unitOfWork);
            #endregion
            #region Act
            ActionResult<MaterialsGroupViewModel> materialsGroupViewModel = await materialGroupController.GetAll(userInfo);
            #endregion
            #region Assert
            Assert.AreEqual(2, materialsGroupViewModel.Value.MaterialGroupSingleViewModels.Count());
            Assert.AreEqual(1, materialsGroupViewModel.Value.MaterialGroupSingleViewModels.First().MaterialGroupId);
            Assert.AreEqual("firstGroup", materialsGroupViewModel.Value.MaterialGroupSingleViewModels.First().Name);
            Assert.AreEqual(2, materialsGroupViewModel.Value.MaterialGroupSingleViewModels.Last().MaterialGroupId);
            Assert.AreEqual("secondGroup", materialsGroupViewModel.Value.MaterialGroupSingleViewModels.Last().Name);
            #endregion
        }

        [Test]
        public async Task GetAllMaterialGroups_ByNullUserInfo_Null()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            UserInfo userInfo = null;
            MaterialsGroupController materialGroupController = new MaterialsGroupController(unitOfWork);
            #endregion
            #region Act
            ActionResult<MaterialsGroupViewModel> materialsGroupViewModel = await materialGroupController.GetAll(userInfo);
            #endregion
            #region Assert
            Assert.AreEqual(null, materialsGroupViewModel.Value.MaterialGroupSingleViewModels);
            #endregion
        }

        [Test]
        public async Task GetAllMaterialTypes__RightMaterialTypesList()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            MaterialsTypeController materialTypeController = new MaterialsTypeController(unitOfWork);
            #endregion
            #region Act
            ActionResult<MaterialTypeViewModel> materialsTypeViewModel = await materialTypeController.GetAll();
            #endregion
            #region Assert
            Assert.AreEqual(2, materialsTypeViewModel.Value.MaterialTypeSingleViewModels.Count());
            Assert.AreEqual(1, materialsTypeViewModel.Value.MaterialTypeSingleViewModels.First().MaterialTypeId);
            Assert.AreEqual("firstType", materialsTypeViewModel.Value.MaterialTypeSingleViewModels.First().Name);
            Assert.AreEqual(2, materialsTypeViewModel.Value.MaterialTypeSingleViewModels.Last().MaterialTypeId);
            Assert.AreEqual("secondType", materialsTypeViewModel.Value.MaterialTypeSingleViewModels.Last().Name);
            #endregion
        }

        [Test]
        public async Task GetAllMaterialVisibility_ByRightUserInfo_RightMaterialVisibilityList()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            UserInfo userInfo = new UserInfo()
            {
                Id = "b31da87c-2091-4347-ab70-a037d02410be",
                Role = "student"
            };
            MaterialVisibilityController materialVisibilityController = new MaterialVisibilityController(unitOfWork);
            #endregion
            #region Act
            ActionResult<VisibilityViewModel> materialsGroupViewModel = await materialVisibilityController.GetAllVisibilities(userInfo);
            #endregion
            #region Assert
            Assert.AreEqual(2, materialsGroupViewModel.Value.Visibilities.Count());
            Assert.AreEqual(1, materialsGroupViewModel.Value.Visibilities.First().MaterialGroupId);
            Assert.AreEqual("firstGroup", materialsGroupViewModel.Value.Visibilities.First().MaterialGroupName);
            Assert.AreEqual(true, materialsGroupViewModel.Value.Visibilities.First().IsVisible);
            Assert.AreEqual(2, materialsGroupViewModel.Value.Visibilities.Last().MaterialGroupId);
            Assert.AreEqual("secondGroup", materialsGroupViewModel.Value.Visibilities.Last().MaterialGroupName);
            Assert.AreEqual(false, materialsGroupViewModel.Value.Visibilities.Last().IsVisible);
            #endregion
        }

        [Test]
        public async Task GetAllMaterialVisibility_ByNullUserInfo_MaterialVisibilityListAsNull()
        {
            #region Arrange
            IUnitOfWork unitOfWork = DataAccessLayerFactory.CreateUnitOfWork();
            UserInfo userInfo = null;
            MaterialVisibilityController materialVisibilityController = new MaterialVisibilityController(unitOfWork);
            #endregion
            #region Act
            ActionResult<VisibilityViewModel> materialsGroupViewModel = await materialVisibilityController.GetAllVisibilities(userInfo);
            #endregion
            #region Assert
            Assert.AreEqual(null, materialsGroupViewModel.Value.Visibilities);
            #endregion
        }
    }
}