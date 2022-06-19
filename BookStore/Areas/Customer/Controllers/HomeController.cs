using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStoreWeb.Areas.Customer.Controllers
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
      var productList = _unitOfWork.Product.GetAll("category,coverType");
      return View(productList);
    }

    public IActionResult Details(int id)
    {
      ShoppingCart cartObj = new ShoppingCart()
      {
        Product = _unitOfWork.Product.Get(p => p.Id == id, "category,coverType"),
        Count = 1,
      };
      return View(cartObj);
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