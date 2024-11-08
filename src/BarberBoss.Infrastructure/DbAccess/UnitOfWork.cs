using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.DbAccess;

// Classe para implemntar o metodo Commit que executará o Save Changes (Salvar Mudanças) no banco de dados
public class UnitOfWork : IUnitOfWork
{
    private readonly BarberBossDbContext _dbContext;

    public UnitOfWork(BarberBossDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit()
    {
       await _dbContext.SaveChangesAsync();
    }
}
