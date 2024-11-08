using BarberBoss.Communication.Response;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberBoss.API.Filter;

/* Criando classe para filtrar as excessões coletadas pelo Exception Filter.
 * Se for um erro dentro da estrutura BarberBoss será tratado com Exceptions personalizadas.
 * Caso seja qualquer outro tipo de erro será do tipo Unknow Error.
 * Erros enviados a classe ResponseErrorJson para que a API retorne o resultado
*/
public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is BarberBossException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var barberBossException = (BarberBossException)context.Exception;
        var errorResponse = new ResponseErrorJson(barberBossException.ErrorsMessage());

        context.HttpContext.Response.StatusCode = barberBossException.StatusCode;
        context.Result = new ObjectResult(errorResponse);

    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        var errorReponse = new ResponseErrorJson(ResourceErrorMessage.UNKNOW_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorReponse);
    }
}
