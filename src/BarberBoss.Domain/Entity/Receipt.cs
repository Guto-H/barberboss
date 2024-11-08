using BarberBoss.Domain.Enum;

namespace BarberBoss.Domain.Entity;

/* Clonagem da request para comunicação entre Aplicação -> Infra
 * Obs: ID será preenchido pelo banco de dados
*/
public class Receipt
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public ReceiptType ReceiptType { get; set; }
}

