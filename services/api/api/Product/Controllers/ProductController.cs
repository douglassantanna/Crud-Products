using System.Collections.Generic;
using System.Threading.Tasks;
using api.Product.Infra;
using api.Product.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> Get()
        {
            var product = await _dataContext.Items.ToListAsync();
            return product;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Item>>> GetItemById(int id)
        {
            var item = await _dataContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(item);
        }
        [HttpPost]
        public ActionResult Create(NewItem newItem)
        {
            var item = new Item(newItem.Name, newItem.Price);
            _dataContext.Items.Add(item);
            _dataContext.SaveChanges();
            return Created("", item);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, ViewItem viewItem)
        {
            var item = await _dataContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            if(item == null) return NotFound();

            item.ChangeName(viewItem.Name);
            item.ChangePrice(viewItem.Price);

            _dataContext.Entry(item).State = EntityState.Modified;
            _dataContext.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _dataContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            if(item == null) return NotFound();

            _dataContext.Items.Remove(item);
            _dataContext.SaveChanges();
            return NoContent();
        }
    }
    public record NewItem(string Name, double Price);
    public record ViewItem(int Id, string Name, double Price);
}
