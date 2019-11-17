using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ADASProject.Models;
using System.Linq;
using System.Linq.Expressions;
using ADASProject.Products;
using System;
using System.Collections.Generic;

namespace ADASProject.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext db;

        public IActionResult Test() => View();

        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public static Dictionary<Type, EditerTModel> Cache { get; } 
            = new Dictionary<Type, EditerTModel>();

        [HttpGet]
        public async Task<IActionResult> EditerT(int id)
        {
            var product = await db.GetProductInfo(id);

            var type = product.GetType();

            var model = new EditerTModel();

            if (!Cache.ContainsKey(type))
            {
                var properties = type.GetProperties();
                model.Types = properties
                    .Select(pr => Tuple.Create(pr.Name, pr.PropertyType, pr.GetCustomAttributes(typeof(Attributes.ClassName), false) == null))
                    .ToArray();

                for(int i = 0; i < properties.Length; i++)
                {
                    model.StandartValues[i] = properties[i].GetValue(product);
                }

                Cache[type] = model;
            }
            model = Cache[type];
            
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

        public IActionResult Index()
        {
            var products = db.Products
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
                Orders = db.Orders
                    .Where(or => or.UserId == (int)TempData.Peek("id"))
                    .ToArray()
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

            var products = db.Products.Where(selector);
            // Filter by custom values
            if (model.CustomValuesInfo != null && model.CustomValuesInfo.FromValues != null && model.CustomValuesInfo.ToValues != null)
            {
                var descriptions = ControllerHelper.FilterCatalogValues(db.GetDescriptions(model.CategoryName + "Descriptions"),
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
