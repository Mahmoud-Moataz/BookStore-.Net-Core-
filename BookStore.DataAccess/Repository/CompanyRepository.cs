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
  public class CompanyRepository : Repository<Company>, ICompanyRepository
  {
    public CompanyRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void Update(Company obj)
    {
      dbSet.Update(obj);
    }
  }
}