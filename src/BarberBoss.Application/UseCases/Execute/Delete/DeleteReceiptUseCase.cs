using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Receipts;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Execute.DeleteReceipt;

/* Variaveis privadas para contato via interface com as classes essenciais.
 * _repository chama funções da classe que contem métodos responsaveis por escrever/deletar dados do banco de dados.
 * _unitOfWork chama metodo para salvar alterações no banco de dados.
*/ 
public class DeleteReceiptUseCase(IReceiptWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteReceiptUseCase
{
    public readonly IReceiptWriteOnlyRepository _repository = repository;
    public readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result is false)
        {
            throw new NotFoundException(ResourceErrorMessage.RECEIPT_NOT_FOUND);
        }

         await _unitOfWork.Commit();

    }
}
