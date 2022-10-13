using la_mia_pizzeria_post.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;

namespace la_mia_pizzeria_post.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        readonly Context _context = new();

        public IActionResult Index()
        {
            List<Pizza> pizzaList = _context.Pizza.Include("Category").Include("Ingredients").ToList();
            return View(pizzaList);
        }
        public IActionResult Details(int id)
        {       
            Pizza pizza = _context.Pizza.Where(Pizza=> Pizza.Id == id).Include("Category").Include("Ingredients").First();
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
            categoriesPizzas.Ingredients = new Context().Ingredient.ToList();
            
            return View(categoriesPizzas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriesPizzas form)
        {
            if (!ModelState.IsValid)
            {
                form.Categories = _context.Category.ToList();
                form.Ingredients = _context.Ingredient.ToList();
                return View("Create", form);
            }
            form.Pizza.Ingredients = _context.Ingredient.Where(ingredient => form.SelectedIngredients.Contains(ingredient.Id)).ToList<Ingredient>();

            Aggiungi(form.Pizza);
            return RedirectToAction("Index");
        }
        
        public IActionResult Update(int id)
        {

            Pizza pizza = _context.Pizza.Where(_ => _.Id == id).Include("Category").Include("Ingredients").First();
            CategoriesPizzas categoriesPizzas = new CategoriesPizzas();
            categoriesPizzas.Pizza = pizza;
            categoriesPizzas.Categories = _context.Category.ToList();
            categoriesPizzas.Ingredients = _context.Ingredient.ToList();

            return View(categoriesPizzas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, CategoriesPizzas form)
        {
            if (!ModelState.IsValid)
            {
                form.Pizza.Id = id;
                form.Pizza.Ingredients = _context.Ingredient.Where(i => form.SelectedIngredients.Contains(i.Id)).ToList();
                form.Categories = _context.Category.ToList();
                form.Ingredients = _context.Ingredient.ToList();
                return View("Update", form);
            }
            Pizza pizza = _context.Pizza.Where(pizza => pizza.Id == id).Include("Ingredients").First();
            pizza.Nome = form.Pizza.Nome;
            pizza.Image = form.Pizza.Image;
            pizza.Descrizione = form.Pizza.Descrizione;
            pizza.CategoryId = form.Pizza.CategoryId;
            pizza.Ingredients = _context.Ingredient.Where(ingredient => form.SelectedIngredients.Contains(ingredient.Id)).ToList();


            _context.Pizza.Update(pizza);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            Pizza pizza = _context.Pizza.Where(_ => _.Id == id).Include("Category").Include("Ingredients").First();
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
        
        public void Aggiungi(Pizza pizza)
        {
            _context.Add(pizza);
            _context.SaveChanges();
        }
       
    }
}