using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Interfaces;
using products.Domain.Omie.Events.Customers;
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
        **********Process to delete a customer have been initialized**********
        ");
        try
        {
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
            if (customer == null) return new NotificationResult("Cliente invalido", false);

            await _repository.DeleteAsync(customer);
            _logger.LogInformation(@"
            **********Customer deleted from local database**********");

            await _mediator.Publish(new CustomerToDelete(
                customer.Codigo_cliente_omie,
                customer.Codigo_cliente_integracao
            ));
            _logger.LogInformation(@"
            **********Customer deleted from Omie ERP**********");
        }
        catch (Exception ex)
        {
            _logger.LogError(@"
            Process could not be completed due to an error. Error: {0}", ex.Message);
        }
        finally
        {
            _logger.LogInformation(@"
            **********Customer deleted from local database and from Omie ERP**********");
        }

        return new NotificationResult("Cliente excluido");
    }
}
