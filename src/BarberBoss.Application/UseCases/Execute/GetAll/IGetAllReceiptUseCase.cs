using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Execute.GetAllReceipt;
public interface IGetAllReceiptUseCase
{
    Task<ResponseReceiptJson> Execute();
}
