using BarberBoss.Communication.Enum;
using BarberBoss.Communication.Request;
using Bogus;

namespace CommonTestUtilities.Request;

public class RequestRegisterReceiptJsonBuilder
{   
    //Função para preenchimento de informações da request com informações aleatorias usando pacote NuGet Bogus
    public static RequestReceiptJson Build()
    {
        return new Faker<RequestReceiptJson>()
            .RuleFor(rf => rf.Title, Faker => Faker.Commerce.Product())
            .RuleFor(rf => rf.Description, Faker => Faker.Commerce.ProductName())
            .RuleFor(rf => rf.Date, Faker => Faker.Date.Past())
            .RuleFor(rf => rf.Amount, Faker => Faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(rf => rf.ReceiptType, Faker => Faker.PickRandom<ReceiptType>());
    }
}
