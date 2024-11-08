namespace BarberBoss.Application.UseCases.Execute.Reports.Excel;
public interface IGenerateReceiptsReportExcelUseCase
{
    public Task<byte[]> Execute(DateOnly month);
}
