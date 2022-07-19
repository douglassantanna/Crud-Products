using MediatR;
using Microsoft.Extensions.Logging;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Entities;
using products.Domain.Customers.Events;
using products.Domain.Customers.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Customers.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, NotificationResult>
{
    private readonly IMediator _mediator;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;

    private readonly ICustomerRepository _repository;

    public CreateCustomerCommandHandler(ICustomerRepository repository, IMediator mediator, ILogger<CreateCustomerCommandHandler> logger)
    {
        _repository = repository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<NotificationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("**********Process to create a customer have been initialized**********");
        var validation = new CreateCustomerValidator();
        var validated = validation.Validate(request);
        if (!validated.IsValid)
        {
            var message = "**********Process to create a customer has failed due to validation errors**********";
            _logger.LogInformation($"{message}, **********Errors: {string.Join(",", validated.Errors.Select(e => e.ErrorMessage))}**********");
            var errors = new NotificationResult(message, false, validated.Errors);
            await _mediator.Publish(new CustomerResult() { Result = errors });
            return errors;
        }

        var address = new EnderecoEntrega(
            request.EnderecoEntrega.EntEndereco,
            request.EnderecoEntrega.EntNumero,
            request.EnderecoEntrega.EntComplemento,
            request.EnderecoEntrega.EntBairro,
            request.EnderecoEntrega.EntCEP,
            request.EnderecoEntrega.EntEstado,
            request.EnderecoEntrega.EntCidade
        );

        _logger.LogInformation("**********Creating customer**********");
        var customer = new Customer(
            request.Email,
            request.Razao_social,
            request.Nome_fantasia,
            request.Cnpj_cpf,
            request.Contato,
            request.Telefone1_ddd,
            request.Telefone1_numero,
            request.Endereco,
            request.Endereco_numero,
            request.Bairro,
            request.Complemento,
            request.Estado,
            request.Cidade,
            request.Cep,
            request.Contribuinte,
            request.Observacao,
            request.Pessoa_fisica,
            address
            );

        await _repository.CreateAsync(customer);
        await _mediator.Publish(new CustomerResult() { Result = new("**********Customer has been created**********") });
        _logger.LogInformation("**********Customer created**********");
        return new NotificationResult("**********Cliente criado**********");
    }
}
