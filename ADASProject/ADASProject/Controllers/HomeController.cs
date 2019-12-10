using ADASProject.Models;
using ADASProject.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ADASProject.Controllers
{
    public class HomeController : Controller
    {
        IDbContext db;
        IHttpContextAccessor accessor;

        public IActionResult Test() => View();

        public HomeController(IDbContext context, IHttpContextAccessor contextAccessor)
        {
            db = context;
            accessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            if (!TempData.ContainsKey("city"))
            {
                var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var city = Autodetect.GetCity(ip);

                if (city != null && city.Length > 2)
                    TempData["city"] = city;
                else
                    TempData["city"] = "Kazan'";
            }

            var products = (await db.GetProductInfosAsync())
                .OrderByDescending(pr => pr.AddDate)
                .Take(4)
                .ToArray();

            return View(new IndexModel() { First4Products = products });
        }

        #region HomeWork

        private static Func<Type, PropertyInfo[]> getProperties = null;

        [HttpGet]
        public async Task<IActionResult> EditerT(int id)
        {
            var product = await db.GetProductInfoAsync(id);

            var timer = new Stopwatch();
            timer.Start();

            if (getProperties == null)
            {
                var type = Expression.Parameter(typeof(Type), "type");

                var methods = typeof(Type).GetMethods();
                var method = methods.Where(m => m.Name == "GetProperties").FirstOrDefault();

                var props = Expression.Call(type, method, new Expression[0]);
                var lambda = Expression.Lambda<Func<Type, PropertyInfo[]>>(props, type);
                getProperties = lambda.Compile();
            }
            var properties = getProperties(typeof(ProductInfo));

            var model = new EditerTModel();
            model.Types = properties
                .Select(property =>
                    Tuple.Create(property.Name, property,
                    property.GetCustomAttribute(typeof(Attributes.ClassName)) != null))
                .ToArray();

            model.StandartValues = new object[model.Types.Length];
            for (int i = 0; i < model.Types.Length; i++)
            {
                model.StandartValues[i] = model.Types[i].Item2.GetValue(product);
            }

            timer.Stop();
            model.TimeInTicks = timer.ElapsedTicks;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditT(EditerTModel model)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string Get(string request)
        {
            return "Test";
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> SetCity()
        {
            var cityList = new List<string>()
            {
                "Kazan'",
                "Moscow",
                "Yekaterinburg",
                "St. Petersburg",
                "Ufa",
                "Novosibirsk",
                "Omsk",
                "Samara",
                "Krasnoyarsk"
            };
            return View(new SetCityModel() { Cities = cityList });
        }

        [HttpPost]
        public async Task<IActionResult> SetCity(SetCityModel model)
        {
            TempData["city"] = model.City;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> PersonalArea()
        {
            TempData.Remove("city");
            var model = new PersonalAreaModel()
            {
                Orders = (await db.GetOrdersAsync((int)TempData.Peek("id"))).ToArray()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Catalog(CatalogModel model)
        {
            // Fill main fields
            model.FillFields();

            bool hasCustomTypes = false;
            if (model.CategoryName != null)
                hasCustomTypes = model.TryToFillCustomFields(model.CategoryName + "Description");

            // Select by categories
            Expression<Func<ProductInfo, bool>> selector = null;

            if (model.CategoryName == null || !CatalogModel.TypeToNameDict.ContainsKey(model.CategoryName))
                selector = (pr) => true;
            else
                selector = (pr) => pr.TableName == model.CategoryName + "Descriptions";

            var products = (await db.GetProductInfosAsync()).Where(selector);
            // Filter by custom values
            if (model.CustomValuesInfo != null && model.CustomValuesInfo.FromValues != null && model.CustomValuesInfo.ToValues != null)
            {
                var descriptions = ControllerHelper.FilterCatalogValues(await db.GetDescriptionsAsync(model.CategoryName + "Descriptions"),
                    ReflectionHelper.FoundType(model.CategoryName + "Description"),
                    model.CustomValuesInfo);

                var ids = descriptions.Select(d => d.Id).OrderBy(id => id).ToHashSet();
                products = products.Where(pr => ids.Contains(pr.Id));
            }
            // Filter by main values
            if (model.StandartValuesInfo != null && model.StandartValuesInfo.FromValues != null && model.StandartValuesInfo.ToValues != null)
            {
                products = ControllerHelper.FilterCatalogValues(products, typeof(ProductInfo), model.StandartValuesInfo);
            }

            if (!hasCustomTypes)
                model.CustomValuesInfo.Types = new Type[0];
            // Sort by parameter
            if (model.SortedBy != null)
                products = ControllerHelper.OrderValuesByParameter(products, model.SortedBy, model.SortedByDescending);

            model.Count = products.Count();
            model.Products = products;

            return View(model);
        }

        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error(ErrorViewModel model)
        {
            if (model == null)
                model = new ErrorViewModel();
            return View(model);
        }
    }
}
