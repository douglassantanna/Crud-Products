using MediatR;
using Microsoft.Extensions.Logging;
using product.Domain.Omie.OmieCustomers.Requests;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Entities;
using products.Domain.Customers.Interfaces;
using products.Domain.Omie.OmieCustomers;
using products.Domain.Shared;

namespace products.Domain.Customers.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, NotificationResult>
{
    private readonly IMediator _mediator;
    private readonly ILogger<CreateCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _repository;
    private readonly IOmieCustomer _omieServices;

    public CreateCustomerCommandHandler(ICustomerRepository repository, IMediator mediator, ILogger<CreateCustomerCommandHandler> logger, IOmieCustomer omieServices)
    {
        _repository = repository;
        _mediator = mediator;
        _logger = logger;
        _omieServices = omieServices;
    }

    public async Task<NotificationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        **********Process to create a customer have been initialized**********
        ");

        try
        {
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

            _logger.LogInformation(@"
            **********Creating customer**********");
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

            await _repository.CreateAsync(customer);
            _logger.LogInformation("**********Customer has been created in local database**********");

            _logger.LogInformation(@"
            **********Starting process to add customer to Omie ERP**********");
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
            var omieResult = await _mediator.Send(omieCustomer);
            _logger.LogInformation(@"
            **********Customer has been added to Omie**********");
        }
        catch (Exception ex)
        {
            _logger.LogError(@"
            Process could not be completed due to an error. Error: {0}", ex.Message);
        }
        finally
        {
            _logger.LogInformation(@"
            **********Customer created in local database and added to Omie ERP**********");
        }
        return new NotificationResult(@"
        **********Cliente criado**********");
    }
}
