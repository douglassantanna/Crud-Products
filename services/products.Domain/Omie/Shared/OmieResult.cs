namespace products.Domain.Omie.Shared;

public class OmieResult
{
    public OmieResult(double codigo_cliente_omie, string codigo_cliente_integracao, string codigo_status, string descricao_status)
    {
        this.codigo_cliente_omie = codigo_cliente_omie;
        this.codigo_cliente_integracao = codigo_cliente_integracao;
        this.codigo_status = codigo_status;
        this.descricao_status = descricao_status;
    }

    public double codigo_cliente_omie { get; private set; }
    public string codigo_cliente_integracao { get; private set; }
    public string codigo_status { get; private set; }
    public string descricao_status { get; private set; }

}