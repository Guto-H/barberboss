using BarberBoss.Application.UseCases.Validator;
using BarberBoss.Communication.Enum;
using BarberBoss.Exception;
using CommonTestUtilities.Request;
using FluentAssertions;

namespace Validators.Tests.Receipt.Register;

//Realizando testes unitarios para verificar o funcionamento da API
public class RegisterReceiptValidatorTest
{
    [Fact]
    public void Success()
    {
        // Arrange -- Instancias necessarias para o teste, chamando função build para preenchimento da request de modo aleatorio
        var validatorTest = new RegisterReceiptValidator();
        var requestTest = RequestRegisterReceiptJsonBuilder.Build();

        // Act -- Ação que deve ser realizada
        var result = validatorTest.Validate(requestTest);

        // Assert - Resutado esperado usando pacote NuGet FluentAssertions
        result.IsValid.Should().BeTrue();

    }

    // Forçando erros em diferentes situações
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Title_Failure(string title)
    {

        var validatorTest = new RegisterReceiptValidator();
        var requestTest = RequestRegisterReceiptJsonBuilder.Build();
        requestTest.Title = title;

        var result = validatorTest.Validate(requestTest);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.TITLE_REQUIRED));
    }

    [Fact]
    public void Date_Failure()
    {
        var validatorTest = new RegisterReceiptValidator();
        var requestTest = RequestRegisterReceiptJsonBuilder.Build();
        requestTest.Date = DateTime.UtcNow.AddDays(1);

        var result = validatorTest.Validate(requestTest);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.RECEIPT_CANNOT_BE_FOR_THE_FUTURE));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Amount_Failure(decimal amount)
    {
        var validatorTest = new RegisterReceiptValidator();
        var requestTest = RequestRegisterReceiptJsonBuilder.Build();
        requestTest.Amount = amount;

        var result = validatorTest.Validate(requestTest);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.AMOUNT_MUST_BE_THAN_ZERO));
    }

    [Fact]
    public void ReceiptType_Failure()
    {
        var validatorTest = new RegisterReceiptValidator();
        var requestTest = RequestRegisterReceiptJsonBuilder.Build();
        requestTest.ReceiptType = (ReceiptType)20;

        var result = validatorTest.Validate(requestTest);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessage.RECEIPT_TYPE_IS_NOT_VALID));
    }

}