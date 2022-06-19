using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Areas.Admin.Controllers
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
      var products = _unitOfWork.Product.GetAll();
      return View(products);
    }

    //public IActionResult Create()
    //{
    //  return View();
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Create(Product obj)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    _unitOfWork.Product.Add(obj);
    //    _unitOfWork.Save();
    //    TempData["Success"] = "Product Created Successfully!";

    //    return RedirectToAction("Index");
    //  }
    //  return View(obj);
    //}

    public IActionResult Upsert(int? id)
    {
      ProductVM productVM = new ProductVM()
      {
        Product = new Product(),
        CategoryList = _unitOfWork.Category.GetAll().Select(
        c => new SelectListItem
        {
          Text = c.Name,
          Value = c.Id.ToString()
        }),

        CoverTypeList = _unitOfWork.Cover.GetAll().Select(
        c => new SelectListItem
        {
          Text = c.Name,
          Value = c.Id.ToString()
        }
        ),
      };
      Product product = new();

      IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.Cover.GetAll().Select(
        c => new SelectListItem
        {
          Text = c.Name,
          Value = c.Id.ToString()
        }
        );
      if (id == 0 || id == null)
      {
        //Create Product
        //ViewBag.categoryList = CategoryList;
        //ViewData["coverTypeList"] = CoverTypeList;
        return View(productVM);
      }
      else
      {
        productVM.Product = _unitOfWork.Product.Get(p => p.Id == id);
        //Update Product
      }

      return View(productVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj, IFormFile? file)
    {
      if (ModelState.IsValid)
      {
        var wwwRootPath = _webHostEnvironment.WebRootPath;
        if (file != null)
        {
          string fileName = Guid.NewGuid().ToString();
          var uploads = Path.Combine(wwwRootPath, @"images\products");
          var extension = Path.GetExtension(file.FileName);
          if (obj.Product.ImageURL != null)
          {
            var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
              System.IO.File.Delete(oldImagePath);
            }
          }
          using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension),
            FileMode.Create))
          {
            file.CopyTo(fileStreams);
          }
          obj.Product.ImageURL = @"images\products\" + fileName + extension;
        }
        if (obj.Product.Id == 0)
        {
          _unitOfWork.Product.Add(obj.Product);
        }
        else
        {
          _unitOfWork.Product.Update(obj.Product);
        }

        _unitOfWork.Save();
        TempData["Success"] = "Product Updated Successfully!";

        return RedirectToAction("Index");
      }

      return View(obj);
    }

    //public IActionResult Delete(int? id)
    //{
    //  if (id == 0 || id == null)
    //  {
    //    return NotFound();
    //  }
    //  var product = _unitOfWork.Product.Get(c => c.Id == id);
    //  if (product == null)
    //  {
    //    return NotFound();
    //  }
    //  return View(product);
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //[ActionName("Delete")]
    //public IActionResult DeletePOST(int? id)
    //{
    //  if (id == 0 || id == null)
    //  {
    //    return NotFound();
    //  }
    //  var product = _unitOfWork.Product.Get(c => c.Id == id);
    //  if (product == null)
    //  {
    //    return NotFound();
    //  }
    //  _unitOfWork.Product.Remove(product);
    //  _unitOfWork.Save();
    //  TempData["Success"] = "Product Deleted Successfully!";

    //  return RedirectToAction("Index");
    //}

    #region API Calls

    [HttpGet]
    public IActionResult GetAll()
    {
      var productList = _unitOfWork.Product.GetAll("category,coverType");
      return Json(new { data = productList });
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int? id)
    {
      //if (id == 0 || id == null)
      //{
      //  return NotFound();
      //}
      var obj = _unitOfWork.Product.Get(c => c.Id == id);
      if (obj == null)
      {
        return Json(new { sucess = false, message = "Error while deleting" });
      }
      var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageURL.TrimStart('\\'));
      if (System.IO.File.Exists(oldImagePath))
      {
        System.IO.File.Delete(oldImagePath);
      }
      _unitOfWork.Product.Remove(obj);
      _unitOfWork.Save();

      return Json(new { sucess = true, message = "product deleted successfully" });

      #endregion API Calls
    }
  }
}