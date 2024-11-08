using BarberBoss.Domain.Enum;
using BarberBoss.Domain.Reports.Conversor;

namespace BarberBoss.Domain.Extension;

// Classe que está convertendo os valores Enums em valores String para os relátorios
public static class ReceiptTypeExtension
{
    public static string ReceiptTypeToString(this ReceiptType receiptType)
    {
        return receiptType switch
        {
            ReceiptType.Cash => ResourceReportConversorReceiptType.CASH,
            ReceiptType.Pix => ResourceReportConversorReceiptType.PIX,
            ReceiptType.DebitCard => ResourceReportConversorReceiptType.DEBITCARD,
            ReceiptType .CreditCard => ResourceReportConversorReceiptType.CREDITCARD,
            _ => string.Empty

        };
    }
}
