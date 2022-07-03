
using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.Models;
using AutosCenterd.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AutosWorld.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _dbContext;
        public CategoryController(ICategoryRepository dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _dbContext.GetAll();
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
            _dbContext.Add(model);
            _dbContext.Save();
            TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var categoryFromDb = _dbContext.GetFirstOrDefault(x => x.Id == id);
            if (categoryFromDb == null)
                return NotFound();
            return View(categoryFromDb);

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
            _dbContext.Save();
            TempData["Success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();
            var category = _dbContext.GetFirstOrDefault(c => c.Id == id);   
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var obj = _dbContext.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
                return NotFound();
            _dbContext.Remove(obj);
            _dbContext.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
