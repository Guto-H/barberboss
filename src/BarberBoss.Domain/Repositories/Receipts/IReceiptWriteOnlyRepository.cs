
using BarberBoss.Domain.Entity;

namespace BarberBoss.Domain.Repositories.Receipts;

// Interface para escrever dados no banco de dados, passando entidade como parametro do metodo Add, sendo Task
public interface IReceiptWriteOnlyRepository
{
    Task Add(Receipt receipt);

    Task<bool> Delete(long id);
}
