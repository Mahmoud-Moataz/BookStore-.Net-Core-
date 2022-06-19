using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
  public class UnitOfWork : IUnitOfWork
  {
    public ICategoryRepository Category { get; private set; }

    public ICoverTypeRepository Cover { get; private set; }
    public IProductRepository Product { get; private set; }
    public ICompanyRepository Company { get; private set; }

    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
      _db = db;
      Category = new CategoryRepository(db);
      Cover = new CoverTypeRepository(db);
      Product = new ProductRepository(db);
      Company = new CompanyRepository(db);
    }

    public void Dispose()
    {
      _db.Dispose();
    }

    public void Save()
    {
      _db.SaveChanges();
    }
  }
}