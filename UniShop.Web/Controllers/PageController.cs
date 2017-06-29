using System.Web.Mvc;
using AutoMapper;
using UniShop.Model.Models;
using UniShop.Service;
using UniShop.Web.Models;


namespace UniShop.Web.Controllers
{
    public class PageController : Controller
    {
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            var model = _pageService.GetPagebyAlias(alias);
            var pageViewModel = Mapper.Map<Page, PageViewModel>(model);
            return View(pageViewModel);
        }
    }
}