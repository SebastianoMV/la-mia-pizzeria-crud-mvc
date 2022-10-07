using la_mia_pizzeria_post.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace la_mia_pizzeria_post.Controllers
{
    public class PizzaController : Controller
    {
        readonly Context _context = new();

        public IActionResult Index()
        {
            List<Pizza> pizzaList = _context.Pizza.Include("Category").ToList();
            return View(Pizze());
        }
        public IActionResult Details(int id)
        {       
            Pizza pizza = _context.Pizza.Where(Pizza=> Pizza.Id == id).Include("Category").First();
            if (pizza == null)
            {
                return NotFound($"La Pizza con id {id} non è stato trovato");
            }
            else
            {
                return View("Details", pizza);
            }
        }

        public IActionResult Create()
        {
            CategoriesPizzas categoriesPizzas = new CategoriesPizzas();
            categoriesPizzas.Categories = new Context().Category.ToList();
            
            return View(categoriesPizzas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriesPizzas form)
        {
            if (!ModelState.IsValid)
            {
                form.Categories = _context.Category.ToList();
                return View("Create", form);
            }
            Aggiungi(form.Pizza);
            return RedirectToAction("Index");
        }
        
        public IActionResult Update(int id)
        {
            
            Pizza pizza = FindPizza(id);
            CategoriesPizzas categoriesPizzas = new CategoriesPizzas();
            categoriesPizzas.Pizza = pizza;
            categoriesPizzas.Categories = _context.Category.ToList();

            return View(categoriesPizzas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id , CategoriesPizzas form)
        {

            form.Pizza.Id = id;
            _context.Pizza.Update(form.Pizza);

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