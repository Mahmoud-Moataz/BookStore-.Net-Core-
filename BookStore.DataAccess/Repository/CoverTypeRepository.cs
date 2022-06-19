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
  public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
  {
    public CoverTypeRepository(ApplicationDbContext db) : base(db)
    {
    }

    public void Update(CoverType obj)
    {
      dbSet.Update(obj);
    }
  }
}