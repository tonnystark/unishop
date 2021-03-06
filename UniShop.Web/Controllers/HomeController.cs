﻿using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using UniShop.Common;
using UniShop.Model.Models;
using UniShop.Service;
using UniShop.Web.Models;

namespace UniShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService,
            IProductService productService)
        {
            _commonService = commonService;
            _productCategoryService = productCategoryService;
            _productService = productService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var slideMode = _commonService.GetSlides();
            var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideMode);

            var lastestProductModel = _productService.GetLastest(3);
            var hotProductModel = _productService.GetHotProduct(3);
            var lastestProductViewModel =
                Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var hotProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(hotProductModel);


            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideViewModel;
            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.HotProducts = hotProductViewModel;

            try
            {
                homeViewModel.Title = _commonService.GetSystemConfig(CommonConstants.HomeTitle).ValueString;
                homeViewModel.MetaKeyword = _commonService.GetSystemConfig(CommonConstants.HomeMetaKeyword).ValueString;
                homeViewModel.MetaDescription =
                    _commonService.GetSystemConfig(CommonConstants.HomeMetaDescription).ValueString;
            }
            catch
            {
            }

            return View(homeViewModel);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(model);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var lstProductCategoryServiceViewModel =
                Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(lstProductCategoryServiceViewModel);
        }
    }
}