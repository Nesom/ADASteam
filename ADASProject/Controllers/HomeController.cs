using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ADASProject.Models;

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
        public async Task<IActionResult> Catalog()
        {
            var model = new CatalogModel()
            {
                ProductsByCategory = ControllerHelper.GetProductsByCategory(db.Products)
            };
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
