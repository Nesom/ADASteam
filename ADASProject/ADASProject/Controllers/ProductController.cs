using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADASProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADASProject.Controllers
{
    public class ProductController : Controller
    {
        ApplicationContext db;

        public ProductController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Get(GetModel model)
        {
            var product = db.GetProduct(model.Id);
            model.Values = ReflectionHelper.GetCharacteristics(product.Description);
            model.StandartValues = ReflectionHelper.GetCharacteristics(product.ProductInfo);
            model.Image = product.ProductInfo.Image;
            var comments = db.GetComments(model.Id);
            foreach (var comment in comments)
            {
                var user = db.GetUser(comment.UserId);
                model.Comments.Add(Tuple.Create(comment, user.Email));
            }
            model.Context = db;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) => await Get(new GetModel { Id = id });

        [HttpPost]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Comment(int id, string text)
        {
            var userId = (int)TempData.Peek("id");
            db.AddComment(userId, id, text);
            return RedirectToAction($"Get/{id}");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveComment(int id)
        {
            var commentId = db.GetComment(id).ProductId;
            db.RemoveComment(id);
            return RedirectToAction($"Get/{commentId}");
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Like(int id)
        {
            db.LikeComment((int)TempData.Peek("id"), id);
            if (TempData["GetId"] != null)
                return RedirectToAction($"Get/{TempData["GetId"]}");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Unlike(int id)
        {
            db.UnlikeComment((int)TempData.Peek("id"), id);
            if (TempData["GetId"] != null)
                return RedirectToAction($"Get/{TempData["GetId"]}");
            return RedirectToAction("Index", "Home");
        }
    }
}