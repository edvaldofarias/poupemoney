using System.Globalization;

namespace PoupeMoney.Core.WebApi.Infrastructure.Culture;

[ExcludeFromCodeCoverage]
internal static class CultureService
{
    internal static void AddCulture(this IServiceCollection services)
    {
        var cultureInfo = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        ValidatorOptions.Global.LanguageManager.Culture = cultureInfo;
    }
}