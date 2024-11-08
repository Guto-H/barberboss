using BarberBoss.Communication.Response;

namespace BarberBoss.Application.UseCases.Execute.GetByIdReceipt;
public interface IGetByIdReceiptUseCase
{
    Task<ResponseReceiptGetByIdJson> Execute(long id);

}
