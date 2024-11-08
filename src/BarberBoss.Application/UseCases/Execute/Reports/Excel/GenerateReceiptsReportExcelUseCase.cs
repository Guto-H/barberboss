
using BarberBoss.Domain.Extension;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Reports.Conversor;
using BarberBoss.Domain.Repositories.Receipts;
using ClosedXML.Excel;

namespace BarberBoss.Application.UseCases.Execute.Reports.Excel;

/* Classe com metodo Execute para Filtrar o conteudo de acordo com o Mes recebido.
 * Criar a estrutura do arquivo excel "WorkBook".
 * Funções VOID para: 
 *     - Inserir informações no cabeçalho com formatação.
 *     - Alinhamento correto das informações.
 * Passar os valores para as células usando repetição ForEach e convertendo Enum (ReceiptType) em String com função via extensão para conversão
*/
public class GenerateReceiptsReportExcelUseCase : IGenerateReceiptsReportExcelUseCase
{ 
    private readonly IReceiptReadOnlyRepository _repositoy;

    public GenerateReceiptsReportExcelUseCase(IReceiptReadOnlyRepository repository)
    {
        _repositoy = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    { 
        var filteredReceipt = await _repositoy.FilterByMounth(month);

        if (filteredReceipt.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();

        workbook.Author = "Developer";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        CreateHeader(worksheet);

        var raw = 2;

        foreach (var receipt in filteredReceipt)
        {
            worksheet.Cell($"A{raw}").Value = receipt.Title;
            worksheet.Cell($"B{raw}").Value = receipt.Date.ToShortDateString();
            worksheet.Cell($"C{raw}").Value = receipt.ReceiptType.ReceiptTypeToString();

            worksheet.Cell($"D{raw}").Value = receipt.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"{ResourceReportConversorReceiptType.SYMBOL} #,##0,00";

            worksheet.Cell($"E{raw}").Value = receipt.Description;

            AligmentCells(raw, worksheet);

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();

        workbook.SaveAs(file);

        return file.ToArray();
        
    }

    private void CreateHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerateMessage.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerateMessage.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerateMessage.RECEIPT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerateMessage.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerateMessage.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");
        worksheet.Cells("A1:E1").Style.Font.FontColor = XLColor.White;

        worksheet.Cells("A1:C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }

    private void AligmentCells(int raw, IXLWorksheet worksheet) {

        worksheet.Cell($"A{raw}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        worksheet.Cell($"B{raw}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell($"C{raw}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell($"D{raw}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell($"E{raw}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
