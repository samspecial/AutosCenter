using AutosWorld.Data;
using AutosWorld.Models;
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
    }
}
