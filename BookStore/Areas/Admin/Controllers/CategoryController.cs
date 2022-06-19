using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class CategoryController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      var objCategoriyList = _unitOfWork.Category.GetAll();
      return View(objCategoriyList);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
      if (obj.Name == obj.DisplayOrder.ToString())
      {
        ModelState.AddModelError("CustomError", "The DisplayOrder can't Exactly match the Name.");
      }
      if (ModelState.IsValid)
      {
        _unitOfWork.Category.Add(obj);
        _unitOfWork.Save();
        TempData["Success"] = "Category Created Successfully!";
        return RedirectToAction("Index");
      }
      return View(obj);
    }

    //I made id a nullable int because of warning in if condition id==null
    public IActionResult Edit(int? id)
    {
      if (id == 0 || id == null)
      {
        return NotFound();
      }
      var category = _unitOfWork.Category.Get(c => c.Id == id);

      if (category == null)
      {
        return NotFound();
      }
      return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
      if (obj.Name == obj.DisplayOrder.ToString())
      {
        ModelState.AddModelError("CustomError", "The DisplayOrder can't Exactly match the Name.");
      }
      if (ModelState.IsValid)
      {
        _unitOfWork.Category.Update(obj);
        _unitOfWork.Save();
        TempData["Success"] = "Category Edited Successfully!";
        return RedirectToAction("Index");
      }
      return View(obj);
    }

    //I made id a nullable int because of warning in if condition id==null
    public IActionResult Delete(int? id)
    {
      if (id == 0 || id == null)
      {
        return NotFound();
      }
      var category = _unitOfWork.Category.Get(c => c.Id == id);

      if (category == null)
      {
        return NotFound();
      }
      return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
      if (id == 0 || id == null)
      {
        return NotFound();
      }
      var category = _unitOfWork.Category.Get(c => c.Id == id);

      if (category == null)
      {
        return NotFound();
      }
      _unitOfWork.Category.Remove(category);
      _unitOfWork.Save();
      TempData["Success"] = "Category Deleted Successfully!";

      return RedirectToAction("Index");
    }
  }
}