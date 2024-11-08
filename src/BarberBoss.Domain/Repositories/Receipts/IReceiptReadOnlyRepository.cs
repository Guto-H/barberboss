using BarberBoss.Domain.Entity;

namespace BarberBoss.Domain.Repositories.Receipts;

// Interface para leitura dos dados em banco de dados
public interface IReceiptReadOnlyRepository
{
    Task<List<Receipt>> GetAll();
    Task<Receipt?> GetById(long id);
    Task<List<Receipt>> FilterByMounth(DateOnly date);
}
