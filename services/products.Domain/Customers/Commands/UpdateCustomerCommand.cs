using FluentValidation;
using MediatR;
using products.Domain.Shared;

namespace products.Domain.Customers.Commands;

public record UpdateCustomerCommand(
    int Id,
    string Codigo_cliente_integracao,
    string Email,
    string Razao_social,
    string Nome_fantasia,
    string Cnpj_cpf,
    string Contato,
    string Telefone1_ddd,
    string Telefone1_numero,
    string Endereco,
    string Endereco_numero,
    string Bairro,
    string Complemento,
    string Estado,
    string Cidade,
    string Cep,
    string Contribuinte,
    string Observacao,
    string Pessoa_fisica,
    List<AddressToUpdate> EnderecoEntrega
      ) : IRequest<NotificationResult>;

public record AddressToUpdate(
    int Id,
    string EntEndereco,
    string EntNumero,
    string EntComplemento,
    string EntBairro,
    string EntCEP,
    string EntEstado,
    string EntCidade
);
public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Email).EmailAddress().WithMessage("Um {PropertyName} válido deve ser fornecido.");
        RuleFor(x => x.Razao_social).NotNull().NotEmpty().WithMessage("Razão social é obrigatório.");
        RuleFor(x => x.Nome_fantasia).NotNull().NotEmpty().WithMessage("Nome fantasia é obrigatório.");
        RuleFor(x => x.Cnpj_cpf).NotNull().NotEmpty().Length(11, 14).WithMessage("Digite um CPF ou CNPJ");
        RuleFor(x => x.Contato).NotNull().NotEmpty().WithMessage("Digite um nome para contato");
        RuleFor(x => x.Telefone1_ddd).NotNull().NotEmpty().Length(2, 2).WithMessage("Digite um DDD");
        RuleFor(x => x.Telefone1_numero).NotNull().NotEmpty().Length(8, 9).WithMessage("Digite um número de telefone válido");
        RuleFor(x => x.Endereco).NotNull().NotEmpty().Length(2, 100).WithMessage("Digite uma rua string entre 2 a 100 caracteres");
        RuleFor(x => x.Endereco_numero).NotNull().NotEmpty().Length(1, 10).WithMessage("Digite o número da rua do endereço");
        RuleFor(x => x.Bairro).NotNull().NotEmpty().Length(2, 50).WithMessage("Digite o nome do bairro");
        RuleFor(x => x.Estado).NotNull().NotEmpty().Length(2, 50).WithMessage("Digite o nome do estado");
        RuleFor(x => x.Cidade).NotNull().NotEmpty().Length(2, 50).WithMessage("Digite o nome do cidade");
        RuleFor(x => x.Cep).NotNull().NotEmpty().Length(8, 8).WithMessage("Digite um CEP");
        RuleFor(x => x.Contribuinte).NotNull().NotEmpty().Length(1, 1).WithMessage("Campo Contribuinte obrigatório. Escolha 'S' para sim e 'N' para não");
        RuleFor(x => x.Pessoa_fisica).NotNull().NotEmpty().Length(1, 1).WithMessage("Campo Pessoa Física obrigatório. Escolha 'S' para sim e 'N' para não");
        RuleFor(e => e.EnderecoEntrega).NotNull().NotEmpty().WithMessage("Necessário informar um endereço para entrega");
    }
}
