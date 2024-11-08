using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Receipts;
using BarberBoss.Infrastructure.DbAccess;
using BarberBoss.Infrastructure.DbAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure;

/* Adicionando Interfaces e classes via Injeção de Dependencia
 * Separadas em classes por funcionalidades
 * 
 * Usando extensão de codigo fonte, com This
 * 
 * AddDbContext possui a rota para acessar o banco de dados, "connections" está com as informações necessarias para se conectar ao banco de dados
 * connections se encontra em appsettings.Json no projeto BarberBoss.API, necessario configurar de acordo com as informações da máquina
 */
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReceiptWriteOnlyRepository, ReceiptRepository>();
        services.AddScoped<IReceiptReadOnlyRepository, ReceiptRepository>();
        services.AddScoped<IReceiptUpdateOnlyRepository, ReceiptRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration) {

        var connectString = configuration.GetConnectionString("connections");

        var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));

        services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectString, serverVersion));

    }
}
