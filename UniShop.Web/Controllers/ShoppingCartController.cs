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
using UniShop.Web.Infrastructure.NganLuongAPI;
using UniShop.Web.Models;

namespace UniShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ApplicationUserManager _userManager;

        private string merchantId = ConfigHelper.GetValueByKey("MerchantId");
        private string merchantPassword = ConfigHelper.GetValueByKey("MerchantPassword");
        private string merchantEmail = ConfigHelper.GetValueByKey("MerchantEmail");

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
        public ActionResult CreateOrder(string orderViewModel)
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
                var orderReturn = _orderService.CreateOrder(newOrder, lstOrderDetails);
                _productService.SaveChanges();

                if (order.PaymentMethod == "CASH")
                {
                    return Json(new
                    {
                        status = true
                    }); 
                }
                else
                {
                    var currentLink = ConfigHelper.GetValueByKey("CurrentLink");
                    RequestInfo info = new RequestInfo();
                    info.Merchant_id = merchantId;
                    info.Merchant_password = merchantPassword;
                    info.Receiver_email = merchantEmail;



                    info.cur_code = "vnd";
                    info.bank_code = order.BankCode;

                    info.Order_code = orderReturn.ID.ToString();
                    info.Total_amount = lstOrderDetails.Sum(x => x.Quantity * x.Price).ToString();
                    info.fee_shipping = "0";
                    info.Discount_amount = "0";
                    info.order_description = "Thanh toán đơn hàng tại UniShop";
                    info.return_url = currentLink + "xac-nhan-don-hang.html";
                    info.cancel_url = currentLink + "huy-don-hang.html";

                    info.Buyer_fullname = order.CustomerName;
                    info.Buyer_email = order.CustomerEmail;
                    info.Buyer_mobile = order.CustomerMobile;

                    APICheckoutV3 objNLChecout = new APICheckoutV3();
                    ResponseInfo result = objNLChecout.GetUrlCheckout(info, order.PaymentMethod);
                    if (result.Error_code == "00")
                    {
                        return Json(new
                        {
                            status = true,
                            urlCheckout = result.Checkout_url,
                            message = result.Description
                        });
                    }
                    else
                        return Json(new
                        {
                            status = false,
                            message = result.Description
                        });
                }
            }
            return Json(new
            {
                status = false,
                message = "Không đủ số lượng hàng"
            });
        }

        public ActionResult ConfirmOrder()
        {
            string token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = merchantId;
            info.Merchant_password = merchantPassword;
            info.Token = token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if (result.errorCode == "00")
            {
                //update status order
                _orderService.UpdateStatus(int.Parse(result.order_code));
                _orderService.Save();
                ViewBag.IsSuccess = true;
                ViewBag.Result = "Thanh toán thành công. Chúng tôi sẽ liên hệ lại sớm nhất.";
            }
            else
            {
                ViewBag.IsSuccess = false;
                ViewBag.Result = "Có lỗi xảy ra. Vui lòng liên hệ admin.";
            }
            return View();
        }

        public ActionResult CancelOrder()
        {
            return View();
        }
    }
}