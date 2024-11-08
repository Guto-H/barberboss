namespace BarberBoss.Exception.ExceptionBase;


public abstract class BarberBossException : SystemException
{
    protected BarberBossException(string ErrorMessageNotFound) : base(ErrorMessageNotFound){}

    public abstract int StatusCode { get; }

    public abstract List<string> ErrorsMessage();

}
