using System.Linq.Expressions;
using products.Domain.Customers.Entities;

namespace products.Domain.Customers.DTOs;

public class ViewCustomer
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Razao_social { get; set; }
    public string? Nome_fantasia { get; set; }
    public string? Cnpj_cpf { get; set; }
    public string? Contato { get; set; }
    public string? Telefone1_ddd { get; set; }
    public string? Telefone1_numero { get; set; }
    public string? Endereco { get; set; }
    public string? Endereco_numero { get; set; }
    public string? Bairro { get; set; }
    public string? Complemento { get; set; }
    public string? Estado { get; set; }
    public string? Cidade { get; set; }
    public string? Cep { get; set; }
    public string? Contribuinte { get; set; }
    public string? Observacao { get; set; }
    public string? Pessoa_fisica { get; set; }
    public EnderecoEntrega? EnderecoEntrega { get; set; }
}

public static class ViewCustomerExtension
{
    public static Expression<Func<Customer, ViewCustomer>> ToView() => x => new ViewCustomer
    {
        Id = x.Id,
        Razao_social = x.Razao_social,
        Nome_fantasia = x.Nome_fantasia,
        Cnpj_cpf = x.Cnpj_cpf,
        Contato = x.Contato,
        Telefone1_ddd = x.Telefone1_ddd,
        Telefone1_numero = x.Telefone1_numero,
        Endereco = x.Endereco,
        Endereco_numero = x.Endereco_numero,
        Bairro = x.Bairro,
        Complemento = x.Complemento,
        Estado = x.Estado,
        Cidade = x.Cidade,
        Cep = x.Cep,
        Contribuinte = x.Contribuinte,
        Observacao = x.Observacao,
        Pessoa_fisica = x.Pessoa_fisica
    };
}

