namespace BarberBoss.Communication.Response;

/* ErrorMessages receberá uma lista de erros tratados vindo do ExceptionFilter ou apenas um erro que será "Erro Desconhecido"
 */

public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; }
    public ResponseErrorJson(string errorMessage) { ErrorMessages = [errorMessage]; }
    public ResponseErrorJson(List<string> errorMessage) { ErrorMessages = errorMessage; }
}