
using AutosCenter.Models;
using AutosCenterd.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AutosWorld.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _dbContext.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
                ModelState.AddModelError("Custom Error", "Display name cannot match Name");
            if (!ModelState.IsValid)
                return View(model);
            _dbContext.Categories.Add(model);
            _dbContext.SaveChanges();
            TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var category = _dbContext.Categories.Find(id);
            var categoryFromDb = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryFromDb == null || category == null)
                return NotFound();
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model)
        {
            if (model.Name == model.DisplayOrder.ToString())
                ModelState.AddModelError("Custom error", "Display name mismatch");
            if (!ModelState.IsValid)
                return View(model);
            
            _dbContext.Update(model);
            _dbContext.SaveChanges();
            TempData["Success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var category = _dbContext.Categories.Find(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = _dbContext.Categories.Find(id);
            if (obj == null)
                return NotFound();
            _dbContext.Categories.Remove(obj);
            _dbContext.SaveChanges();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
