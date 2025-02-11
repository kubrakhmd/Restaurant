
using Restaurant.Domain.Models;

namespace Restaurant.Application.Abstractions.Repositories
{

        public interface IQRRepository : IRepository<QR>
        {
            Task<QR> GetQRCodeByProductTypeAndCategoryIdAsync(string productType, int categoryId) ;
        }
    
}
