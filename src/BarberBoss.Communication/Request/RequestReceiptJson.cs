using BarberBoss.Communication.Enum;

namespace BarberBoss.Communication.Request;

// Dados necessarios
public class RequestReceiptJson
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public ReceiptType ReceiptType { get; set; }
}
