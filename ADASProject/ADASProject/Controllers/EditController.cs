using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADASProject.Models;
using ADASProject.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADASProject.Controllers
{
    public class EditController : Controller
    {
        ApplicationContext db;

        public EditController(ApplicationContext context)
        {
            db = context;
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
            var productInfo = ReflectionHelper.CreateProductInfo(model.StandartInfoValues, true);
            var product = Product<IDescription>.GetProduct(description, productInfo);
            product.ProductInfo.Image = ControllerHelper.ConvertFileToBytes(model.Image);
            await db.AddProduct(product);
            return RedirectToAction("Index", "Home");
        }
    }
}
