namespace product.Domain.Omie.OmieCustomers.Results;

public record OmieGetCustomerResult(
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
public record OmieCreateCustomerResult(double codigo_cliente_omie, string codigo_cliente_integracao, string codigo_status, string descricao_status);
