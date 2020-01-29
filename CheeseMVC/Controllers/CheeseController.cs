using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Authorization;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext Context { get; }
        private IAuthorizationService AuthorizationService { get; }
        private UserManager<IdentityUser> UserManager { get; }

        public CheeseController(CheeseDbContext dbContext,
                            IAuthorizationService authorizationService,
                            UserManager<IdentityUser> userManager) : base()
        {
            Context = dbContext;
            AuthorizationService = authorizationService;
            UserManager = userManager;
        }

        // GET: /<controller>/
        // /Cheese/Index -- Get request
        public IActionResult Index()
        {
            var cheeses = from c in Context.Cheeses
                          select c;

            var isAdminAuthorized = User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAdminAuthorized)
            {
                cheeses = cheeses.Where(c => c.UserID == currentUserId);
            }

            return View(cheeses.Include(c => c.Category).ToList());
        }

        [Route("/Cheese")]
        [Route("/Cheese/Index")]
        [HttpPost]
        public IActionResult RemoveCheese(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese ch = Context.Cheeses.Single(c => c.ID == cheeseId);
                Context.Cheeses.Remove(ch);
            }

            Context.SaveChanges();

            return Redirect("/Cheese/Index");
        }

        public IActionResult Add()
        {
            AddCheeseViewModel addCheeseViewModel =
                new AddCheeseViewModel(Context.Categories.ToList());

            return View(addCheeseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCheeseCategory =
                    Context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);


                Cheese newCheese = new Cheese()
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Rating = addCheeseViewModel.Rating,
                    Category = newCheeseCategory,
                    UserID = UserManager.GetUserId(User)
                };

                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                    User, newCheese,
                                                    CheeseOperations.Create);

                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                Context.Cheeses.Add(newCheese);
                Context.SaveChanges();

                return Redirect("/Cheese/Index");
            }

            return View(addCheeseViewModel);
        }

        // GET /Cheese/Edit?cheeseId=#
        public IActionResult Edit(int cheeseId)
        {
            Cheese ch = Context.Cheeses.Single(c => c.ID == cheeseId);

            AddEditCheeseViewModel vm = new AddEditCheeseViewModel(ch, Context.Categories.ToList());

            return View(vm);
        }

        // POST /Cheese/Edit
        [HttpPost]
        public IActionResult Edit(AddEditCheeseViewModel vm)
        {
            // Validate the form data
            if (ModelState.IsValid)
            {
                Cheese ch = Context.Cheeses.Single(c => c.ID == vm.CheeseId);
                ch.Name = vm.Name;
                ch.Description = vm.Description;
                ch.Category = Context.Categories.Single(c => c.ID == vm.CategoryID);
                ch.Rating = vm.Rating;

                Context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(vm);
        }


    }
}
