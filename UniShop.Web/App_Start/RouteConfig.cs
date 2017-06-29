using System.Web.Mvc;
using System.Web.Routing;

namespace UniShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
             "Cart",
             "gio-hang.html",
             new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "UniShop.Web.Controllers" }
             );

            routes.MapRoute(
            "Register",
            "dang-ky.html",
            new { controller = "Account", action = "Register", alias = UrlParameter.Optional },
            namespaces: new string[] { "UniShop.Web.Controllers" }
            );

            routes.MapRoute(
               "Contact",
               "lien-he.html",
               new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
               namespaces: new string[] { "UniShop.Web.Controllers" }
               );

            routes.MapRoute(
            "Page",
            "trang/{alias}.html",
            new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
            namespaces: new string[] { "UniShop.Web.Controllers" }
            );

            routes.MapRoute(
            "Search",
            "tim-kiem.html",
            new { controller = "Product", action = "Search", keyword = UrlParameter.Optional },
            namespaces: new string[] { "UniShop.Web.Controllers" }
            );

            routes.MapRoute(
            "Login",
            "dang-nhap.html",
            new { controller = "Account", action = "Login", id = UrlParameter.Optional },
            namespaces: new string[] { "UniShop.Web.Controllers" }
            );

            routes.MapRoute(
             "About",
             "gioi-thieu.html",
             new { controller = "About", action = "Index", id = UrlParameter.Optional },
             namespaces: new string[] { "UniShop.Web.Controllers" }
             );

            routes.MapRoute(
              "Product Category",
              "{alias}.pc-{id}.html",
              new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new string[] { "UniShop.Web.Controllers" }
              );

            routes.MapRoute(
             "Product",
             "{alias}.p-{id}.html",
             new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               namespaces: new string[] { "UniShop.Web.Controllers" }
             );

            routes.MapRoute(
             "Tag",
             "tag/{tagId}.html",
             new { controller = "Product", action = "ListByTag", tagId = UrlParameter.Optional },
             namespaces: new string[] { "UniShop.Web.Controllers" }
             );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                  namespaces: new string[] { "UniShop.Web.Controllers" }
                );


        }
    }
}