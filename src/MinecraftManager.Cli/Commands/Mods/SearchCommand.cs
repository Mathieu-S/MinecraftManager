using System.ComponentModel;
using MinecraftManager.Core.Exceptions;
using MinecraftManager.Core.Models;
using MinecraftManager.Core.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace MinecraftManager.Cli.Commands.Mods;

[Description("Search mods by name.")]
public class SearchCommand : AsyncCommand<SearchCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "<MODNAME>")]
        [Description("Mod name.")]
        public string ModName { get; set; }
    }

    private readonly IModService _modService;

    public SearchCommand(IModService modService)
    {
        _modService = modService ?? throw new ArgumentNullException(nameof(modService));
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        List<Mod> mods;

        try
        {
            mods = (List<Mod>)await _modService.SearchAsync(settings.ModName);
        }
        catch (CurseApiException e)
        {
            AnsiConsole.MarkupLine($"[red]{e.Message}[/]");
            return -1;
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return -2;
        }

        if (!mods.Any())
        {
            AnsiConsole.Write(new Markup(
                $"[yellow]Sorry but your search terms \"{settings.ModName}\" did not return any results.[/]"));
            return 1;
        }

        var table = new Table();
        table.AddColumns("Id", "Name");

        foreach (var mod in mods)
        {
            table.AddRow(mod.Id.ToString(), mod.Name.EscapeMarkup());
        }

        AnsiConsole.WriteLine($"Your search terms return {mods.Count}");
        AnsiConsole.Write(table);

        return 0;
    }
}