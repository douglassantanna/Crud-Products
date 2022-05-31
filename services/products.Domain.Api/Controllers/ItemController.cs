using Microsoft.AspNetCore.Mvc;
using products.Domain.Entities;
using products.Domain.Infra.Repositories.ItemRepo;
using products.Domain.Infra.ViewModels.Item;

namespace products.Domain.Api.Controllers
{
    [ApiController]
    [Route("item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll()
        {
            var itens = await _itemRepository.GetAllAsync();
            return itens;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewItem item)
        {
            var newItem = await _itemRepository.CreateAsync(item);
            return Created("", item);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {
            var updateItem = await _itemRepository.UpdateAsync(id, item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}