using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADASProject.Models;
using ADASProject.Notifications;
using ADASProject.Order;
using ADASProject.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADASProject.Controllers
{
    public class OrderController : Controller
    {
        IDbContext db;

        public OrderController(IDbContext context)
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

            if (!await db.IsAvailable(id))
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
                var product = await db.GetProductInfoAsync(value.Key);
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

            if (model.Count > 65535 || model.Count < 0 || !await db.HasQuantity(model.Id, model.Count))
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
                if (await db.HasQuantity(item.Key, item.Value))
                {
                    var product = await db.GetProductInfoAsync(item.Key);
                    orderInfo.SubOrders.Add(new SubOrder() { ProductId = item.Key, Count = item.Value });
                    orderInfo.Amount += product.Price * item.Value;
                }
                else
                {
                    var productName = await db.GetProductInfoAsync(item.Key);
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
            orderInfo.StatusInfo = Status.Review;
            orderInfo.OrderTime = DateTime.Now;



            if (!await db.TryToSaveOrderAsync(orderInfo))
                return base.RedirectToAction("Error", 
                     new ErrorViewModel()
                     {
                         ActionName = "Basket",
                         RequestInfo = $"Error creating order. Some products may be missing."
                     });

            HttpContext.Session.Remove("basket");

            await NotificationService.Service.SendOrderNotificationAsync((string)TempData.Peek("username"), orderInfo, Status.Review);

            // добавить возможность оценивать продукт если статус = доставлено(получено) (??)

            return View(new OrderModel() { });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetOrders()
        {
            var model = new GetOrdersModel();
            // Get all orders from DB
            var orders = (IQueryable<OrderInfo>)await db.GetOrdersAsync();
            foreach (var order in orders)
            {
                // Add all sub orders from this order
                model.Products.Add(order, new List<Tuple<ProductInfo, int>>());
                var subOrders = await db.GetSubOrdersAsync(order.Id);
                foreach (var subOrder in subOrders)
                {
                    var product = await db.GetProductInfoAsync(subOrder.ProductId);
                    model.Products[order].Add(Tuple.Create(product, subOrder.Count));
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await db.GetOrderAsync(id);

            if (order.UserId != (int)TempData.Peek("id") && "admin" != (string)TempData.Peek("role"))
                return RedirectToAction("Error",
                           new ErrorViewModel()
                           {
                               ActionName = "GetOrders",
                               RequestInfo = $"You dont have permissions."
                           });

            if (order == null)
                return RedirectToAction("Error", "Home",
                           new ErrorViewModel()
                           {
                               ActionName = "Index",
                               RequestInfo = $"Order with id = {id} doesn't exist."
                           });

            // Get sub orders
            order.SubOrders = await db.GetSubOrdersAsync(order.Id);
            // Create list of subOrders
            var list = new List<Tuple<ProductInfo, int>>();
            // Fill sub orders info
            foreach (var subOrder in order.SubOrders)
            {
                var product = await db.GetProductInfoAsync(subOrder.ProductId);
                list.Add(Tuple.Create(product, subOrder.Count));
            }
            // Create and return model
            var model = new GetOrderModel() { Order = order, SubOrders = list, Status = order.StatusInfo, Role = (string)TempData.Peek("role") };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> ChangeOrder(GetOrderModel model)
        {
            var order = await db.GetOrderAsync(model.Order.Id);
            // Get user
            var user = await db.GetUserAsync(order.UserId);
            // Send mail about changes in status to this user
            await NotificationService.Service.SendOrderNotificationAsync(user.Email, order, model.Status);
            // Change and save order
            await db.ChangeStatusAsync(model.Order.Id, model.Status);
            return RedirectToAction("PersonalArea", "Home");
        }
    }
}