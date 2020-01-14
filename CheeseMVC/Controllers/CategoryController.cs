using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET /Category/Add
        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();

            return View(addCategoryViewModel);
        }

        // POST /Category/Add
        [HttpPost]
        public IActionResult Add(AddCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory
                {
                    Name = viewModel.Name
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();

                return Redirect("Index");
            }

            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetCategories()
        {
            List<CheeseCategory> categories = context.Categories.ToList();

            return Json(categories);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (context.Categories.Where(c => c.ID == id).Any())
            {
                CheeseCategory cat = context.Categories.Single(c => c.ID == id);
                context.Categories.Remove(cat);
            }
            return NoContent();
        }
    }
}
