using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniShop.Common;
using UniShop.Data.Infrastructure;
using UniShop.Data.Repositories;
using UniShop.Model.Models;

namespace UniShop.Service
{
    public interface IProductService
    {
        Product Add(Product Product);
        void Update(Product Product);
        Product Delete(int id);
        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);
     
        Product GetById(int id);

        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;


        public ProductService(IProductRepository ProductRepositoryCategoryRepository,
            IUnitOfWork unitOfWork, IProductTagRepository productTagRepository, ITagRepository tagRepository)
        {
            _productRepository = ProductRepositoryCategoryRepository;
            _unitOfWork = unitOfWork;
            _productTagRepository = productTagRepository;
            _tagRepository = tagRepository;
        }

        public Product Add(Product Product)
        {
            var product = _productRepository.Add(Product);
            _unitOfWork.Commit();

            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (int i = 0; i < tags.Length; ++i)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }

            return product;
        }

        public void Update(Product Product)
        {
            _productRepository.Update(Product);

            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (int i = 0; i < tags.Length; ++i)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    _productTagRepository.DeleteMulti(x => x.ProductID == Product.ID);

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }

            _unitOfWork.Commit();
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return
                    _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));

            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
