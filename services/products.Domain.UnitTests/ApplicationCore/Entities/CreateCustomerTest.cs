using products.Domain.Customers.Entities;
using Xunit.Abstractions;

namespace products.Domain.UnitTests.ApplicationCore.Entities;

public class CreateCustomerTest
{
    private readonly ITestOutputHelper _helper;
    private readonly Customer _validCustomer;

    public CreateCustomerTest(ITestOutputHelper helper)
    {
        _validCustomer = new Customer(
            email: "teste@teste.com",
            razao_social: "teste",
            nome_fantasia: "teste",
            cnpj_cpf: "40560278896",
            contato: "teste",
            telefone1_ddd: "11",
            telefone1_numero: "941012994",
            endereco: "teste",
            endereco_numero: "teste",
            bairro: "teste",
            complemento: "teste",
            estado: "sp",
            cidade: "teste",
            cep: "13219110",
            contribuinte: "s",
            observacao: "teste",
            pessoa_fisica: "s",
            new(){
                new EnderecoEntrega(
                    entEndereco: "teste",
                    entNumero: "teste",
                    entComplemento: "teste",
                    entBairro: "teste",
                    entCEP: "13219110",
                    entEstado: "sp",
                    entCidade: "teste")
                }
        );
        _helper = helper;
    }

    [Fact]
    public void Create_customer_with_success()
    {
        var newCustomer = new Customer(
            email: "teste@teste.com",
            razao_social: "teste",
            nome_fantasia: "teste",
            cnpj_cpf: "40560278896",
            contato: "teste",
            telefone1_ddd: "11",
            telefone1_numero: "941012994",
            endereco: "teste",
            endereco_numero: "teste",
            bairro: "teste",
            complemento: "teste",
            estado: "sp",
            cidade: "teste",
            cep: "13219110",
            contribuinte: "s",
            observacao: "teste",
            pessoa_fisica: "s",
            new(){
                new EnderecoEntrega(
                    entEndereco: "teste",
                    entNumero: "teste",
                    entComplemento: "teste",
                    entBairro: "teste",
                    entCEP: "13219110",
                    entEstado: "sp",
                    entCidade: "teste")
                }
        );
        _helper.WriteLine(newCustomer.ToString());
        Assert.NotNull(newCustomer);
    }

    // [Theory]
    // [InlineData("Douglas", "")]
    // public void WhenCreatingCustomer_WithNoFullNameOrNoEmail_ThrowExcepiton(string fullName, string email)
    // {   
    //     var exception = Assert.Throws<Exception>(() => new Customer(fullName, email, DateTime.Now.AddYears(-19)));
    // }
}