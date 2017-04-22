using System.Collections.Generic;
using UniShop.Data.Infrastructure;
using UniShop.Data.Repositories;
using UniShop.Model.Models;

namespace UniShop.Service
{
    public interface IPostCategoryService
    {
        void Add(PostCategory postCategory);
        void Update(PostCategory postCategory);
        void Delete(int id);
        IEnumerable<PostCategory> GetAll();
        IEnumerable<PostCategory> GetAllByParentId(int parentId);
        PostCategory GetById(int id);

        void SaveChanges();
    }

    internal class PostCategoryService : IPostCategoryService
    {
        private readonly IPostCategoryRepository _postCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            _postCategoryRepository = postCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(PostCategory postCategory)
        {
            _postCategoryRepository.Add(postCategory);
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }

        public void Delete(int id)
        {
            _postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return _postCategoryRepository.GetMulti(p => p.Status && p.ParentID == parentId);
        }

        public PostCategory GetById(int id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}