using System.Net;

namespace BarberBoss.Exception.ExceptionBase;

/* Tratamento de erros coletados pela validação
 * Metodos vindo da classe BarberBossException via herança 
 * StatusCode recebe int com o codigo de erro
 * 
 * Construtor receberá uma lista de erros sempre que a classe for chamada
 * essa lista de erros será passada para a variavel privada Erroros, que por sua vez
 * será retornada no metodo ErrorsMessage que se encontra dentro do BarberBossException
 */
public class ErrorOnValidationException : BarberBossException
{
    private List<string> Errors { get; set; }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        Errors = errorMessages;
    }

    public override List<string> ErrorsMessage()
    {
        return Errors;
    }
}
