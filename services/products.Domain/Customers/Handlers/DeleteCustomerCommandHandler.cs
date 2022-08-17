using MediatR;
using Microsoft.Extensions.Logging;
using product.Domain.Omie.OmieCustomers.Requests;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Customers.Handlers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, NotificationResult>
{
    private readonly IMediator _mediator;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _repository;

    public DeleteCustomerCommandHandler(ICustomerRepository repository, IMediator mediator, ILogger<DeleteCustomerCommandHandler> logger)
    {
        _repository = repository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<NotificationResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        **********Process to delete a customer have been initialized**********");

        var validation = new DeleteCustomerValidator();
        var validated = validation.Validate(request);
        if (!validated.IsValid)
        {
            var message = "**********Process to update a customer has failed due to validation errors**********";
            _logger.LogInformation($"{message}, **********Errors: {string.Join(",", validated.Errors.Select(e => e.ErrorMessage))}**********");
            var errors = new NotificationResult(message, false, validated.Errors);
            return errors;
        }

        var customer = _repository.GetById(request.Id);
        if (customer is null) return new NotificationResult("Customer is null.", false);

        OmieDeleteCustomerRequest customerRequest = new(
            codigo_cliente_omie: customer.Codigo_cliente_omie,
            codigo_cliente_integracao: customer.Codigo_cliente_integracao
        );
        _logger.LogInformation(@"
            **********Deleting customer from Omie ERP**********");
        var omieResponse = await _mediator.Send(customerRequest);
        if (!omieResponse.Success)
        {
            _logger.LogError(@"
            **********Delete customer process at Omie has failed. Error: {0}**********", new { omieResponse.Data });
            return new("An error occured:", false, new { omieResponse.Data });
        }
        _logger.LogInformation(@"
            **********Customer deleted from Omie ERP**********");

        await _repository.DeleteAsync(customer);
        _logger.LogInformation(@"
            **********Customer deleted from local database**********");
        _logger.LogInformation(@"
            **********Process completed**********");
        return new NotificationResult("Customer deleted.", true, new { omieResponse.Data });
    }
}
