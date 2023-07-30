namespace Okaz.API.Models.Interfaces;

public interface IUnitOfWork
{
	IProductRepository ProductRepository {get; }
    Task<bool> SaveAsync();
}