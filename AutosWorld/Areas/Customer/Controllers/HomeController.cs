using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.Models;
using AutosCenter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutosWorld.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperty:"Category,CoverType");
            return View(products);
        }

        public IActionResult Details(int id)
        {
            DetailsPageViewModel details = new()
            {
                Count = 1,
                Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id, includeProperty: "Category,CoverType")
            };
            return View(details);
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
    }
}