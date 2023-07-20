using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI_E2.Models;
using System;
using System.Linq;
using WEBAPI_E2.Data; 

namespace WEBAPI_E2.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly WEBAPI_E2DbContext _webapie2DbContext;

        public ProductController(WEBAPI_E2DbContext webapie2DbContext)
        {
            _webapie2DbContext = webapie2DbContext;
        }
        /*
        [HttpGet("Start")]
        public IActionResult Start()
        {
            return Ok();
        }
        */
        [HttpGet("Show")]
        public IActionResult Show()
        {

            //var prod = _webapie2DbContext.Products.Include(c => c.Category).Where(c => !c.IsDeleted).AsNoTracking().ToList();
            
           var prod = _webapie2DbContext.Products.Where(x => !x.IsDeleted).ToList();
           // prod[0].Category= _webapie2DbContext.Categories.Find(prod[0].CategoryId);
            return Ok(prod);
        }

        [HttpGet("Image")]
        public IActionResult Image()
        {

            //var prod = _webapie2DbContext.Products.Include(c => c.Category).Where(c => !c.IsDeleted).AsNoTracking().ToList();
            var image = _webapie2DbContext.Image.ToList();
            var prod = _webapie2DbContext.Products.Where(x => !x.IsDeleted).ToList();
            var joined = prod.Join(image,
                 p1 => p1.ProductId,
                 p2 => p2.ProductId,
                  (p1, p2) => new
                  {
                      ProductId = p1.ProductId,
                      name = p1.Name,
                      ImageData = p2.ImageData,
                      description = p1.Description,
                      price = p1.Price,
                      categoryId = p1.CategoryId
                  });
            // prod[0].Category= _webapie2DbContext.Categories.Find(prod[0].CategoryId);
            return Ok(joined);
        }
        [HttpGet("Find/{id}")]
        public IActionResult Find(long id)
        {
            var product = _webapie2DbContext.Products.AsNoTracking().Include(c => c.Category).FirstOrDefault(x => x.ProductId == id);
            if (product == null || product.IsDeleted)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            var cat = _webapie2DbContext.Categories.AsNoTracking().ToList();
            return Ok(cat);
        }
        /*
        [HttpPost("Create")]
        public IActionResult Create(ProductModel ProductAdd)
        {
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);

            if (ModelState.IsValid)
            {
                var cat = _webapie2DbContext.Categories.AsNoTracking().ToList();
                var cats = cat.Select(p => p.CategoryId).ToList();

                if (cats.Contains(ProductAdd.Category))
                {
                    if (ProductAdd.ManDate.HasValue && ProductAdd.ExpDate.HasValue)
                    {
                        int s = DateTime.Compare((DateTime)ProductAdd.ManDate, (DateTime)ProductAdd.ExpDate);
                        if (s > 0)
                        {
                            return BadRequest("Invalid date range");
                        }
                        else
                        {
                            ProductAdd.IsDeleted = false;
                            ProductAdd.CreatedBy = UserId;
                            ProductAdd.CreatedDate = DateTime.Now;
                            ProductAdd.LastModifiedBy = UserId;
                            ProductAdd.LastModifiedDate = DateTime.Now;
                            _webapie2DbContext.Products.Add(ProductAdd);
                            _webapie2DbContext.SaveChanges();
                            return CreatedAtAction(nameof(Find), new { id = ProductAdd.ProductId }, ProductAdd);
                        }
                    }

                    ProductAdd.Category = _webapie2DbContext.Categories.Find(ProductAdd.Category);
                    ProductAdd.IsDeleted = false;
                    ProductAdd.CreatedBy = UserId;
                    ProductAdd.CreatedDate = DateTime.Now;
                    ProductAdd.LastModifiedBy = UserId;
                    ProductAdd.LastModifiedDate = DateTime.Now;
                    _webapie2DbContext.Products.Add(ProductAdd);
                    _webapie2DbContext.SaveChanges();
                    return CreatedAtAction(nameof(Find), new { id = ProductAdd.ProductId }, ProductAdd);
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit(long id, ProductModel edit)
        {
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);
            var product = _webapie2DbContext.Products.Include(c => c.Cat).FirstOrDefault(x => x.ProductId == id);
            if (product == null || product.IsDeleted)
            {
                return NotFound();
            }

            product.Cat = _webapie2DbContext.Categories.Find(edit.Category);
            product.Name = edit.Name;
            product.Brand = edit.Brand;
            product.Made = edit.Made;
            product.ManDate = edit.ManDate;
            product.ExpDate = edit.ExpDate;
            product.Description = edit.Description;
            product.Price = edit.Price;
            product.Category = edit.Category;
            product.LastModifiedBy = UserId;
            product.LastModifiedDate = DateTime.Now;
            _webapie2DbContext.SaveChanges();
            return NoContent();
        }
        
        [HttpDelete("DeleteCheck/{id}")]
        public IActionResult DeleteCheck(long id)
        {
            var prod = _webapie2DbContext.Products.Find(id);
            if (prod == null)
            {
                return NotFound();
            }

            var stock = _webapie2DbContext.Stocks.Where(c => c.ProductId == prod.ProductId).ToList();
            if (stock.Any())
            {
                var stocklist = stock.Select(c => c.StockId.ToString()).ToList();
                TempData["Stocktd"] = stocklist;
                TempData["id"] = id;
                return Ok(new { Stocktd = stocklist });
            }
            else
            {
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            long UserId = Convert.ToInt64(Request.Cookies["CurrentUser"]);
            string[] sd = (string[])TempData["Stocktd"];

            if (sd != null)
            {
                long[] Stocktd = sd.Select(long.Parse).ToArray();

                foreach (var stocktd in Stocktd)
                {
                    var Stock = _e2DbContext.Stocks.Find(stocktd);
                    if (Stock == null)
                    {
                        return NotFound();
                    }
                    Stock.isDeleted = true;
                    Stock.LastModifiedDate = DateTime.Now;
                    Stock.LastModifiedBy = UserId;
                }
                _e2DbContext.SaveChanges();
            }

            var prod = _e2DbContext.Products.Include(c => c.Cat).FirstOrDefault(x => x.ProductId == id);
            if (prod == null || prod.IsDeleted)
            {
                return NotFound();
            }

            prod.IsDeleted = true;
            prod.LastModifiedBy = UserId;
            prod.LastModifiedDate = DateTime.Now;
            _e2DbContext.SaveChanges();
            return NoContent();
        }*/
    }
}

