﻿using System.Web.Http;
using UniShop.Service;
using UniShop.Web.Infrastructure.Core;

namespace UniShop.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorService _errorService;

        public HomeController(IErrorService errorService) : base(errorService)
        {
            _errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Welcome to Unicor";
        }
    }
}