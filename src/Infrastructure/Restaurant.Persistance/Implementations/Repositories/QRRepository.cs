using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;
using Microsoft.EntityFrameworkCore;


using Restaurant.Persistence.Implementations.Repostories.Generic;

namespace Restaurant.Persistence.Implementations.Repositories
{
         internal  class QRRepository : Repository<QR>, IQRRepository
    {
    
        public QRRepository(AppDbContext context) : base(context) { }

        public async Task<QR> GetQRCodeByProductTypeAndCategoryIdAsync(string productType, int categoryId)
        {
            return await GetAll(x => x.ProductType == productType && x.ProductId == categoryId)
                         .FirstOrDefaultAsync();
        }
    }
}

