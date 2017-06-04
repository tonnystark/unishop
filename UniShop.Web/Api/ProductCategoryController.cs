using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using AutoMapper;
using UniShop.Model.Models;
using UniShop.Service;
using UniShop.Web.Infrastructure.Core;
using UniShop.Web.Infrastructure.Extensions;
using UniShop.Web.Models;

namespace UniShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    [Authorize]
    public class ProductCategoryController : ApiControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }


        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetById(id);
                var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, string keyword, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                var totalRows = 0;
                var model = _productCategoryService.GetAll(keyword);

                totalRows = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRows,
                    TotalPages = (int)Math.Ceiling((decimal)totalRows / pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }


        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAllParent(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();
                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var newProductCategory = new ProductCategory();
                    newProductCategory.UpdateProductCategory(productCategoryViewModel);
                    newProductCategory.CreatedBy = User.Identity.Name;
                    newProductCategory.CreatedDate = DateTime.Now;
                    _productCategoryService.Add(newProductCategory);
                    _productCategoryService.SaveChanges();

                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);

                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }


                return response;
            });

        }


        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var dbProductCategory = _productCategoryService.GetById(productCategoryViewModel.ID);

                    dbProductCategory.UpdateProductCategory(productCategoryViewModel);
                    dbProductCategory.UpdatedDate = DateTime.Now;
                    dbProductCategory.UpdatedBy = User.Identity.Name;
                   
                    _productCategoryService.Update(dbProductCategory);
                    _productCategoryService.SaveChanges();

                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);

                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });

        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var oldProductCategory = _productCategoryService.Delete(id);
                    _productCategoryService.SaveChanges();

                    var responseData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(oldProductCategory);

                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }


                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteAll(HttpRequestMessage request, string checkedProductCategories)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var listProductCategory = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductCategories);
                    foreach (var id in listProductCategory)
                    {
                        _productCategoryService.Delete(id);
                    }
                   
                    _productCategoryService.SaveChanges();
                    

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

    }
}