using MediatR;
using Microsoft.Extensions.Logging;
using product.Domain.Omie.OmieCustomers.Requests;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Entities;
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
        _logger.LogInformation(@"
        **********Process to create a customer have been initialized**********
        ");

        var validation = new CreateCustomerValidator();
        var validated = validation.Validate(request);
        if (!validated.IsValid)
        {
            var message = "**********Process to create a customer has failed due to validation errors**********";
            _logger.LogInformation($"{message}, **********Errors: {string.Join(",", validated.Errors.Select(e => e.ErrorMessage))}**********");
            var errors = new NotificationResult(message, false, validated.Errors);
            return errors;
        }

        var addresses = request.EnderecoEntrega.Select(x =>
            new EnderecoEntrega(
                x.entEndereco,
                x.entNumero,
                x.entComplemento,
                x.entBairro,
                x.entCEP,
                x.entEstado,
                x.entCidade)).ToList();

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
            addresses
            );

        _logger.LogInformation(@"
            **********Creating customer**********");
        await _repository.CreateAsync(customer);
        _logger.LogInformation("**********Customer has been created in local database**********");

        OmieCreateCustomerRequest omieCustomer = new(
            codigo_cliente_integracao: request.Cnpj_cpf,
            email: request.Email,
            razao_social: request.Razao_social,
            cnpj_cpf: request.Cnpj_cpf,
            contato: request.Contato,
            telefone1_numero: request.Telefone1_numero,
            endereco: request.Endereco,
            endereco_numero: request.Endereco_numero,
            bairro: request.Bairro,
            complemento: request.Complemento,
            estado: request.Estado,
            cidade: request.Cidade,
            cep: request.Cep,
            contribuinte: request.Contribuinte,
            observacao: request.Observacao,
            pessoa_fisica: request.Pessoa_fisica
        );
        _logger.LogInformation(@"
            **********Creating customer at Omie ERP**********");
        var omieResponse = await _mediator.Send(omieCustomer);
        if (!omieResponse.Success)
        {
            _logger.LogError(@"
            **********Create customer process at Omie has failed. Error: {0}**********", new { omieResponse.Data });
            return new("An error occured:", false, new { omieResponse.Data });
        }
        _logger.LogInformation(@"
            **********Customer has been added to Omie**********");

        _logger.LogInformation(@"
            **********Process completed**********");

        return new NotificationResult("Customer created", true, new { omieResponse.Data });
    }
}
