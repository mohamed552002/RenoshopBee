using RenoshopBee.Models;

namespace RenoshopBee.Interfaces.ProductInterfaces
{
    public interface IProductSortTechnique
    {
        IQueryable<Product> SortTechnique(IQueryable<Product> products);
    }
    public class SortByPrice : IProductSortTechnique
    {
        public IQueryable<Product> SortTechnique(IQueryable<Product> products)
        {
            return products.OrderBy(p=>p.Price);
        }
    }
    public class SortByModifiedDate : IProductSortTechnique
    {
        public IQueryable<Product> SortTechnique(IQueryable<Product> products)
        {
            return products.OrderBy(p => p.Created_at);
        }
    }
    
}
