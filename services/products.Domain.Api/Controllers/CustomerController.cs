using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using product.Domain.Omie;
using products.Domain.Customers.Commands;
using products.Domain.Customers.DTOs;
using products.Domain.Customers.Interfaces;
using products.Domain.Infra.Context;
using products.Domain.Omie;
using products.Domain.Shared;

namespace products.Domain.Api.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _CustomerRepository;
        private readonly IMediator _mediator;
        private readonly AppDbContext _db;
        private readonly IOmieCustomer _omieCustomer;

        public CustomerController(ICustomerRepository customerRepository, IMediator mediator, AppDbContext db, IOmieCustomer omieCustomer)
        {
            _CustomerRepository = customerRepository;
            _mediator = mediator;
            _db = db;
            _omieCustomer = omieCustomer;
        }
        [HttpPost("get-customer")]
        public async Task<IActionResult> Get(OmieGetCustomerCommand customer)
        {
            var result = await _mediator.Send(customer);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<Pagination<ViewCustomer>> GetAll(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sort = "desc"
        )
        {
            var query = _db.Customers
            .AsSingleQuery()
            .Select(ViewCustomerExtension.ToView());
            if (sort == "desc")
                query = query.OrderByDescending(x => x.Id);
            else
                query = query.OrderBy(x => x.Id);

            return new Pagination<ViewCustomer>(query, pageIndex, pageSize);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(_CustomerRepository.GetCustomerWithAddress(id));
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand customer)
        {
            var result = await _mediator.Send(customer);
            if (!result.Success) return BadRequest(result);
            return Created("", result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand customer)
        {
            if (id != customer.Id) return BadRequest(new NotificationResult("ID do Customer invalido.", false, new { customerId = id, customer }));
            var result = await _mediator.Send(customer);
            if (!result.Success) return BadRequest(result);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var req = new DeleteCustomerCommand(id);
            var result = await _mediator.Send(req);
            if (!result.Success) return BadRequest(result);
            return NoContent();
        }
    }
}