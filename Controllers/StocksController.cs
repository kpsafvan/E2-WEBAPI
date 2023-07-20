using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPI_E2.Data;
using WEBAPI_E2.Models;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace WEBAPI_E2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly WEBAPI_E2DbContext _webapie2DbContext;

        public StockController(WEBAPI_E2DbContext webapie2DbContext)
        {
            _webapie2DbContext = webapie2DbContext;
        }

        // GET: api/Stock
        [HttpGet("Show")]

        public ActionResult<IEnumerable<StockModel>> Get()
        {
            /*var stocks = _webapie2DbContext.Stocks
                .Where(p => p.Product.ExpDate <= DateTime.Today || p.Product.ExpDate == null)
                .Where(p => !p.isDeleted)
                .Include(c => c.Product)
                .Include(x => x.Location)
                .ToList();
            */
            var stocks = _webapie2DbContext.Stocks.ToList();
            if (stocks!=null)
            {
                return Ok(stocks);

            }
            else
            {
                return BadRequest();
            }


        }

        // GET: api/Stock/5
        [HttpGet("{id}")]
        public ActionResult<StockModel> Get(long id)
        {
            /*var stock = _webapie2DbContext.Stocks
                .Include(c => c.Product)
                .Include(x => x.Location)
                .FirstOrDefault(x => x.StockId == id);
            */
            var stock = _webapie2DbContext.Stocks
                .FirstOrDefault(x => x.StockId == id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }
        

        // POST: api/Stock
        [HttpPost]
        public ActionResult<StockModel> Post(StockModel stockAdd)
        {
            long user = Convert.ToInt64(Request.Cookies["CurrentUser"]);

            var prod = _webapie2DbContext.Products
                .Where(t => !t.IsDeleted)
                .FirstOrDefault(x => x.ProductId == stockAdd.ProductId);
            if (ModelState.IsValid && prod != null)
            {
                stockAdd.isDeleted = false;
                stockAdd.CreatedDate = DateTime.Now;
                stockAdd.CreatedBy = user;
                stockAdd.LastModifiedDate = DateTime.Now;
                stockAdd.LastModifiedBy = user;
                _webapie2DbContext.Stocks.Add(stockAdd);
                _webapie2DbContext.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = stockAdd.StockId }, stockAdd);
            }
            return BadRequest();
        }
        /*
        // PUT: api/Stock/5
        [HttpPut("{id}")]
        public ActionResult<StockModel> Put(long id, StockModel edit)
        {
            long userId = Convert.ToInt64(Request.Cookies["CurrentUser"]);

            var stock = _e2DbContext.Stocks
                .Include(c => c.Product)
                .Include(x => x.Location)
                .FirstOrDefault(x => x.StockId == id);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Quantity = edit.Quantity;
            stock.LocationId = edit.LocationId;
            stock.Location = edit.Location;
            stock.LastModifiedBy = userId;
            stock.LastModifiedDate = DateTime.Now;

            _e2DbContext.SaveChanges();

            return Ok(stock);
        }

        // DELETE: api/Stock/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var user = Request.Cookies["CurrentUser"];

            var stock = _e2DbContext.Stocks
                .Include(c => c.Product)
                .Include(x => x.Location)
                .FirstOrDefault(x => x.StockId == id);

            if (stock == null)
            {
                return NotFound();
            }

            stock.isDeleted = true;
            stock.LastModifiedDate = DateTime.Now;
            stock.LastModifiedBy = Convert.ToInt64(user);

            _e2DbContext.SaveChanges();

            return Ok(stock);
        }*/
    }
}
