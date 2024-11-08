using AutoMapper;
using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Entity;

namespace BarberBoss.Application.AutoMapper;

/* Mapeamento de objetos realizado pelo pacote NuGet AutoMapper
 */
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestReceiptJson, Receipt>();
    }

    private void EntityToResponse()
    {
        CreateMap<Receipt, ResponseRegisteredReceiptJson>();
        CreateMap<Receipt, ResponseShortReceiptJson>();
        CreateMap<Receipt, ResponseReceiptGetByIdJson>();
    }
}
