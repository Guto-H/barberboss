using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Execute.RegisterReceipt;

public interface IRegisterReceiptUseCase
{
    Task<ResponseRegisteredReceiptJson> Execute(RequestReceiptJson request);
}
