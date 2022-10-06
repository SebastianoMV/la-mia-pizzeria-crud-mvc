using la_mia_pizzeria_post.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace la_mia_pizzeria_post.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        { 
            return View(Pizze());
        }

        public IActionResult Details(int id)
        {
            Context db = new Context();
            Pizza pizza = (Pizza)db.Pizza.Where(pizze=> pizze.Id == id).First();
            return View(pizza);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }
            Aggiungi(pizza);
            return RedirectToAction("Index");
        }
        
        public IActionResult Update(int id)
        {
            Context db = new Context();
            Pizza pizza = db.Pizza.Where(_ => _.Id == id).First();
            return View(pizza);
        }
        [HttpPost]
        public IActionResult Update(int id , Pizza form)
        {
            Context db = new Context();
            Pizza pizza = db.Pizza.Where(_ => _.Id == id).First();
            pizza.Nome = form.Nome;
            pizza.Descrizione = form.Descrizione;
            pizza.Image = form.Image;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Context db = new Context();
            Pizza pizza = db.Pizza.Where(_ => _.Id == id).First();
            db.Pizza.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<Pizza> Pizze()
        {
            Context db = new Context();
            List<Pizza> pizze = db.Pizza.ToList<Pizza>();
            return pizze;
        }


        public void Aggiungi(Pizza pizza)
        {
            Context db = new Context();
            db.Add(pizza);
            db.SaveChanges();

        }

    }
}