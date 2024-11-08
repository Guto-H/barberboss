namespace BarberBoss.Application.UseCases.Execute.Reports.Pdf;
public interface IGenerateReceiptsReportPdfUseCase
{
    public Task<byte[]> Execute(DateOnly month);
}
