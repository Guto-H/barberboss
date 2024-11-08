using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Execute.Reports.Pdf.Fonts;

/* Implementando Classes obrigatorias do IFontResolver onde GetFont coleta as fontes personalizadas e retorna.
 * Caso a fonte seja nula será passado uma fonte por padrão.
 * Tambem tem a classe que pegara o nome da Fonte que queremos e outra que irá buscar o nome da fonte na pasta onde está armazenada.
 */
public class ReceiptFontResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        stream ??= ReadFontFile(FontHelper.DEFAULT_FONT);

        var lenght = (int)stream!.Length;

        var data = new byte[lenght];

        stream.Read(data, 0, lenght);

        return data;
        
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName); 
    }

    private Stream? ReadFontFile(string facename)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"BarberBoss.Application.UseCases.Execute.Reports.Pdf.Fonts.{facename}.ttf");
    }
}
