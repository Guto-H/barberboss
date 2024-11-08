using BarberBoss.Communication.Enum;

namespace BarberBoss.Communication.Response;

// Tipo de resposta para GetById
public class ResponseReceiptGetByIdJson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public ReceiptType ReceiptType { get; set; }
}
