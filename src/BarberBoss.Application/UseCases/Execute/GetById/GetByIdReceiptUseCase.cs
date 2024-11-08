using AutoMapper;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Repositories.Receipts;

namespace BarberBoss.Application.UseCases.Execute.GetByIdReceipt;

/* Variaveis privadas para contato via interface com as classes essenciais.
 * _repository chama funções da classe que contem métodos responsaveis por ler dados do banco de dados.
 * _mapper chama o mapeamento automatico feito pelo AutoMapper compativel já pré configurado na classe AutoMapping.
*/
public class GetByIdReceiptUseCase(IReceiptReadOnlyRepository repository, IMapper mapper) : IGetByIdReceiptUseCase
{
    private readonly IReceiptReadOnlyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseReceiptGetByIdJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        return _mapper.Map<ResponseReceiptGetByIdJson>(result);
    }
}
