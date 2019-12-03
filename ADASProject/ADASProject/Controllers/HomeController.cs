using ADASProject.Models;
using ADASProject.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            var ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var city = Autodetect.GetCity(ip);

            if (city != null)
                TempData["city"] = city;
            else
                TempData["city"] = "Kazan'";

            var products = (await db.GetProductInfosAsync())
                .OrderByDescending(pr => pr.AddDate)
                .Take(4)
                .ToArray();

            return View(new IndexModel() { First4Products = products });
        }

        #region HomeWork

        public static Dictionary<Type, Tuple<string, PropertyInfo, bool>[]> cache { get; }
            = new Dictionary<Type, Tuple<string, PropertyInfo, bool>[]>();

        [HttpGet]
        public async Task<IActionResult> EditerT(int id)
        {
            //var product = await db.GetProductInfoAsync(id);

            //var type = product.GetType();

            //var timer = new Stopwatch();

            //timer.Start();

            //var model = new EditerTModel();
            //if (!cache.ContainsKey(type))
            //{
            //    var properties = type.GetProperties();
            //    var types = properties
            //        .Select(pr => Tuple.Create(pr.Name, pr, pr.GetCustomAttributes(typeof(Attributes.ClassName), false) == null))
            //        .ToArray();
            //    cache[type] = types;
            //}
            //model.Types = cache[type];
            //model.StandartValues = new object[model.Types.Length];
            //for (int i = 0; i < model.Types.Length; i++)
            //{
            //    model.StandartValues[i] = model.Types[i].Item2.GetValue(product);
            //}
            //timer.Stop();
            //model.TimeInTicks = timer.ElapsedTicks;
            //return View(model);

            var type = Expression.Parameter(typeof(Type), "type");

            var method = typeof(Type).GetMethod("GetProperties");

            

            var properties =MethodCallExpression.Call(method, type);

            var lambda = Expression.Lambda<Func<Type, object>>(properties, type);
            var parser = lambda.Compile();

            var t = parser(typeof(ProductInfo));

            var d = (PropertyInfo[])t;

            return View();

            //for (int i = 0; i < properties.Length; i++)
            //{
            //    assigments.Add(Expression.Bind(Expression.Call(properties[i].GetValue(obj), toStr),
            //        Expression.ArrayIndex(values, Expression.Constant(i))));
            //}

            //var body = Expression.MemberInit(
            //    Expression.New(typeof(TParameters)
            //        .GetConstructor(new Type[0])),
            //    assigments);
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
