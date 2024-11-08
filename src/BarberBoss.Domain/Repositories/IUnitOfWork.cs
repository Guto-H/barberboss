namespace BarberBoss.Domain.Repositories;

//Interface para qque o aplication consiga chamar o Save Changes (Salvar mudanças) que fica na Infra
public interface IUnitOfWork
{
    Task Commit();
}
