using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly ApplicationDbContext _db;

    protected DbSet<T> dbSet;
    private IEnumerable<Product> prods;

    public Repository(ApplicationDbContext db)
    {
      _db = db;
      prods = _db.products.Include("category");

      dbSet = _db.Set<T>();
    }

    public void Add(T entity)
    {
      dbSet.Add(entity);
    }

    public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, string? includeProperties = null)
    {
      IQueryable<T> query = dbSet;
      query = query.Where(filter);
      if (includeProperties != null)
      {
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
          StringSplitOptions.RemoveEmptyEntries))
        {
          query.Include(includeProperty);
        }
      }

      return query.FirstOrDefault(filter);
    }

    public IEnumerable<T> GetAll(string? includeProperties = null)
    {
      IQueryable<T> query = dbSet;
      if (includeProperties != null)
      {
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' },
          StringSplitOptions.RemoveEmptyEntries))
        {
        query=  query.Include(includeProperty);
        }
      }

      return query.ToList();
    }

    public void Remove(T entity)
    {
      dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
      dbSet.RemoveRange(entities);
    }
  }
}