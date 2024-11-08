using BarberBoss.Domain.Entity;
using BarberBoss.Domain.Repositories.Receipts;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DbAccess.Repositories;

/* Implementando metodos das interfaces que estão em Domain, interface responsaveis por:
 * Escrever/Deletar informações no banco de dados -> IReceiptWriteOnlyRepository
 * Ler/buscar informações no banco de dados -> IReceiptReadOnlyRepository
 * Atualizar informações no banco de dados -> IReceiptUpdateOnlyRepository
 * 
 * _dbContext é o contato direto com o Banco de Dados, vindo da classe BarberBossDbContext
 */

public class ReceiptRepository : IReceiptWriteOnlyRepository, IReceiptReadOnlyRepository, IReceiptUpdateOnlyRepository
{
    private readonly BarberBossDbContext _dbContext;

    public ReceiptRepository(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Receipt receipt)
    {
        await _dbContext.Revenue.AddAsync(receipt);
    }

    public async Task<bool> Delete(long id)
    { 
        var result = await _dbContext.Revenue.FirstOrDefaultAsync(receipt => receipt.Id == id);

        if(result is null)
        {
            return false;
        }

        _dbContext.Revenue.Remove(result);

        return true;
    }

    public async Task<List<Receipt>> GetAll()
    {
        return await _dbContext.Revenue.AsNoTracking().ToListAsync();
    }

    async Task<Receipt?> IReceiptReadOnlyRepository.GetById(long id)
    {
        return await _dbContext.Revenue.AsNoTracking().FirstOrDefaultAsync(receipt => receipt.Id == id);
    }

    async Task<Receipt?> IReceiptUpdateOnlyRepository.GetById(long id)
    {
        return await _dbContext.Revenue.AsNoTracking().FirstOrDefaultAsync(receipt => receipt.Id == id);
    }

    public void Update(Receipt receipt)
    {
         _dbContext.Revenue.Update(receipt);
    }

    public async Task<List<Receipt>> FilterByMounth(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
        var daysInMounth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMounth, hour: 23, minute: 59, second:59);

        return await _dbContext.Revenue.
            AsNoTracking().
            Where(receipt => receipt.Date >= startDate && receipt.Date <= endDate).
            OrderBy(receipt => receipt.Date).
            ToListAsync();
    }
}

