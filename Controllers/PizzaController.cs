using la_mia_pizzeria_post.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace la_mia_pizzeria_post.Controllers
{
   
    public class PizzaController : Controller
    {
        
        readonly Context _context = new();

        public IActionResult Index()
        { 
            return View(Pizze());
        }

        public IActionResult Details(int id)
        {
            
            Pizza pizza = (Pizza)_context.Pizza.Where(pizze=> pizze.Id == id).First();
            return View(pizza);
        }

        public IActionResult Create()
        {
            CategoriesPizzas categoriesPizzas = new CategoriesPizzas();
            categoriesPizzas.Categories = new Context().Category.ToList();
            
            return View(categoriesPizzas);
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
            
            Pizza pizza = FindPizza(id);
            return View(pizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id , Pizza form)
        {
            
            _context.Pizza.Update(form);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            
            Pizza pizza = FindPizza(id);
            _context.Pizza.Remove(pizza);
            _context.SaveChanges();
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
            
            List<Pizza> pizze = _context.Pizza.ToList<Pizza>();
            return pizze;
        }

        public void Aggiungi(Pizza pizza)
        {

            _context.Add(pizza);
            _context.SaveChanges();
        }

        public Pizza FindPizza(int id)
        {
            
            Pizza pizza = _context.Pizza.Where(_ => _.Id == id).First();
            return pizza;
        }
    }
}