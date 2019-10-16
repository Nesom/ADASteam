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

        public IActionResult Index()
        {
            return View();
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ChouseAdd()
        {
            var types = ReflectionHelper.GetAllProductClasses();
            var nameToTypeDict = ReflectionHelper.CreateNameToTypeDict(types, typeof(Attributes.ClassName));
            var model = new AddModel() { ProductNames = nameToTypeDict.Keys.ToArray() };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add(AddModel model)
        {
            var types = ReflectionHelper.GetAllProductClasses();
            var nameToTypeDict = ReflectionHelper.CreateNameToTypeDict(types, typeof(Attributes.ClassName));

            if (nameToTypeDict.ContainsKey(model.Name))
                model.Name = nameToTypeDict[model.Name];

            var type = ReflectionHelper.FoundType(model.Name);
            var newModel = ReflectionHelper.CreateAddModelByType(type);

            newModel.Name = model.Name;

            return View(newModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddHelper(AddModel model)
        {
            var description = ReflectionHelper.CreateProductDescription(model.Name, model.Values);
            var productInfo = ReflectionHelper.CreateProductInfo(model.StandartInfoValues);
            var product = new Product<IDescription>();
            product.Description = description;
            product.ProductInfo = productInfo;
            product.ProductInfo.Image = ControllerHelper.ConvertFileToBytes(model.Image);
            db.AddProduct(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Get(GetModel model)
        {
            var product = db.GetById(model.Id);
            model.Values = ReflectionHelper.GetCharacteristics(product.Description);
            model.StandartValues = ReflectionHelper.GetCharacteristics(product.ProductInfo);
            model.Image = product.ProductInfo.Image;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) => await Get(new GetModel { Id = id });

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
                    RequestInfo = "Товар не доступен"
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

            var dict = new Dictionary<ProductInfo, int>();
            foreach (var value in values)
                dict.Add(db.GetProductInfo(value.Key), value.Value);

            return View(new BasketModel() { Products = dict });
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
                        RequestInfo = "Количество недоступно!"
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
                if (db.TryToChangeCountOfProducts(item.Key, item.Value))
                {
                    var product = db.GetProductInfo(item.Key);
                    orderInfo.Products.Add(new SubOrder()
                    {
                        Product = product,
                        Count = item.Value
                    });
                    orderInfo.Amount += product.Price * item.Value;
                }
                else
                {
                    var productName = db.GetProductInfo(item.Key);
                    return RedirectToAction("Error",
                        new ErrorViewModel()
                        {
                            ActionName = "Basket",
                            RequestInfo = $"Отстутствует {item.Value} товаров на складе ({productName})"
                        });
                }
            }

            var userId = (int)TempData.Peek("id");
            model.Address.UserId = userId;
            orderInfo.UserId = userId;

            orderInfo.StatusInfo = Status.Проверяется;
            orderInfo.OrderTime = DateTime.Now;
            orderInfo.AddressId = db.SaveAndGetId(model.Address);

            db.SaveAndGetId(orderInfo);
            // изменить кол-во заказов у продукта
            // изменить структуру OrderInfo в бд
            // добавить возможность меня статус заказа
            // добавить возможность оценивать продукт если статус = доставлено(получено)

            HttpContext.Session.Remove("basket");

            return View(new OrderModel() { });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetOrders()
        {
            return View((IEnumerable<OrderInfo>)db.Orders);
        }
    }
}