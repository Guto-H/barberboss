
using AutoMapper;
using BarberBoss.Application.UseCases.Validator;
using BarberBoss.Communication.Request;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Receipts;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Execute.UpdateReceipt;

/* Variaveis privadas para contato via interface com as classes essenciais.
 * _repository chama funções da classe que contem métodos responsaveis por atualizar dados do banco de dados.
 * _mapper chama o mapeamento automatico feito pelo AutoMapper compativel já pré configurado na classe AutoMapping.
 * _unitOfWork chama metodo para salvar alterações no banco de dados.
 * 
 * Função Validate para validar os dados assim como é feito no Register
 */
public class UpdateReceiptUseCase : IUpdateReceiptUseCase
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReceiptUpdateOnlyRepository _repository;

    public UpdateReceiptUseCase(IMapper mapper, IUnitOfWork unitOfWork, IReceiptUpdateOnlyRepository repository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Execute(long id, RequestReceiptJson request)
    {
        Validate(request);

        var receipt = await _repository.GetById(id);

        if(receipt is null)
        {
            throw new NotFoundException(ResourceErrorMessage.RECEIPT_NOT_FOUND);
        }

        _mapper.Map(request, receipt);

        _repository.Update(receipt);

        await _unitOfWork.Commit();

    }

    private void Validate(RequestReceiptJson request)
    {
        var validator = new RegisterReceiptValidator();

        var resultValidator = validator.Validate(request);

        if (resultValidator.IsValid == false)
        {
            var errorMessage = resultValidator.Errors.Select(le => le.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessage);
        }
    }
}
