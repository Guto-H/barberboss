
namespace BarberBoss.Communication.Response;

// Classe para devolver algumas informações importantes para o Get
public class ResponseShortReceiptJson
{
    public int id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
