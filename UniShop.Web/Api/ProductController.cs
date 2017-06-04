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
    [RoutePrefix("api/product")]
    [Authorize]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IErrorService errorService, IProductService ProductService)
            : base(errorService)
        {
            _productService = ProductService;
        }


        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetById(id);
                var responseData = Mapper.Map<Product, ProductViewModel>(model);
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
                var model = _productService.GetAll(keyword);

                totalRows = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>
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
                var model = _productService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel ProductViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var newProduct = new Product();
                    newProduct.UpdateProduct(ProductViewModel);
                    newProduct.CreatedDate = DateTime.Now;
                    newProduct.CreatedBy = User.Identity.Name;
                    _productService.Add(newProduct);
                    _productService.SaveChanges();

                    var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);

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
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel ProductViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var dbProduct = _productService.GetById(ProductViewModel.ID);

                    dbProduct.UpdateProduct(ProductViewModel);
                    dbProduct.UpdatedDate = DateTime.Now;
                    dbProduct.UpdatedBy = User.Identity.Name;
                    _productService.Update(dbProduct);
                    _productService.SaveChanges();

                    var responseData = Mapper.Map<Product, ProductViewModel>(dbProduct);

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
                    var oldProduct = _productService.Delete(id);
                    _productService.SaveChanges();

                    var responseData = Mapper.Map<Product, ProductViewModel>(oldProduct);

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
        public HttpResponseMessage DeleteAll(HttpRequestMessage request, string checkedProducts)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var id in listProduct)
                    {
                        _productService.Delete(id);
                    }
                   
                    _productService.SaveChanges();
                    

                    response = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
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