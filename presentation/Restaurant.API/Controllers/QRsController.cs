
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

        [Authorize(Roles = "Admin")]
        [HttpPost("QRCodeGenerator/{productType}/{categoryId}")]
        public async Task<IActionResult> Generate(string productType, short categoryId)
        {
         
            string qrCodeContent = string.Empty;

            switch (productType.ToLower())
            {
                case "food":
                    var foods = await _context.Foods
                        .Where(f => f.CategoryID == categoryId) 
                        .Include(f => f.Category) 
                        .ToListAsync();

                    if (!foods.Any())
                    {
                        return NotFound("No foods found for this category.");
                    }

                    qrCodeContent = $"Category ID: {categoryId}\n" +
                                    $"Foods:\n" +
                                    $"{string.Join("\n", foods.Select(f => $"-Name: {f.Name},Description: {f.Description}, Price: ${f.Price}"))}";
                    break;  

            }

            byte[] qrCodeBytes;

           
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(qrCodeContent, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new BitmapByteQRCode(qrCodeData);

     
                var qrCodeImage = qrCode.GetGraphic(5);

                using (var skBitmap = SKBitmap.Decode(qrCodeImage))
                {
                    using (var stream = new MemoryStream())
                    {
                        skBitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
                        qrCodeBytes = stream.ToArray();
                    }
                }
            }

            var qr = new QR
            {
                Content = qrCodeContent,
                QRCodeData = Convert.ToBase64String(qrCodeBytes),  
                ProductType = productType,
                ProductId = categoryId, 

            };

            _context.QRs.Add(qr);
            await _context.SaveChangesAsync();


            return File(qrCodeBytes, "image/png");
        }




        private bool QRExists(int id)
        {
            return (_context.QRs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
