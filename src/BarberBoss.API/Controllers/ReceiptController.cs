using BarberBoss.Application.UseCases.Execute.DeleteReceipt;
using BarberBoss.Application.UseCases.Execute.GetAllReceipt;
using BarberBoss.Application.UseCases.Execute.GetByIdReceipt;
using BarberBoss.Application.UseCases.Execute.RegisterReceipt;
using BarberBoss.Application.UseCases.Execute.UpdateReceipt;
using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.API.Controllers;
[Route("api/[controller]")]
[ApiController]

/* EndPoints responsavéis pelo gerenciamento do faturamento
 * Funcionalidades CRUD (Create, Read, Update, Delete)
*/
public class ReceiptController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredReceiptJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterReceipt(
        [FromBody] RequestReceiptJson request,
        [FromServices] IRegisterReceiptUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }


    [HttpGet]
    [ProducesResponseType(typeof(ResponseReceiptJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllReceipt([FromServices] IGetAllReceiptUseCase useCase)
    {
        var response = await useCase.Execute();
        return Ok(response);
    }


    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseReceiptGetByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdReceipt(
        [FromRoute] long id,
        [FromServices] IGetByIdReceiptUseCase useCase
        )
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }


    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReceipt(
        [FromRoute] long id,
        [FromServices] IDeleteReceiptUseCase useCase)
    {
        await useCase.Execute(id);
        return NoContent();
    }


    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> UpdateReceipt(
        [FromRoute] long id,
        [FromBody] RequestReceiptJson request,
        [FromServices] IUpdateReceiptUseCase useCase)
    {

        await useCase.Execute(id, request);
        return NoContent();
    }
}
