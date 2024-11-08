
using AutoMapper;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Repositories.Receipts;

namespace BarberBoss.Application.UseCases.Execute.GetAllReceipt;

/* Variaveis privadas para contato via interface com as classes essenciais.
 * _repository chama funções da classe que contem métodos responsaveis por ler dados do banco de dados.
 * _mapper chama o mapeamento automatico feito pelo AutoMapper compativel já pré configurado na classe AutoMapping.
*/
public class GetAllReceiptUseCase(IReceiptReadOnlyRepository repository, IMapper mapper) : IGetAllReceiptUseCase
{
    private readonly IReceiptReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseReceiptJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseReceiptJson
        {
            ResponseReceipt = _mapper.Map<List<ResponseShortReceiptJson>>(result)
        };
    }
}
