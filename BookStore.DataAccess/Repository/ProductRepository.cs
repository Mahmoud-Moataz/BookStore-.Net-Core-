using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
  public class ProductRepository : Repository<Product>, IProductRepository
  {
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db) : base(db)
    {
      _db = db;
    }

    public void Update(Product obj)
    {
      //Here It's the same way of using update(obj) but it gives a more flexiability to choose which proberty
      //you want to update to net assigned to null if it's not existing in "obj"
      var objFromDb = _db.products.FirstOrDefault(p => p.Id == obj.Id);
      if (objFromDb != null)
      {
        objFromDb.Title = obj.Title;
        objFromDb.Description = obj.Description;
        objFromDb.Price = obj.Price;
        objFromDb.ListPrice = obj.ListPrice;
        objFromDb.Price50 = obj.Price50;
        objFromDb.Price100 = obj.Price100;
        objFromDb.ISBN = obj.ISBN;
        objFromDb.Author = obj.Author;
        objFromDb.CategoryId = obj.CategoryId;
        objFromDb.CoverTypeId = obj.CoverTypeId;
        if (obj.ImageURL != null)
        {
          objFromDb.ImageURL = obj.ImageURL;
        }
      }
    }
  }
}