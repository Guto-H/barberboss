using BarberBoss.Communication.Enum;
using BarberBoss.Communication.Request;
using BarberBoss.Exception;
using FluentValidation;

namespace BarberBoss.Application.UseCases.Validator;

/*Validação de dados vindo da request utilizando pacote NuGet FluentValidation
 * Caso as condições sejam falsas irá devolver mensagens de erros personalizadas vindo do ResourceErrorMessage
 */
public class RegisterReceiptValidator : AbstractValidator<RequestReceiptJson>
{
    public RegisterReceiptValidator()
    {
        RuleFor(receipt => receipt.Title).NotEmpty().WithMessage(ResourceErrorMessage.TITLE_REQUIRED);
        RuleFor(receipt => receipt.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage(ResourceErrorMessage.RECEIPT_CANNOT_BE_FOR_THE_FUTURE);
        RuleFor(receipt => receipt.Amount).GreaterThan(0).WithMessage(ResourceErrorMessage.AMOUNT_MUST_BE_THAN_ZERO);
        RuleFor(receipt => receipt.ReceiptType).IsInEnum().WithMessage(ResourceErrorMessage.RECEIPT_TYPE_IS_NOT_VALID);
    }
}
