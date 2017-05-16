using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using UniShop.Service;
using UniShop.Web.Infrastructure.Core;
using UniShop.Web.Models;

namespace UniShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();
                var lstProductCategoryVm = Mapper.Map<List<ProductCategoryViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, lstProductCategoryVm);

                return response;
            });
        }
    }
}
