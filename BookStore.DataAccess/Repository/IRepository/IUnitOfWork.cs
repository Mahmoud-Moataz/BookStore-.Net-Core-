using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IRepository
{
  public interface IUnitOfWork : IDisposable
  {
    ICategoryRepository Category { get; }
    ICoverTypeRepository Cover { get; }
    IProductRepository Product { get; }
    ICompanyRepository Company { get; }

    void Save();
  }
}