using products.Domain.Shared;

namespace products.Domain.Customers.Entities;

public class Customer : Entity
{
    public Customer(
        string email,
        string razao_social,
        string nome_fantasia,
        string cnpj_cpf,
        string contato,
        string telefone1_ddd,
        string telefone1_numero,
        string endereco,
        string endereco_numero,
        string bairro,
        string complemento,
        string estado,
        string cidade,
        string cep,
        string contribuinte,
        string observacao,
        string pessoa_fisica,
        List<EnderecoEntrega> enderecosEntrega)
    {
        Codigo_cliente_integracao = cnpj_cpf;
        Email = email;
        if (string.IsNullOrEmpty(email)) throw new CustomException("E-mail não pode ser vazio");
        Razao_social = razao_social;
        if (string.IsNullOrEmpty(razao_social)) throw new CustomException("Nome não pode ser vazio");
        Nome_fantasia = nome_fantasia;
        Cnpj_cpf = cnpj_cpf;
        Contato = contato;
        Telefone1_ddd = telefone1_ddd;
        Telefone1_numero = telefone1_numero;
        Endereco = endereco;
        Endereco_numero = endereco_numero;
        Bairro = bairro;
        Complemento = complemento;
        Estado = estado;
        Cidade = cidade;
        Cep = cep;
        Contribuinte = contribuinte;
        Observacao = observacao;
        Pessoa_fisica = pessoa_fisica;
        EnderecosEntrega = enderecosEntrega;

    }
    protected Customer() { }

    public string Codigo_cliente_integracao { get; private set; }
    public string Email { get; private set; }
    public string Razao_social { get; private set; }
    public string Nome_fantasia { get; private set; }
    public string Cnpj_cpf { get; private set; }
    public string Contato { get; private set; }
    public string Telefone1_ddd { get; private set; }
    public string Telefone1_numero { get; private set; }
    public string Endereco { get; private set; }
    public string Endereco_numero { get; private set; }
    public string Bairro { get; private set; }
    public string Complemento { get; private set; }
    public string Estado { get; private set; }
    public string Cidade { get; private set; }
    public string Cep { get; private set; }
    public string Contribuinte { get; private set; }
    public string Observacao { get; private set; }
    public string Pessoa_fisica { get; private set; }
    public List<EnderecoEntrega> EnderecosEntrega { get; private set; }
    public void UpdateRazao_social(string razao_social) => Razao_social = razao_social;
    public void UpdateEmail(string email) => Email = email;
}
public class EnderecoEntrega : Entity
{
    public EnderecoEntrega(string entEndereco, string entNumero, string entComplemento, string entBairro, string entCEP, string entEstado, string entCidade)
    {
        EntEndereco = entEndereco;
        EntNumero = entNumero;
        EntComplemento = entComplemento;
        EntBairro = entBairro;
        EntCEP = entCEP;
        EntEstado = entEstado;
        EntCidade = entCidade;
    }

    public string EntEndereco { get; private set; }
    public string EntNumero { get; private set; }
    public string EntComplemento { get; private set; }
    public string EntBairro { get; private set; }
    public string EntCEP { get; private set; }
    public string EntEstado { get; private set; }
    public string EntCidade { get; private set; }
}

