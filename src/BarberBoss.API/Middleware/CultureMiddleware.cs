using System.Globalization;

namespace BarberBoss.API.Middleware;

//Classe que fará a conversão de idioma da API
public class CultureMiddleware
{
    private readonly RequestDelegate _next;
    public CultureMiddleware(RequestDelegate next) { _next = next; }

    /* listSuportedLanguage: Coleta todas as culturas suportadas pelo .NET
     * requestLanguage: Idioma que o site/app solicita pelo header da requisição.
     * defaultLanguage: Idioma padrão 
    */

    public async Task Invoke(HttpContext context)
    {
        var listSuportedLanguage = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
        
        var requestLanguage = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var defaultLanguage = new CultureInfo("en-US");


        if(string.IsNullOrWhiteSpace(requestLanguage) == false && listSuportedLanguage.Exists(lsuport => lsuport.Name.Equals(requestLanguage)))
        {
            defaultLanguage = new CultureInfo(requestLanguage);
        } 

        CultureInfo.CurrentCulture = defaultLanguage;
        CultureInfo.CurrentUICulture = defaultLanguage; 

        await _next(context);
    }
}
