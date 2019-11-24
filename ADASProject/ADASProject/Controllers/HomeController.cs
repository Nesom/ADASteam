using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ADASProject.Models;
using System.Linq;
using System.Linq.Expressions;
using ADASProject.Products;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ADASProject.Controllers
{
    public class HomeController : Controller
    {
        IDbContext db;

        public IActionResult Test() => View();

        private string str;

        public HomeController(IDbContext context)
        {
            db = context;
        }

        #region HomeWork
        public static Dictionary<Type, Tuple<string, PropertyInfo, bool>[]> Cache { get; }
            = new Dictionary<Type, Tuple<string, PropertyInfo, bool>[]>();

        [HttpGet]
        public async Task<IActionResult> EditerT(int id)
        {
            var product = await db.GetProductInfoAsync(id);

            var type = product.GetType();

            var timer = new Stopwatch();

            timer.Start();

            var model = new EditerTModel();
            if (!Cache.ContainsKey(type))
            {
                var properties = type.GetProperties();
                var types = properties
                    .Select(pr => Tuple.Create(pr.Name, pr, pr.GetCustomAttributes(typeof(Attributes.ClassName), false) == null))
                    .ToArray();
                Cache[type] = types;
            }
            model.Types = Cache[type];
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

        public async Task<IActionResult> Index()
        {
            var products = (await db.GetProductInfosAsync())
                .OrderByDescending(pr => pr.AddDate)
                .Take(4)
                .ToArray();
            return View(new IndexModel() { First4Products = products });
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> PersonalArea()
        {
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
