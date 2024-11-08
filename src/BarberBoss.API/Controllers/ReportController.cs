using BarberBoss.Application.UseCases.Execute.Reports.Excel;
using BarberBoss.Application.UseCases.Execute.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BarberBoss.API.Controllers;
[Route("api/[controller]")]
[ApiController]

// EndPoints para coleta de relátorios em Excel e PDF
public class ReportController : ControllerBase
{
    [HttpGet("Excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateReceiptsReportExcelUseCase useCase,
        [FromHeader] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);
 
        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "Report.xlsx");

        return NoContent();
    }


    [HttpGet("PDF")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPDF(
        [FromHeader] DateOnly month,
        [FromServices] IGenerateReceiptsReportPdfUseCase useCase)
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "Report.pdf");

        return NoContent();

    }
}
