using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using products.Domain.Infra.Context;
using products.Domain.Itens.Commands;
using products.Domain.Itens.DTOs;
using products.Domain.Itens.Contracts;
using products.Domain.Shared;

namespace products.Domain.Api.Controllers
{
    [ApiController]
    [Route("item")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMediator _mediator;
        private readonly AppDbContext _db;

        public ItemController(IItemRepository itemRepository, IMediator mediator, AppDbContext db)
        {
            _itemRepository = itemRepository;
            _mediator = mediator;
            _db = db;
        }
        [HttpGet]
        public ActionResult<Pagination<ViewItem>> GetAll(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sort = "desc"
        )
        {
            var query = _db.Itens
            .AsSingleQuery()
            .Select(ViewItemExtension.ToView());
            if (sort == "desc")
                query = query.OrderByDescending(x => x.Id);
            else
                query = query.OrderBy(x => x.Id);

            return new Pagination<ViewItem>(query, pageIndex, pageSize);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_db.Itens.FirstOrDefault(x => x.Id == id));
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemCommand item)
        {
            var result = await _mediator.Send(item);
            if (!result.Success) return BadRequest(result);
            return Created("", result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateItemCommand item)
        {
            if (id != item.Id) return BadRequest(new NotificationResult("ID do item invalido.", false, new { itemId = id, item }));
            var result = await _mediator.Send(item);
            if (!result.Success) return BadRequest(result);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var req = new DeleteItemCommand(id);
            var result = await _mediator.Send(req);
            if (!result.Success) return BadRequest(result);
            return NoContent();
        }
    }
}