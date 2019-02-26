using MongoDB.GenericRepository.Interfaces;
using MongoDB.GenericRepository.Model;

namespace MongoDB.GenericRepository.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoContext context) : base(context)
        {
        }
    }
}
