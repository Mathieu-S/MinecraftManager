using Microsoft.Extensions.DependencyInjection;
using MinecraftManager.Cli.Commands.Mods;
using MinecraftManager.Cli.Infrastructure;
using MinecraftManager.Core.Repositories;
using MinecraftManager.Core.Services;
using Spectre.Console.Cli;

namespace MinecraftManager.Cli;

public static class Program
{
    public static int Main(string[] args)
    {
        // IoC
        var registrations = new ServiceCollection();
        registrations.AddSingleton<IModService, ModService>();
        registrations.AddSingleton<IModRepository, ModRepository>();
        var registrar = new TypeRegistrar(registrations);

        // Register commands
        var app = new CommandApp(registrar);
        app.Configure(config =>
        {
            config.SetApplicationName("mcm");
            config.SetApplicationVersion("0.1.0");

            config.AddBranch("mods", mods =>
            {
                mods.SetDescription("Manage the mods.");
                mods.AddCommand<AddModCommand>("add");
                mods.AddCommand<SearchCommand>("search");
            });
        });

        return app.Run(args);
    }
}