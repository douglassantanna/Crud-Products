using MediatR;
using Microsoft.AspNetCore.Mvc;
using products.Domain.Itens.Commands;
using products.Domain.Itens.Entities;
using products.Domain.Itens.Interfaces;

namespace products.Domain.Api.Controllers
{
    [ApiController]
    [Route("item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMediator _mediator;

        public ItemController(IItemRepository itemRepository, IMediator mediator)
        {
            _itemRepository = itemRepository;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAll()
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
        public async Task<IActionResult> Create(CreateItemCommand item)
        {
            var result = await _mediator.Send(item);
            if(!result.Success) return BadRequest(result);
            return Created("", result);
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