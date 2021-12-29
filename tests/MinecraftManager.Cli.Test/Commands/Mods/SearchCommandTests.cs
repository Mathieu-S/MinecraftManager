using System;
using System.Collections.Generic;
using MinecraftManager.Cli.Commands.Mods;
using MinecraftManager.Core.Exceptions;
using MinecraftManager.Core.Models;
using MinecraftManager.Core.Services;
using Moq;
using Spectre.Console.Cli;
using Spectre.Console.Testing;
using Xunit;

namespace MinecraftManager.Cli.Test.Commands.Mods;

public class SearchCommandTests
{
    private readonly CommandAppTester _app;
    private readonly Mock<IModService> _modService;

    public SearchCommandTests()
    {
        _app = new CommandAppTester();
        _modService = new Mock<IModService>();

        _app.SetDefaultCommand<SearchCommand>();
        _app.Configure(config =>
        {
            config.Settings.Registrar.RegisterInstance(_modService.Object);
            config.PropagateExceptions();
        });
    }

    [Fact]
    public void Execute_Found_Results()
    {
        // Arrange
        var mods = new List<Mod>
        {
            new() { Id = 1, Name = "HoSg2odc" },
            new() { Id = 2, Name = "lv3" }
        };
        _modService
            .Setup(x => x.SearchAsync(It.IsAny<string>()))
            .ReturnsAsync(mods);

        // Act
        var result = _app.Run("JEI");

        // Assert
        Assert.Equal(0, result.ExitCode);
    }

    [Fact]
    public void Execute_NotFound_Results()
    {
        // Arrange
        _modService
            .Setup(x => x.SearchAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Mod>());

        // Act
        var result = _app.Run("JEI");

        // Assert
        Assert.Equal(1, result.ExitCode);
    }

    [Fact]
    public void Execute_Throw_CurseApiException()
    {
        // Arrange
        _modService
            .Setup(x => x.SearchAsync(It.IsAny<string>()))
            .ThrowsAsync(new CurseApiException());

        // Act & Assert
        var result = _app.Run("JEI");

        // Assert
        Assert.Equal(-1, result.ExitCode);
    }

    [Fact]
    public void Execute_Throw_UnexpectedException()
    {
        // Arrange
        _modService
            .Setup(x => x.SearchAsync(It.IsAny<string>()))
            .ThrowsAsync(new Exception());

        // Act & Assert
        var result = _app.Run("JEI");

        // Assert
        Assert.Equal(-2, result.ExitCode);
    }
}