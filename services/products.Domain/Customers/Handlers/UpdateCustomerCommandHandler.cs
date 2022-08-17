using MediatR;
using Microsoft.Extensions.Logging;
using product.Domain.Omie.OmieCustomers.Requests;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Entities;
using products.Domain.Customers.Interfaces;
using products.Domain.Shared;

namespace products.Domain.Customers.Handlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, NotificationResult>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;
    private readonly ICustomerRepository _repository;

    public UpdateCustomerCommandHandler(ICustomerRepository repository, IMediator mediator, ILogger<UpdateCustomerCommandHandler> logger)
    {
        _repository = repository;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<NotificationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(@"
        **********Process to update a customer have been initialized**********
        ");
        var validation = new UpdateCustomerValidator();
        var validated = validation.Validate(request);
        if (!validated.IsValid)
        {
            var message = "**********Process to update a customer has failed due to validation errors**********";
            _logger.LogInformation($"{message}, **********Errors: {string.Join(",", validated.Errors.Select(e => e.ErrorMessage))}**********");
            var errors = new NotificationResult(message, false, validated.Errors);
            return errors;
        }

        var customer = _repository.GetById(request.Id);
        if (customer == null) return new NotificationResult("Invalid customer", false);


        var addressesToUpdate = request.EnderecoEntrega.Select(x =>
           new AddressToUpdateOmie(
               x.EntEndereco,
               x.EntNumero,
               x.EntComplemento,
               x.EntBairro,
               x.EntCEP,
               x.EntEstado,
               x.EntCidade
               )).ToList();

        OmieUpdateCustomerRequest omieUpdateCustomer = new(
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
            pessoa_fisica: request.Pessoa_fisica,
            enderecoEntrega: addressesToUpdate
        );
        _logger.LogInformation(@"
            **********Updating customer to Omie ERP**********");
        var omieResponse = await _mediator.Send(omieUpdateCustomer);
        if (!omieResponse.Success)
        {
            _logger.LogError(@"
            **********Update customer process at Omie has failed. Error: {0}**********", new { omieResponse.Data });
            return new("An error occured:", false, new { omieResponse.Data });
        }
        _logger.LogInformation(@"
            **********Customer has been updated to Omie**********");


        var addressToUpdate = customer.EnderecoEntrega
        .Where(a => request.EnderecoEntrega.Select(b => b.Id)
        .Contains(a.Id))
        .Where(c => c.Id != 0)
        .ToList();
        addressToUpdate.ForEach((address) =>
        {
            var addressUpdated = request.EnderecoEntrega.FirstOrDefault(a => a.Id == address.Id);
            address.UpdateAddress(
                addressUpdated.EntEndereco,
                addressUpdated.EntNumero,
                addressUpdated.EntComplemento,
                addressUpdated.EntBairro,
                addressUpdated.EntCEP,
                addressUpdated.EntEstado,
                addressUpdated.EntCidade
            );
            customer.UpdateAddress(address);
        });

        var addressToRemove = customer.EnderecoEntrega
            .Where(n => !request.EnderecoEntrega.Any(a => a.Id == n.Id))
            .Where(b => b.Id != 0)
            .ToList();
        addressToRemove.ForEach((address) => { customer.RemoveAddress(address); });

        var newAddressToAdd = request.EnderecoEntrega
                        .Where(a => a.Id == 0)
                        .Select(n => new EnderecoEntrega(
                        n.EntEndereco,
                        n.EntNumero,
                        n.EntComplemento,
                        n.EntBairro,
                        n.EntCEP,
                        n.EntEstado,
                        n.EntCidade)
                        ).ToList();
        newAddressToAdd.ForEach((address) => { customer.AddAddress(address); });

        customer.UpdateCustomer(
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
            customer.EnderecoEntrega
            );

        await _repository.UpdateAsync(customer);
        _logger.LogInformation("**********Customer updated to local database**********");
        _logger.LogInformation(@"
            **********Process completed**********");
        return new NotificationResult("Customer updated", true, new { omieResponse.Data });
    }
}
