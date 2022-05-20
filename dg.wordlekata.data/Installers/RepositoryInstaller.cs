using dg.wordlekata.data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace dg.wordlekata.data.Installers;

public static class RepositoryInstaller
{
    public static void Install(IServiceCollection services)
    {
        services.AddSingleton<IWordRepository, WordRepository>();
    }
}