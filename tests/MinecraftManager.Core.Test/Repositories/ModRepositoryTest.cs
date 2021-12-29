using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Testing;
using MinecraftManager.Core.Models;
using MinecraftManager.Core.Repositories;
using Xunit;

namespace MinecraftManager.Core.Test.Repositories;

public class ModRepositoryTest
{
    private readonly IModRepository _modRepository;
    private readonly HttpTest _httpTest;

    public ModRepositoryTest()
    {
        _modRepository = new ModRepository();
        _httpTest = new HttpTest();
    }

    [Fact]
    public async Task GetByIdAsync()
    {
        // Arrange
        _httpTest.RespondWithJson(new Mod { Id = 1, Name = "krt" });

        // Act
        var result = await _modRepository.GetAsync(100);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<Mod>(result);
    }

    [Fact]
    public async Task GetByIdAsync_NotFound()
    {
        // Arrange
        _httpTest.RespondWithJson(null, 404);

        // Act & Assert
        await Assert.ThrowsAsync<FlurlHttpException>(() => _modRepository.GetAsync(1));
    }

    [Fact]
    public async Task SearchModByNameAsync()
    {
        // Arrange
        var mods = new List<Mod>
        {
            new() { Id = 1, Name = "1R2" },
            new() { Id = 2, Name = "5ZtI42" }
        };

        _httpTest.RespondWithJson(mods);

        // Act
        var result = await _modRepository.SearchAsync("JEI");

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Mod>>(result);
    }

    [Fact]
    public async Task SearchModByNameAsync_Throw_TimeoutException()
    {
        // Arrange
        _httpTest.SimulateTimeout();

        // Act & Assert
        await Assert.ThrowsAsync<TimeoutException>(() => _modRepository.SearchAsync("JEI"));
    }

    [Theory]
    [InlineData(404)]
    [InlineData(500)]
    public async Task SearchModByNameAsync_Throw_HttpRequestException(int statusCode)
    {
        // Arrange
        _httpTest.RespondWithJson(null, statusCode);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _modRepository.SearchAsync("JEI"));
    }
}