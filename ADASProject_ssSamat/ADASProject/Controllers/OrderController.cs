using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADASProject.Models;
using ADASProject.Order;
using ADASProject.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADASProject.Controllers
{
    public class OrderController : Controller
    {
        ApplicationContext db;

        public OrderController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error(ErrorViewModel model)
        {
            if (model == null)
                model = new ErrorViewModel();
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Buy(int id)
        {
            var value = new byte[0];
            if (HttpContext.Session.Keys.Contains("basket"))
                value = HttpContext.Session.Get("basket");

            if (!db.IsAvailable(id))
                return RedirectToAction("Error", new ErrorViewModel()
                {
                    ActionName = "Catalog",
                    RequestInfo = "Product not available"
                });

            HttpContext.Session.Set("basket", value.AddValue(id));
            return RedirectToAction("Basket");
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Basket()
        {
            if (!HttpContext.Session.Keys.Contains("basket"))
                return View(new BasketModel() { Products = new Dictionary<ProductInfo, int>() });

            var basket = HttpContext.Session.Get("basket");
            var values = basket.GetValues();

            var model = new BasketModel();

            foreach (var value in values)
            {
                var product = db.GetProductInfo(value.Key);
                model.Products.Add(product, value.Value);
                model.Amount += product.Price * value.Value;
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            if (!HttpContext.Session.Keys.Contains("basket"))
                return RedirectToAction("Index", "Home");

            var basket = HttpContext.Session.Get("basket");
            basket.RemoveValue(id);
            HttpContext.Session.Set("basket", basket);
            return RedirectToAction("Basket");
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> ChangeCount(BasketModel model)
        {
            if (!HttpContext.Session.Keys.Contains("basket"))
                return RedirectToAction("Index", "Home");

            if (model.Count > 65535 || model.Count < 0 || !db.HasQuantity(model.Id, model.Count))
                return RedirectToAction("Error",
                    new ErrorViewModel()
                    {
                        ActionName = "Basket",
                        RequestInfo = "This count not available!"
                    });

            var basket = HttpContext.Session.Get("basket");
            basket.ChangeCount(model.Id, (byte)model.Count);
            HttpContext.Session.Set("basket", basket);
            return RedirectToAction("Basket");
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Sending()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> Order(OrderModel model)
        {
            if (!HttpContext.Session.Keys.Contains("basket") || TempData.Peek("id") == null)
                return RedirectToAction("Index", "Home");

            var basket = HttpContext.Session.Get("basket");
            var values = basket.GetValues();

            var orderInfo = new OrderInfo();
            foreach (var item in values)
            {
                if (db.HasQuantity(item.Key, item.Value))
                {
                    var product = db.GetProductInfo(item.Key);
                    orderInfo.SubOrders.Add(new SubOrder() { ProductId = item.Key, Count = item.Value });
                    orderInfo.Amount += product.Price * item.Value;
                }
                else
                {
                    var productName = db.GetProductInfo(item.Key);
                    return RedirectToAction("Error",
                        new ErrorViewModel()
                        {
                            ActionName = "Basket",
                            RequestInfo = $"Not available {item.Value} products in stock ({productName})"
                        });
                }
            }

            var userId = (int)TempData.Peek("id");
            model.Address.UserId = userId;
            orderInfo.UserId = userId;
            orderInfo.StatusInfo = Status.Проверяется;
            orderInfo.OrderTime = DateTime.Now;

            

            if (!db.TryToSaveOrder(orderInfo))
                return RedirectToAction("Error",
                                        new ErrorViewModel()
                                        {
                                            ActionName = "Basket",
                                            RequestInfo = $"Error creating order. Some products may be missing."
                                        });

            HttpContext.Session.Remove("basket");

            // добавить возможность меня статус заказа
            // добавить возможность оценивать продукт если статус = доставлено(получено)



            return View(new OrderModel() { });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetOrders()
        {
            var model = new GetOrdersModel();
            foreach(var order in db.Orders)
            {
                model.Products.Add(order, new List<Tuple<ProductInfo, int>>());
                var subOrders = db.GetSubOrders(order.Id);
                foreach(var subOrder in subOrders)
                {
                    var product = db.GetProductInfo(subOrder.ProductId);
                    model.Products[order].Add(Tuple.Create(product, subOrder.Count));
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChangeOrder(int id)
        {
            var order = db.Orders.Find(id);
            throw new NotImplementedException();
        }
    }
}