namespace BarberBoss.Communication.Response;

// Classe para coletar todas as informações e passar para o Short Response em lista

public class ResponseReceiptJson
{
    public List<ResponseShortReceiptJson> ResponseReceipt { get; set; } = [];
}
