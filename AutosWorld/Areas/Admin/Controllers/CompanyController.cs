
using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.Models;
using AutosCenter.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutosCenter.Models.ViewModels;

namespace AutosWorld.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
           
            return View();
        }
    
        public IActionResult Upsert(int? id)
        {
            Company company = new();
            if (id == null || id == 0)
            {
               
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstOrDefault(p => p.Id == id);
                return View(company);

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company model)
        {
           
            if (ModelState.IsValid)
            {
               
                if(model.Id == 0)
                {
                    _unitOfWork.Company.Add(model);
                    TempData["Success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(model);
                    TempData["Success"] = "Company updated successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var company = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
            if(company == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _unitOfWork.Company.Remove(company);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

        #region API CALLS  
         
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyDirectory = _unitOfWork.Company.GetAll();
            return Json(new { data = companyDirectory });
        }

        #endregion
    }
}
