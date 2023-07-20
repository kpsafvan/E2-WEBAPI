using WEBAPI_E2.Data;
using WEBAPI_E2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WEBAPI_E2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly WEBAPI_E2DbContext _dbContext;

        public CategoryController(WEBAPI_E2DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Category
        [HttpGet("Show")]
        public ActionResult<IEnumerable<CategoryModel>> Show()
        {
            var cat = _dbContext.Categories.Where(x => !x.isDeleted).AsNoTracking().ToList();

            string[] par = new string[cat.Count + 1];
            if (cat != null)
            {
                foreach (var c in cat)
                {
                    if (c.Parent == 0)
                    {
                        par[c.CategoryId] = "No Parent";
                    }
                    else
                    {
                        string i = (from d in cat
                                    where d.CategoryId == c.Parent
                                    select d.Name).First();
                        par[c.CategoryId] = i;
                    }
                }

                
                return Ok(cat);
            }

            return NotFound();
        }

        // POST: api/Category/Create
        [HttpPost("Create")]
        public IActionResult Create(CategoryModel cat)
        {
            long userId = Convert.ToInt64(Request.Cookies["CurrentUser"]);

            if (ModelState.IsValid)
            {
                cat.isDeleted = false;
                cat.CreatedDate = DateTime.Now;
                cat.CreatedBy = userId;
                cat.LastModifiedBy = userId;
                cat.LastModifiedDate = DateTime.Now;

                _dbContext.Categories.Add(cat);
                _dbContext.SaveChanges();

                return CreatedAtAction(nameof(Get), new { id = cat.CategoryId }, cat);
            }

            return BadRequest();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public ActionResult<CategoryModel> Get(int id)
        {
            var category = _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefault(x => x.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // Other action methods...
    }
}
