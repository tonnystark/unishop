using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniShop.Service;
using UniShop.Web.Infrastructure.Core;

namespace UniShop.Web.Api
{
    [RoutePrefix("api/statistic")]
    public class StatisticController : ApiControllerBase
    {
        private IErrorService _errorService;
        private IStatisticService _statisticService;
        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            _errorService = errorService;
            _statisticService = statisticService;
        }

        [Route("getrevenue")]
        [HttpGet]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, string fromDate, string toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetRevenueStatistic(fromDate, toDate);
                return request.CreateResponse(HttpStatusCode.OK, model);
            });
        }
    }
}
