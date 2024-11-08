using System.Net;

namespace BarberBoss.Exception.ExceptionBase;

/* Passando a mensagem de erro para a classe base
 * Implementando metodo abstract e reescrevendo passado o int do status code do Not Found
 * Reescrevendo metodo ErrorsMessage para retornar apenas a mensagem
*/

public class NotFoundException : BarberBossException
{
    public NotFoundException(string ErrorMessageNotFound) : base(ErrorMessageNotFound){ }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> ErrorsMessage()
    {
        return [Message];
    }
}
