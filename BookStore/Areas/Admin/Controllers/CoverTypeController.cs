using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
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
      var covers = _unitOfWork.Cover.GetAll();
      return View(covers);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Cover.Add(obj);
        _unitOfWork.Save();
        TempData["Success"] = "Cover Type Created Successfully!";

        return RedirectToAction("Index");
      }
      return View(obj);
    }

    public IActionResult Edit(int? id)
    {
      if (id == 0 || id == null)
      {
        return NotFound();
      }
      var cover = _unitOfWork.Cover.Get(c => c.Id == id);
      if (cover == null)
      {
        return NotFound();
      }

      return View(cover);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType cover)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Cover.Update(cover);
        _unitOfWork.Save();
        TempData["Success"] = "Cover Type Updated Successfully!";

        return RedirectToAction("Index");
      }

      return View(cover);
    }

    public IActionResult Delete(int? id)
    {
      if (id == 0 || id == null)
      {
        return NotFound();
      }
      var cover = _unitOfWork.Cover.Get(c => c.Id == id);
      if (cover == null)
      {
        return NotFound();
      }
      return View(cover);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
      if (id == 0 || id == null)
      {
        return NotFound();
      }
      var cover = _unitOfWork.Cover.Get(c => c.Id == id);
      if (cover == null)
      {
        return NotFound();
      }
      _unitOfWork.Cover.Remove(cover);
      _unitOfWork.Save();
      TempData["Success"] = "Cover Type Deleted Successfully!";

      return RedirectToAction("Index");
    }
  }
}