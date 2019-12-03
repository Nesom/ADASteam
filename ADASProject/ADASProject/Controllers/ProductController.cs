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
    public class ProductController : Controller
    {
        IDbContext db;

        public ProductController(IDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Get(GetModel model)
        {
            var product = await db.GetProductAsync(model.Id);

            // Set custom values description
            model.Values = ReflectionHelper.GetCharacteristics(product.Description);
            // Set standart (ProductInfo) values description
            model.StandartValues = ReflectionHelper.GetCharacteristics(product.ProductInfo);
            // Set image field
            model.Image = product.ProductInfo.Image;
            // Set can vote field
            if (TempData.ContainsKey("id"))
                model.CanVote = await db.IsVotedAsync(model.Id, (int)TempData.Peek("id"));
            // Set comments
            var comments = await db.GetCommentsAsync(model.Id);
            foreach (var comment in comments)
            {
                var user = await db.GetUserAsync(comment.UserId);
                model.Comments.Add(Tuple.Create(comment, user.Email));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) => await Get(new GetModel { Id = id });

        [HttpPost]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Comment(int id, string text, string vote)
        {
            int _vote = -1;
            if (Int32.TryParse(vote, out _vote))
                await db.ChangeVoteAsync(id, (int)TempData.Peek("id"), _vote);

            var userId = (int)TempData.Peek("id");
            await db.AddCommentAsync(new Comments.Comment() { UserId = userId, ProductId = id, Text = text });
            return RedirectToAction($"Get/{id}");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveComment(int id)
        {
            var commentId = (await db.GetCommentAsync(id)).ProductId;
            await db.RemoveCommentAsync(id);
            return RedirectToAction($"Get/{commentId}");
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Like(int id)
        {
            await db.LikeCommentAsync((int)TempData.Peek("id"), id);
            if (TempData["GetId"] != null)
                return RedirectToAction($"Get/{TempData["GetId"]}");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Unlike(int id)
        {
            await db.UnlikeCommentAsync((int)TempData.Peek("id"), id);
            if (TempData["GetId"] != null)
                return RedirectToAction($"Get/{TempData["GetId"]}");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetRelationProducts(int id)
        {
            var ids = (await db.GetRelationsAsync())
                .Where(r => r.LesserId == id || r.BiggerId == id)
                .OrderByDescending(r => r.Count)
                .Select(r => r.LesserId == id ? r.BiggerId : r.LesserId);
            var products = new List<ProductInfo>();
            foreach (var productId in ids)
                products.Add(await db.GetProductInfoAsync(id));
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string request)
        {
            var products = await db.GetProductInfosAsync();
            return View(new SearchModel()
            {
                Products = products
                    .Where(pr => request.Contains(pr.Name))
            });
        }

        [HttpPost]
        [Authorize(Roles = "user, admin")]
        public async Task<IActionResult> Rate(int productId, int rate)
        {
            if (!await db.IsVotedAsync(productId, (int)TempData.Peek("id")) && rate > 0 && rate <= 5)
            {
                await db.VoteAsync(productId, (int)TempData.Peek("id"), rate);
            }
            return RedirectToAction("Get/" + productId, "Product");
        }
    }
}