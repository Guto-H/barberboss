using BarberBoss.Domain.Entity;

namespace BarberBoss.Domain.Repositories.Receipts;
public interface IReceiptUpdateOnlyRepository
{
    Task<Receipt?> GetById(long id);

    void Update(Receipt receipt);
}
