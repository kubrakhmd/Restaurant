
using Restaurant.Domain.Models;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IQRService
    {
        Task<QR> GetQRCodeByProductTypeAndCategoryIdAsync(string productType, int categoryId);
        Task AddQRCodeAsync(QR qr);
        Task UpdateQRCodeAsync(QR qr);
        Task DeleteQRCodeAsync(QR qr);
    }
}
