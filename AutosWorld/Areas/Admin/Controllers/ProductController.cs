
using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.Models;
using AutosCenter.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutosWorld.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll();
            return View(productList);
        }
    
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString()});
            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            if (id == null || id == 0)
            {
                ViewBag.CategoryList = CategoryList;
                ViewBag.CoverTypeList = CoverTypeList;
                return View(product);
            }
            else
            {

            }
            var categoryFromDb = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (categoryFromDb == null)
                return NotFound();
            return View(categoryFromDb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _unitOfWork.Product.Update(model);
            _unitOfWork.Save();
            TempData["Success"] = "Cover Type updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();
            var category = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
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
            var obj = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
                return NotFound();
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Cover Type deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
