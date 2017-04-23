using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UniShop.Data.Infrastructure;
using UniShop.Data.Repositories;
using UniShop.Model.Models;
using UniShop.Service;

namespace UniShop.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategortyServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _categoryService;
        private List<PostCategory> _listCategories;


        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listCategories = new List<PostCategory>()
            {
                new PostCategory() {ID =1 ,Name="DM1",Status=true },
                new PostCategory() {ID =2 ,Name="DM2",Status=true },
                new PostCategory() {ID =3 ,Name="DM3",Status=true },
            };


        }

        [TestMethod]
        public void PostCategortyService_Create()
        {
            PostCategory category = new PostCategory();
            category.Name = "Test";
            category.Alias = "test";
            category.Status = true;

            _mockRepository.Setup(p => p.Add(category)).Returns(category);

            var result = _categoryService.Add(category);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.ID);
        }

        [TestMethod]
        public void PostCategortyService_GetAll()
        {
            //set up medthod
            _mockRepository.Setup(p => p.GetAll(null)).Returns(_listCategories);

            // call action
            var result = _categoryService.GetAll() as List<PostCategory>;

            //comepare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }
    }
}
