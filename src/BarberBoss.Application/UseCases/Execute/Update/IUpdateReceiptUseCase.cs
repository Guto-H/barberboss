using BarberBoss.Communication.Request;

namespace BarberBoss.Application.UseCases.Execute.UpdateReceipt;
public interface IUpdateReceiptUseCase
{
    Task Execute(long id, RequestReceiptJson request);
}
