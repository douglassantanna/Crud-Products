using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using products.Domain.Infra.Context;
using products.Domain.Itens.DTOs;
using products.Domain.Orders.Commands;
using products.Domain.Orders.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Api.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IMediator _mediator;
        private readonly AppDbContext _db;

        public OrderController(IOrderRepository OrderRepository, IMediator mediator, AppDbContext db)
        {
            _OrderRepository = OrderRepository;
            _mediator = mediator;
            _db = db;
        }
        [HttpGet]
        public ActionResult<Pagination<ViewOrder>> GetAll(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sort = "desc"
        )
        {
            var query = _db.Orders
            .AsSingleQuery()
            .Select(ViewOrderExtension.ToView());
            if (sort == "desc")
                query = query.OrderByDescending(x => x.Id);
            else
                query = query.OrderBy(x => x.Id);

            return new Pagination<ViewOrder>(query, pageIndex, pageSize);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_db.Orders.FirstOrDefault(x => x.Id == id));
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand order)
        {
            var result = await _mediator.Send(order);
            if (!result.Success) return BadRequest(result);
            return Created("", result);
        }
        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, UpdateOrderCommand Order)
        // {
        //     if (id != Order.Id) return BadRequest(new NotificationResult("ID do Order invalido.", false, new { OrderId = id, Order }));
        //     var result = await _mediator.Send(Order);
        //     if (!result.Success) return BadRequest(result);
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var req = new DeleteOrderCommand(id);
        //     var result = await _mediator.Send(req);
        //     if (!result.Success) return BadRequest(result);
        //     return NoContent();
        // }
    }
}