
using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.Models;
using AutosCenter.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutosCenter.Models.ViewModels;

namespace AutosWorld.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
           
            return View();
        }
    
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productVM = new()
            {
                Product = new(),
                CategoryList  = _unitOfWork.Category.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),

            };
            if (id == null || id == 0)
            {
               
                return View(productVM);
            }
            else
            {
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
                return View(productVM);

            }
           
            return View(productVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel viewModel, IFormFile? file)
        {
           
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if(viewModel.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, viewModel.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    viewModel.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if(viewModel.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(viewModel.Product);
                    //TempData["Success"] = "Product created successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(viewModel.Product);
                    //TempData["Success"] = "Product updated successfully";
                }
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully";   
                return RedirectToAction("Index");
            }
            return View(viewModel);
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

        [HttpDelete]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if(product == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }

        #region API CALLS
         
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperty:"Category,CoverType");
            return Json(new { data = productList });
        }

        #endregion
    }
}
