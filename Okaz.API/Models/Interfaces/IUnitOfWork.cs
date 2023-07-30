namespace Okaz.API.Models.Interfaces;

public interface IUnitOfWork
{
  IProductRepository ProductRepository { get; }
  ICategoryRepository CategoryRepository { get; }
  Task<bool> SaveAsync();
}