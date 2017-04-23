using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniShop.Data.Infrastructure;
using UniShop.Data.Repositories;
using UniShop.Model.Models;

namespace UniShop.UnitTest
{
    [TestClass]
    public class PostCategortyRepositoryUnitTest
    {
        private IDbFactory _dbFactory;
        private IPostCategoryRepository _postCategoryRepository;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            _dbFactory = new DbFactory();
            _postCategoryRepository = new PostCategoryRepository(_dbFactory);
            _unitOfWork = new UnitOfWork(_dbFactory);
        }

        [TestMethod]
        public void PostCategortyRepository_Create()
        {
            PostCategory category = new PostCategory();
            category.Name = "Test";
            category.Alias = "Test";
            category.Status = true;

            var result = _postCategoryRepository.Add(category);
            _unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.ID);
        }

        [TestMethod]
        public void PostCategortyRepository_GetAll()
        {
            var lst = _postCategoryRepository.GetAll();
            Assert.AreEqual(3, lst.Count());
        }
    }
}