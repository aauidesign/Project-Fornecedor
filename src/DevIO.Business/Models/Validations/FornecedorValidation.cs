using DevIO.Business.Models.Validations.Documentos;
using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser preenchido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");


            When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            {
                RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
                .WithMessage("O Campo CPF precisa ter {ComparisonValue} caracteres e foi preenchido {PropertyValue}.");

                RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O CPF preenchido é inválido.");

            });

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            {

                RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
                .WithMessage("O Campo CNPJ precisa ter {ComparisonValue} caracteres e foi preenchido {PropertyValue}.");

                RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true)
                .WithMessage("O CNPJ preenchido é inválido.");
            });
        }
    }
}
