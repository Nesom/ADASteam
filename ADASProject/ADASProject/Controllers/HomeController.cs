using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ADASProject.Models;
using System.Linq;
using System.Linq.Expressions;
using ADASProject.Products;
using System;
using Searching;
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

        public void Do()
        {
            var list = new List<Product>()
            {
                new Product(1, "Apple 4", "Apple 4, not Apple 5"),
                new Product(2, "Apple 5", "Apple 5, not Apple 6"),
                new Product(3, "Apple 6", "It's Apple 6"),
                new Product(4, "Just Apple", "Not Apple 4, Apple 5, Apple 6. Just Apple"),
                new Product(5, "Apple", "Simple Apple"),
                new Product(6, "Mega Apple 4 5 6 Simple Just 9999", "China")
            };

            var test1 = Searcher.Search(new List<string>() { "Apple" }, list).ToList();
            var test2 = Searcher.Search(new List<string>() { "Apple", "4" }, list).ToList();
            var test3 = Searcher.Search(new List<string>() { "Apple", "5" }, list).ToList();
            var test4 = Searcher.Search(new List<string>() { "Apple", "6" }, list).ToList();
            var test5 = Searcher.Search(new List<string>() { "Apple", "4", "5" }, list).ToList();
            var test6 = Searcher.Search(new List<string>() { "China" }, list).ToList();

            var a = "aaa";
        }

        [HttpGet]
        public string Cookie()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("cook"))
                HttpContext.Response.Cookies.Append("cook", "pe4enka");
            return HttpContext.Request.Cookies["cook"];
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public async Task<IActionResult> PersonalArea()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Catalog(CatalogModel model)
        {
            model.FillFields();
            bool hasCustomTypes = false;
            if (model.CategoryName != null)
                hasCustomTypes = model.TryToFillCustomFields(model.CategoryName + "Description");

            Expression<Func<ProductInfo, bool>> selector = null;

            if (model.CategoryName == null || !CatalogModel.TypeToNameDict.ContainsKey(model.CategoryName))
                selector = (pr) => true;
            else
                selector = (pr) => pr.TableName == model.CategoryName + "Descriptions";

            var products = db.Products.Where(selector);

            if (model.CustomFromValues != null && model.CustomToValues != null)
            {
                var fromValues = ReflectionHelper.ConvertTypes(model.CustomFromValues, model.CustomTypes);
                var toValues = ReflectionHelper.ConvertTypes(model.CustomToValues, model.CustomTypes);
                var descriptions = db.GetDescriptions(model.CategoryName + "Descriptions");
                var type = ReflectionHelper.FoundType(model.CategoryName + "Description");
                for (int i = 0; i < fromValues.Length; i++)
                    descriptions = ControllerHelper.FilterValues(descriptions, type, model.CustomPropertyNames[i], fromValues[i], toValues[i]);

                var ids = descriptions.Select(d => d.Id).OrderBy(id => id).ToHashSet();
                products = products.Where(pr => ids.Contains(pr.Id));
            }
            
            if (model.FromValues != null && model.ToValues != null)
            {

                var fromValues = ReflectionHelper.ConvertTypes(model.FromValues, model.Types);
                var toValues = ReflectionHelper.ConvertTypes(model.ToValues, model.Types);

                for (int i = 0; i < fromValues.Length; i++)
                    products = ControllerHelper.FilterValues(products, typeof(ProductInfo), model.PropertyNames[i], fromValues[i], toValues[i]);
            }

            
            model.Count = products.Count();
            model.Products = products;
            if (!hasCustomTypes)
                model.CustomTypes = new Type[0];
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
