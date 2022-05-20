using dg.wordlekata.Services;
using Microsoft.Extensions.DependencyInjection;

namespace dg.wordlekata.Installers;

public static class ServiceInstaller
{
    public static void Install(IServiceCollection services)
    {
        services.AddSingleton<IWordService, WordService>();
        services.AddSingleton<IGuessService, GuessService>();
    }
}