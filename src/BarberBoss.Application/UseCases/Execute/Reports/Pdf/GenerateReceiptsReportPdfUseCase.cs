using BarberBoss.Application.UseCases.Execute.Reports.Pdf.Color;
using BarberBoss.Application.UseCases.Execute.Reports.Pdf.Fonts;
using BarberBoss.Domain.Extension;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Reports.Conversor;
using BarberBoss.Domain.Repositories.Receipts;

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Execute.Reports.Pdf;

/* Construtor para coletar a comunicação com o banco de dados (Repository) e para alterar a Fonte do arquivo para a que foi criada
 * Metodos para Criar a estrutura do documento PDF 
 * Criar pagina do PDF 
 * Criar cabeçalho com foto e nome
 * Adicionar o valor total do faturamento
 * Renderizar o documento
 */
public class GenerateReceiptsReportPdfUseCase : IGenerateReceiptsReportPdfUseCase
{

    private readonly IReceiptReadOnlyRepository _repository;
    public GenerateReceiptsReportPdfUseCase(IReceiptReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new ReceiptFontResolver();

    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        
        var filtredMonth = await _repository.FilterByMounth(month);

        if (filtredMonth.Count == 0)
        {
            return [];
        }

        var doc = CreatDocument(month);
        var page = CreatPage(doc);

        CreateHeaderWithPhotoAndName(page);

        var totalRevenue = filtredMonth.Sum(total => total.Amount);

        CreateTotalRevenue(page, month, totalRevenue);

        foreach(var receipt in filtredMonth)
        {
            var table = CreateTableWithValues(page);

            var row = table.AddRow();
            row.Height = 25;

            AddTitle(row.Cells[0], receipt.Title);
            AddAmountHeader(row.Cells[3]);
            
            row = table.AddRow();
            row.Height = 25;

            row.Cells[0].AddParagraph(receipt.Date.ToString("dd MMMM yyyy"));
            StyleForInformations(row.Cells[0]);
            row.Format.LeftIndent = 9;

            row.Cells[1].AddParagraph(receipt.Date.ToString("t"));
            StyleForInformations(row.Cells[1]);

            row.Cells[2].AddParagraph(receipt.ReceiptType.ReceiptTypeToString());
            StyleForInformations(row.Cells[2]);

            AddAmount(row.Cells[3], receipt.Amount);

            if (string.IsNullOrEmpty(receipt.Description) == false)
            {
                var rowDescription = table.AddRow();
                rowDescription.Height = 25;

                rowDescription.Cells[0].AddParagraph(receipt.Description);
                rowDescription.Cells[0].Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 9, Color = ColorHelper.DESCR_FONT};
                rowDescription.Cells[0].Shading.Color = ColorHelper.DESCR_BG;
                rowDescription.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                rowDescription.Cells[0].Format.LeftIndent = 7;
                rowDescription.Cells[0].MergeRight = 2;

            }

            AddWhiteSpace(table);
        }
        
        return RenderDocument(doc); 
    }

    public Document CreatDocument(DateOnly month)
    {
        var doc = new Document();

        doc.Info.Title = $"{ResourceReportGenerateMessage.BILLING_FOR_THE_MONTH_OF} {month:Y}";
        doc.Info.Author = $"{ResourceReportGenerateMessage.BARBERSHOP}";

        var font = doc.Styles["Normal"];
        font!.Font.Name = FontHelper.ROBOTO_REGULAR;

        return doc;
    }
    private Section CreatPage(Document doc)
    {
        var section = doc.AddSection();
        section.PageSetup = doc.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.TopMargin = 50;
        section.PageSetup.BottomMargin = 50;
        section.PageSetup.RightMargin = 35;
        section.PageSetup.LeftMargin = 35;

        return section;
    }
    private void CreateHeaderWithPhotoAndName(Section page)
    {
        var table = page.AddTable();
        var assembly = Assembly.GetExecutingAssembly();
        var directoyName = Path.GetDirectoryName(assembly.Location);

        table.AddColumn();
        table.AddColumn("300");
        var row = table.AddRow();

        row.Cells[0].AddImage(Path.Combine(directoyName!, "Logo", "logo.png"));

        row.Cells[1].AddParagraph(ResourceReportGenerateMessage.BARBERSHOP);
        row.Cells[1].Format.Font = new Font { Name = FontHelper.BEBASNENUE_REGULAR, Size = 25, Bold = true, Color = ColorHelper.BLACK };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;

    }
    private Table CreateTableWithValues(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("160").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }
    private void CreateTotalRevenue(Section page, DateOnly month, decimal totalRevenue)
    {
        var paragraph = page.AddParagraph();
        var title = string.Format(ResourceReportGenerateMessage.TOTAL_REVENUE, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.ROBOTO_MEDIUM, Size = 15} );
        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{ResourceReportConversorReceiptType.SYMBOL} {totalRevenue}", new Font { Name= FontHelper.BEBASNENUE_REGULAR, Size= 50, Bold = true} );

        paragraph.Format.SpaceBefore = 38;
        paragraph.Format.SpaceAfter = 64;
        
    }
    private void AddTitle(Cell cell, string title)
    {
        cell.AddParagraph(title);
        cell.Format.Font = new Font { Name = FontHelper.BEBASNENUE_REGULAR, Size = 15, Bold = true, Color = ColorHelper.WHITE} ;
        cell.Shading.Color = ColorHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 7;
        cell.MergeRight = 2;
    } 
    private void AddAmountHeader(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerateMessage.AMOUNT_PDF);
        cell.Format.Font = new Font { Name = FontHelper.BEBASNENUE_REGULAR, Size = 15, Bold = true, Color = ColorHelper.WHITE} ;
        cell.Shading.Color = ColorHelper.GREEN_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.RightIndent = 2;
    }
    private void AddAmount(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{ResourceReportConversorReceiptType.SYMBOL} {amount}");
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorHelper.BLACK };
        cell.Shading.Color = ColorHelper.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }
    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }
    private void StyleForInformations(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.ROBOTO_REGULAR, Size = 10, Color = ColorHelper.BLACK} ;
        cell.Shading.Color = ColorHelper.TEXT_BG;
        cell.VerticalAlignment = VerticalAlignment.Center;

    }
    private byte[] RenderDocument(Document doc)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = doc
        };

        renderer.RenderDocument();

        using var file = new MemoryStream(); 

        renderer.PdfDocument.Save(file);

        return file.ToArray();
        
    }
}
