using BarberBoss.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infrastructure.DbAccess;

/* Classe para contato com banco de dados MySQL utilizando pacote NuGet EnityFrameWorkCore
 * Revenue faz contato com BD
*/
public class BarberBossDbContext : DbContext
{
    public BarberBossDbContext(DbContextOptions contextOptions) : base(contextOptions){}
    public DbSet<Receipt> Revenue { get; set; }



}
