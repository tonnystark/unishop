using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AutoMapper;
using Microsoft.AspNet.Identity;
using UniShop.Common;
using UniShop.Model.Models;
using UniShop.Service;
using UniShop.Web.App_Start;
using UniShop.Web.Infrastructure.Extensions;
using UniShop.Web.Models;

namespace UniShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ApplicationUserManager _userManager;

        public ShoppingCartController(IProductService productService, ApplicationUserManager userManager,
            IOrderService orderService)
        {
            _productService = productService;
            _userManager = userManager;
            _orderService = orderService;
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return View();
        }

        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();

            var cart = (List<ShoppingCartViewModel>) Session[CommonConstants.SessionCart];
            return Json(new
            {
                data = cart,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>) Session[CommonConstants.SessionCart];
            var product = _productService.GetById(productId);
            if (cart == null)
                cart = new List<ShoppingCartViewModel>();

            if (product.Quantity == 0)
            {
                return Json(new
                {
                    status = false,
                    message = "Sản phẩm này hiện đang hết hàng"
                });
            }

            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity++;
                    }
                }
            }
            else
            {
                var shoppingCart = new ShoppingCartViewModel();
                shoppingCart.ProductId = productId;
                shoppingCart.Product = Mapper.Map<Product, ProductViewModel>(product);
                shoppingCart.Quantity = 1;

                cart.Add(shoppingCart);
            }
            Session[CommonConstants.SessionCart] = cart;

            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            var cartSession = (List<ShoppingCartViewModel>) Session[CommonConstants.SessionCart];

            foreach (var cartSs in cartSession)
            {
                foreach (var cart in cartViewModel)
                {
                    if (cartSs.ProductId == cart.ProductId)
                    {
                        cartSs.Quantity = cart.Quantity;
                    }
                }
            }

            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = null;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>) Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        public ActionResult CheckOut()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                return Redirect("/gio-hang.html");
            }
            return View();
        }

        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new
                {
                    status = true,
                    data = user
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult CreateOrder(string orderViewModel)
        {
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            var newOrder = new Order();

            newOrder.UpdateOrder(order);

            if (Request.IsAuthenticated)
            {
                newOrder.CustomerId = User.Identity.GetUserId();
                newOrder.CreateBy = User.Identity.GetUserName();
            }
            var cart = (List<ShoppingCartViewModel>) Session[CommonConstants.SessionCart];
            var lstOrderDetails = new List<OrderDetail>();
            var isEnough = true;
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.Price = item.Product.Price;

                lstOrderDetails.Add(detail);

                isEnough = _productService.SellProduct(item.ProductId, item.Quantity);
                if (isEnough == false)
                    break;
            }

            if (isEnough)
            {
                _orderService.CreateOrder(newOrder, lstOrderDetails);
                _productService.SaveChanges();
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false,
                message = "Không đủ số lượng hàng"
            });
        }
    }
}