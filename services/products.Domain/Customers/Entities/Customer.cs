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
        List<EnderecoEntrega> enderecoEntrega)
    {
        Codigo_cliente_integracao = cnpj_cpf;
        Email = email;
        if (string.IsNullOrEmpty(email))
            throw new CustomException("E-mail não pode ser vazio");
        Razao_social = razao_social;
        if (string.IsNullOrEmpty(razao_social))
            throw new CustomException("Nome não pode ser vazio");
        Nome_fantasia = nome_fantasia;
        if (string.IsNullOrEmpty(nome_fantasia))
            throw new CustomException("Nome fantasia não pode ser vazio");
        Cnpj_cpf = cnpj_cpf;
        if (string.IsNullOrEmpty(cnpj_cpf))
            throw new CustomException("CNPJ ou CPF não pode ser vazio");
        Contato = contato;
        if (string.IsNullOrEmpty(contato))
            throw new CustomException("Nome para contato não pode ser vazio");
        Telefone1_ddd = telefone1_ddd;
        if (string.IsNullOrEmpty(telefone1_ddd))
            throw new CustomException("DDD não pode ser vazio");
        Telefone1_numero = telefone1_numero;
        if (string.IsNullOrEmpty(telefone1_numero))
            throw new CustomException("Telefone não pode ser vazio");
        Endereco = endereco;
        if (string.IsNullOrEmpty(endereco))
            throw new CustomException("Rua não pode ser vazio");
        Endereco_numero = endereco_numero;
        if (string.IsNullOrEmpty(endereco_numero))
            throw new CustomException("Número do endereço não pode ser vazio");
        Bairro = bairro;
        if (string.IsNullOrEmpty(bairro))
            throw new CustomException("Bairro não pode ser vazio");
        Complemento = complemento;
        if (string.IsNullOrEmpty(complemento))
            throw new CustomException("Complemento não pode ser vazio");
        Estado = estado;
        if (string.IsNullOrEmpty(estado))
            throw new CustomException("Estado não pode ser vazio");
        Cidade = cidade;
        if (string.IsNullOrEmpty(cidade))
            throw new CustomException("Cidade não pode ser vazio");
        Cep = cep;
        if (string.IsNullOrEmpty(cep))
            throw new CustomException("CEP não pode ser vazio");
        Contribuinte = contribuinte;
        if (string.IsNullOrEmpty(contribuinte))
        {
            throw new CustomException("Contribuinte não pode ser vazio");
        }
        else
        {
            contribuinte.ToUpper();
        }
        Observacao = observacao;
        Pessoa_fisica = pessoa_fisica;
        if (string.IsNullOrEmpty(pessoa_fisica))
            throw new CustomException("Pessoa física não pode ser vazio");
        EnderecoEntrega = enderecoEntrega;

    }
    protected Customer() { }

    public string Codigo_cliente_integracao { get; private set; }
    public double Codigo_cliente_omie { get; set; }
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
    public List<EnderecoEntrega> EnderecoEntrega { get; private set; }
    public void UpdateCustomer(
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
        List<EnderecoEntrega> enderecoEntrega
    )
    {
        this.Email = email;
        this.Razao_social = razao_social;
        this.Nome_fantasia = nome_fantasia;
        this.Cnpj_cpf = cnpj_cpf;
        this.Contato = contato;
        this.Telefone1_ddd = telefone1_ddd;
        this.Telefone1_numero = telefone1_numero;
        this.Endereco = endereco;
        this.Endereco_numero = endereco_numero;
        this.Bairro = bairro;
        this.Complemento = complemento;
        this.Estado = estado;
        this.Cidade = cidade;
        this.Cep = cep;
        this.Contribuinte = contribuinte;
        this.Observacao = observacao;
        this.Pessoa_fisica = pessoa_fisica;
        this.EnderecoEntrega = enderecoEntrega;
    }
    public void UpdateClienteOmieId(double data)
    {
        if (data is 0)
            throw new CustomException("Codigo cliente omie deve ser maior que zero");
        Codigo_cliente_omie = data;
    }
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
    protected EnderecoEntrega() { }
    public string EntEndereco { get; private set; }
    public string EntNumero { get; private set; }
    public string EntComplemento { get; private set; }
    public string EntBairro { get; private set; }
    public string EntCEP { get; private set; }
    public string EntEstado { get; private set; }
    public string EntCidade { get; private set; }
    public void UpdateAddress(string entEndereco, string entNumero, string entComplemento, string entBairro, string entCEP, string entEstado, string entCidade)
    {
        this.EntEndereco = entEndereco;
        this.EntNumero = entNumero;
        this.EntComplemento = entComplemento;
        this.EntBairro = entBairro;
        this.EntCEP = entCEP;
        this.EntEstado = entEstado;
        this.EntCidade = entCidade;
    }
}

