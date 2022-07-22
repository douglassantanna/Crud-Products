using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using products.Domain.Customers.Commands;
using products.Domain.Customers.Entities;
using products.Domain.Customers.Handlers;
using products.Domain.Customers.Interfaces;
using products.Domain.Infra.Context;
using products.Domain.Infra.Repositories.CustomerRepo;
using Xunit.Abstractions;

namespace products.Domain.IntegrationTests.Customer_Test;

public class CriarNovoCliente_Teste
{
    private readonly AppDbContext _db;
    Mock<IMediator> _mediator = new Mock<IMediator>();
    Mock<ILogger<CreateCustomerCommandHandler>> _logger = new Mock<ILogger<CreateCustomerCommandHandler>>();
    private readonly ICustomerRepository _customerRepository;
    private readonly ITestOutputHelper _helper;
    NewShippingAddress _validAddress;
    List<NewShippingAddress> _validAddresses;

    public CriarNovoCliente_Teste(ITestOutputHelper helper, ICustomerRepository customerRepository, Mock<ILogger<CreateCustomerCommandHandler>> logger)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase("dshop")
        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        .Options;
        _db = new AppDbContext(options);
        _mediator.Setup(x => x.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()));
        _helper = helper;
        _validAddress = new(
                entEndereco: "rua teste",
                entNumero: "12",
                entComplemento: "complemento teste",
                entBairro: "bairro teste",
                entCEP: "13219110",
                entEstado: "SP",
                entCidade: "Jundiaí"
        );
        _validAddresses = new List<NewShippingAddress>();
        _validAddresses.Add(_validAddress);
        _customerRepository = new CustomerRepository(_db);
        _logger = logger;
    }

    [Fact]
    public async void AoCriarUmCliente_ComDadosCorretos_DeveSerPersistido()
    {
        CreateCustomerCommand command = new(
            Email: "douglas@teste.com",
            Razao_social: "douglas teste",
            Nome_fantasia: "douglas teste",
            Cnpj_cpf: "40560278896",
            Contato: "douglas teste",
            Telefone1_ddd: "11",
            Telefone1_numero: "941012994",
            Endereco: "rua teste",
            Endereco_numero: "12",
            Bairro: "bairro teste",
            Complemento: "complemento teste",
            Estado: "SP",
            Cidade: "Jundiaí",
            Cep: "13219110",
            Contribuinte: "S",
            Observacao: "",
            Pessoa_fisica: "S",
            EnderecoEntrega: _validAddresses
        );
        CreateCustomerValidator validator = new();
        var validation = validator.Validate(command);
        Assert.True(validation.IsValid);

        CreateCustomerCommandHandler handler = new(_customerRepository, _mediator.Object, _logger.Object);
        var result = await handler.Handle(command, default(CancellationToken));
        Assert.True(result.Success);

        _mediator.Verify(x => x.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);
    }
}
