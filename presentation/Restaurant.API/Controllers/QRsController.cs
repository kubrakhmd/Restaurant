
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;
using SkiaSharp;

namespace QRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QRsController(AppDbContext context)
        {
            _context = context;
        }

  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QR>>> GetQRCode()
        {
            if (_context.QRs == null)
            {
                return NotFound();
            }
            return await _context.QRs.ToListAsync();
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<QR>> GetQR(int id)
        {
            if (_context.QRs == null)
            {
                return NotFound();
            }
            var qR = await _context.QRs.FindAsync(id);

            if (qR == null)
            {
                return NotFound();
            }

            return qR;
        }

       
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQR(int id, QR qR)
        {
            if (id != qR.Id)
            {
                return BadRequest();
            }

            _context.Entry(qR).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QRExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

      
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<QR>> PostQR(QR qR)
        {
            if (_context.QRs == null)
            {
                return Problem("Entity set 'AppDbContext.QRCode'  is null.");
            }
            _context.QRs.Add(qR);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQR", new { id = qR.Id }, qR);
        }

        // DELETE: api/QRs/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQR(int id)
        {
            if (_context.QRs == null)
            {
                return NotFound();
            }
            var qR = await _context.QRs.FindAsync(id);
            if (qR == null)
            {
                return NotFound();
            }

            _context.QRs.Remove(qR);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// QR kod yaradır və Swagger UI-də birbaşa göstərir.
        /// </summary>
        /// <param name="productType">Məhsul növü (məs., "food")</param>
        /// <param name="categoryId">Kateqoriya ID-si</param>
        /// <returns>QR kod Base64 formatında</returns>
        /// <response code="200">QR kod Base64 string şəklində qaytarılır.</response>
        /// <response code="404">Kateqoriya tapılmadı.</response>
        [Authorize(Roles = "Admin")]
        [HttpPost("QRCodeGenerator/{productType}/{categoryId}")]
        public async Task<IActionResult> Generate(string productType, short categoryId)
        {
            string qrCodeContent = await GenerateQrCodeContent(productType, categoryId);
            if (string.IsNullOrEmpty(qrCodeContent))
            {
                return NotFound("No foods found for this category.");
            }

            byte[] qrCodeBytes = GenerateQrCodeImage(qrCodeContent);
            string base64Image = Convert.ToBase64String(qrCodeBytes);

            return Ok(new
            {
                ImageUrl = $"data:image/png;base64,{base64Image}"
            });
        }

        /// <summary>
        /// QR kod məzmununu yaradır.
        /// </summary>
        private async Task<string> GenerateQrCodeContent(string productType, short categoryId)
        {
            if (productType.ToLower() == "food")
            {
                var foods = await _context.Foods
                    .Where(f => f.CategoryID == categoryId)
                    .Include(f => f.Category)
                    .ToListAsync();

                if (!foods.Any())
                {
                    return string.Empty;
                }

                return $"Category ID: {categoryId}\n" +
                       $"Foods:\n" +
                       $"{string.Join("\n", foods.Select(f => $"-Name: {f.Name},Description: {f.Description}, Price: ${f.Price}"))}";
            }

            return string.Empty;
        }

        /// <summary>
        /// QR kod PNG şəklini yaradır.
        /// </summary>
        private byte[] GenerateQrCodeImage(string content)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new BitmapByteQRCode(qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(20);

                using (var skBitmap = SKBitmap.Decode(qrCodeImage))
                {
                    using (var skCanvas = new SKCanvas(skBitmap))
                    {
                        // Loqo faylını oxuyuruq
                        string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logo.png");
                        if (System.IO.File.Exists(logoPath))
                        {
                            using (var logoBitmap = SKBitmap.Decode(logoPath))
                            {
                                int logoSize = skBitmap.Width / 5; // Loqonun ölçüsünü müəyyən edirik (QR kodun 1/5 hissəsi)
                                int x = (skBitmap.Width - logoSize) / 2;
                                int y = (skBitmap.Height - logoSize) / 2;

                                var resizedLogo = logoBitmap.Resize(new SKImageInfo(logoSize, logoSize), SKFilterQuality.High);
                                skCanvas.DrawBitmap(resizedLogo, x, y);
                            }
                        }
                    }

                    using (var stream = new MemoryStream())
                    {
                        skBitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
                        return stream.ToArray();
                    }
                }
            }
        }





        private bool QRExists(int id)
        {
            return (_context.QRs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
