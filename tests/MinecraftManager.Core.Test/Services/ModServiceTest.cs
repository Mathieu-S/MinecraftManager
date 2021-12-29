using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MinecraftManager.Core.Exceptions;
using MinecraftManager.Core.Models;
using MinecraftManager.Core.Repositories;
using MinecraftManager.Core.Services;
using Moq;
using Xunit;

namespace MinecraftManager.Core.Test.Services;

public class ModServiceTest
{
    private readonly ModService _modService;
    private readonly Mock<IModRepository> _modRepository;

    public ModServiceTest()
    {
        _modRepository = new Mock<IModRepository>();
        _modService = new ModService(_modRepository.Object);
    }

    [Fact]
    public async Task Search()
    {
        // Arrange
        _modRepository.Setup(x => x.SearchAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Mod>());

        // Act
        var result = await _modService.SearchAsync("JEI");

        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task Search_Throw_ArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _modService.SearchAsync(null));
    }
    
    [Fact]
    public async Task Search_RetryPolicy()
    {
        // Arrange
        _modRepository.SetupSequence(x => x.SearchAsync(It.IsAny<string>()))
            .ThrowsAsync(new TimeoutException())
            .ReturnsAsync(new List<Mod>());

        // Act
        var result = await _modService.SearchAsync("JEI");

        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task Search_RetryPolicy_Fail()
    {
        // Arrange
        _modRepository.SetupSequence(x => x.SearchAsync(It.IsAny<string>()))
            .ThrowsAsync(new TimeoutException())
            .ThrowsAsync(new TimeoutException())
            .ThrowsAsync(new TimeoutException())
            .ReturnsAsync(new List<Mod>());

        // Act & Assert
        await Assert.ThrowsAsync<CurseApiException>(() => _modService.SearchAsync("JEI"));
    }
    
    [Fact]
    public async Task Search_Throw_CurseApiException()
    {
        // Arrange
        _modRepository.Setup(x => x.SearchAsync(It.IsAny<string>()))
            .ThrowsAsync(new HttpRequestException());
        
        // Act & Assert
        await Assert.ThrowsAsync<CurseApiException>(() => _modService.SearchAsync("JEI"));
    }
}