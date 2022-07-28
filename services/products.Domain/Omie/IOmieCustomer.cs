namespace products.Domain.Omie;
public interface IOmieCustomer
{
    Task<OmieCustomerResult> GetCustomer(OmieGetCustomer request);
}
public record OmieCustomerResult(
        string codigo_cliente_integracao,
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
        string pessoa_fisica
);
public class OmieGetCustomer
{
    public string? codigo_cliente_omie { get; set; }
    public string? codigo_cliente_integracao { get; set; }
}
