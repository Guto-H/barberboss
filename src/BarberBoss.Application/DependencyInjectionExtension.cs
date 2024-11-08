using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Execute.DeleteReceipt;
using BarberBoss.Application.UseCases.Execute.GetAllReceipt;
using BarberBoss.Application.UseCases.Execute.GetByIdReceipt;
using BarberBoss.Application.UseCases.Execute.RegisterReceipt;
using BarberBoss.Application.UseCases.Execute.Reports.Excel;
using BarberBoss.Application.UseCases.Execute.Reports.Pdf;
using BarberBoss.Application.UseCases.Execute.UpdateReceipt;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;

/* Adicionando via Injeção de Dependencias Interfaces e Classes que implementam seus metodos
 * Separadas por funcionalidades em suas respectivas classes
 */
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMaper(services);
    }

    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterReceiptUseCase, RegisterReceiptUseCase>();
        services.AddScoped<IGetAllReceiptUseCase, GetAllReceiptUseCase>();
        services.AddScoped<IGetByIdReceiptUseCase, GetByIdReceiptUseCase>();
        services.AddScoped<IDeleteReceiptUseCase, DeleteReceiptUseCase>();
        services.AddScoped<IUpdateReceiptUseCase, UpdateReceiptUseCase>();
        services.AddScoped<IGenerateReceiptsReportExcelUseCase, GenerateReceiptsReportExcelUseCase>();
        services.AddScoped<IGenerateReceiptsReportPdfUseCase, GenerateReceiptsReportPdfUseCase>();
    }

    public static void AddAutoMaper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }
}
