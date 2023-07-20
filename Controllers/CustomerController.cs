using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI_E2.Data;
using WEBAPI_E2.Models;

namespace WEBAPI_E2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly WEBAPI_E2DbContext _webapie2DbContext;

        public CustomerController(WEBAPI_E2DbContext webapie2DbContext)
        {
            _webapie2DbContext = webapie2DbContext;
        }

        [HttpGet("Show")]
        public IActionResult Show()
        {

            //var prod = _webapie2DbContext.Products.Include(c => c.Category).Where(c => !c.IsDeleted).AsNoTracking().ToList();

            var image = _webapie2DbContext.Image.ToList();
            // prod[0].Category= _webapie2DbContext.Categories.Find(prod[0].CategoryId);
            return Ok(image);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file,int ProductId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                // var image = new ImageModel { Image = memoryStream.ToArray() };
                var imageModel = new ImageModel { ProductId = ProductId, ImageData = memoryStream.ToArray(), Id = 0 };
                 _webapie2DbContext.Image.Add(imageModel);
                _webapie2DbContext.SaveChanges();
               // return Ok(new { ImageId = image.Id });\
               return Ok(imageModel);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Download(long id)
        {
            var image = _webapie2DbContext.Image.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }
    }
}
