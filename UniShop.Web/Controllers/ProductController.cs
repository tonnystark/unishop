using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using UniShop.Common;
using UniShop.Model.Models;
using UniShop.Service;
using UniShop.Web.Infrastructure.Core;
using UniShop.Web.Models;

namespace UniShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        // GET: Product
        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(productModel);

            var relatedProduct = _productService.GetReatedProducts(id, 6);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);

            var lstImages = new JavaScriptSerializer().Deserialize<List<string>>(productModel.MoreImages);
            ViewBag.MoreImages = lstImages;

            ViewBag.Tags =
                Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(id));

            return View(productViewModel);
        }

        public ActionResult Category(int id, int page = 1, string sort = "new")
        {
            var pageSize = int.Parse(ConfigHelper.GetValueByKey("PageSize"));
            var totalrow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalrow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            var totalPages = (int) Math.Ceiling((double) totalrow/pageSize);

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                TotalPages = totalPages,
                Page = page,
                TotalCount = totalrow,
                MaxPages = int.Parse(ConfigHelper.GetValueByKey("MaxPage"))
            };

            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "new")
        {
            var pageSize = int.Parse(ConfigHelper.GetValueByKey("PageSize"));
            var totalrow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalrow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            var totalPages = (int) Math.Ceiling((double) totalrow/pageSize);

            ViewBag.Keyword = keyword;

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                TotalPages = totalPages,
                Page = page,
                TotalCount = totalrow,
                MaxPages = int.Parse(ConfigHelper.GetValueByKey("MaxPage"))
            };

            return View(paginationSet);
        }

        public ActionResult ListByTag(string tagId, int page = 1, string sort = "new")
        {
            var pageSize = int.Parse(ConfigHelper.GetValueByKey("PageSize"));
            var totalRow = 0;
            var productModel = _productService.GetListProductByTag(tagId, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            var totalPage = (int) Math.Ceiling((double) totalRow/pageSize);

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));
            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                MaxPages = int.Parse(ConfigHelper.GetValueByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetProductByName(keyword);

            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }
    }
}