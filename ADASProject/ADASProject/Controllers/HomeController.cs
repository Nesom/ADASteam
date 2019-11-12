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

        [HttpPost]
        public string Get(string request)
        {
            return "Test";
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
                var descriptions = FilterCatalogValues(db.GetDescriptions(model.CategoryName + "Descriptions"),
                    ReflectionHelper.FoundType(model.CategoryName + "Description"),
                    model.CustomValuesInfo);

                var ids = descriptions.Select(d => d.Id).OrderBy(id => id).ToHashSet();
                products = products.Where(pr => ids.Contains(pr.Id));
            }
            // Filter by main values
            if (model.StandartValuesInfo != null && model.StandartValuesInfo.FromValues != null && model.StandartValuesInfo.ToValues != null)
            {
                products = FilterCatalogValues(products, typeof(ProductInfo), model.StandartValuesInfo);
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

        public IQueryable<T> FilterCatalogValues<T>(IQueryable<T> values, Type type, CatalogValuesInfo valuesInfo)
        {
            var fromValuesObj = ReflectionHelper.ConvertTypes(valuesInfo.FromValues, valuesInfo.Types);
            var toValuesObj = ReflectionHelper.ConvertTypes(valuesInfo.ToValues, valuesInfo.Types);

            for (int i = 0; i < valuesInfo.Types.Length; i++)
                values = ControllerHelper.FilterValues(values, type, valuesInfo.PropertyNames[i], fromValuesObj[i], toValuesObj[i]);

            return values;
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
