using Microsoft.AspNetCore.Mvc;
using products.Domain.Entities;
using products.Domain.Infra.Repositories.ItemRepo;

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
        public async Task<IActionResult> GetAll()
        {
            var itens = await _itemRepository.GetAllAsync();
            return Ok(itens);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            var newItem = await _itemRepository.CreateAsync(item);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Item item)
        {
            var updateItem = await _itemRepository.UpdateAsync(id, item);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}