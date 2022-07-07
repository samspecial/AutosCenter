
using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.Models;
using AutosCenter.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AutosWorld.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> categoryList = _unitOfWork.CoverType.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _unitOfWork.CoverType.Add(model);
            _unitOfWork.Save();
            TempData["Success"] = "Cover Type created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var categoryFromDb = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (categoryFromDb == null)
                return NotFound();
            return View(categoryFromDb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _unitOfWork.CoverType.Update(model);
            _unitOfWork.Save();
            TempData["Success"] = "Cover Type updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();
            var category = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
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
            var obj = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
                return NotFound();
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Cover Type deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
