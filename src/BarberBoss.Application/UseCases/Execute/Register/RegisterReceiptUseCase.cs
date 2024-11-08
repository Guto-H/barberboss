using AutoMapper;
using BarberBoss.Application.UseCases.Validator;
using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Entity;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Receipts;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Execute.RegisterReceipt;

/* Variaveis privadas para contato via interface com as classes essenciais.
 * _repository chama funções da classe que contem métodos responsaveis por escrever/deletar dados do banco de dados.
 * _mapper chama o mapeamento automatico feito pelo AutoMapper compativel já pré configurado na classe AutoMapping.
 * _unitOfWork chama metodo para salvar alterações no banco de dados.
 * 
 * entity usar o mapper para fazer mapeamento do objeto "Receipt" de acodo com os dados da request.
 * 
 * função Validate faz a validação dos dados vindo da request, validação feita dentro da classe RegisterReceiptValidator
 * caso a propriedade isValid da validação for falsa irá coletar dos os erros vindo da validação, transformar em lista e 
 * enviar para o tratamento de erros em ErrorOnValidationException
*/
public class RegisterReceiptUseCase(IReceiptWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : IRegisterReceiptUseCase
{

    private readonly IReceiptWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseRegisteredReceiptJson> Execute(RequestReceiptJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Receipt>(request);

        await _repository.Add(entity);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredReceiptJson>(entity);
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
