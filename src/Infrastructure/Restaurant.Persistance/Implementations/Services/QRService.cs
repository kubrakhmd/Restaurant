
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Application.Abstractions.Services;

using Restaurant.Domain.Models;
using Restaurant.Persistence.Implementations.Repositories;

namespace Restaurant.Persistence.Implementations.Service
{
     internal  class QRCodeService : IQRService
    {
        private readonly QRRepository _qrCodeRepository;

        public QRCodeService(QRRepository qrRepository)
        {
            _qrCodeRepository = qrRepository;
        }

        public async Task<QR> GetQRCodeByProductTypeAndCategoryIdAsync(string productType, int categoryId)
        {
            return await _qrCodeRepository.GetQRCodeByProductTypeAndCategoryIdAsync(productType, categoryId);
        }

        public async Task AddQRCodeAsync(QR qr)
        {
            await _qrCodeRepository.AddAsync(qr);
            await _qrCodeRepository.SaveChangesAsync();
        }

        public async Task UpdateQRCodeAsync(QR qr)
        {
            _qrCodeRepository.Update(qr);
            await _qrCodeRepository.SaveChangesAsync();
        }

        public async Task DeleteQRCodeAsync(QR qr)
        {
            _qrCodeRepository.Delete(qr);
            await _qrCodeRepository.SaveChangesAsync();
        }

      
    }
}
