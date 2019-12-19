using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private static Dictionary<string, string> Cheeses = new Dictionary<string, string>();

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [Route("/Cheese")]
        [Route("/Cheese/Index")]
        [HttpPost]
        public IActionResult RemoveCheese(string [] cheese_remove)
        {
            foreach(string ch in cheese_remove)
            {
                foreach(Cheese cheeseObject in cheese)
                {
                    if (ch.Equals(cheeseObject.Name))
                    {
                        Cheeses.Remove(cheeseObject);
                        break;
                    }
                }
            }
            return Redirect("/Cheese/Index");
        }

        [Route("/Cheese/Add")]
        [HttpPost]
        public IActionResult NewCheese(string name, string description)
        {
            Cheese ch = new Cheese(name, description);
            Cheeses.Add(ch);

            return Redirect("/Cheese");
        }
    }
}
