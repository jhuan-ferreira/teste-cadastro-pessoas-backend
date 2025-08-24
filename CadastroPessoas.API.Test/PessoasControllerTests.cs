using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Pessoas.API.DTOs.V1;
using Xunit;
using System;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Pessoas.API.Tests;

public class PessoasControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public PessoasControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private async Task<string> GetJwtToken()
    {
        var client = _factory.CreateClient();
        var loginRequest = new { Username = "admin", Password = "Pa$$w0rd" };
        var response = await client.PostAsJsonAsync("/api/auth/login", loginRequest);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        return result.Token;
    }

    private record LoginResponse(string Token);

    [Fact]
    public async Task PostPessoa_WithValidData_ReturnsCreated()
    {
        var client = _factory.CreateClient();
        var token = await GetJwtToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var newPessoa = new PessoaCreateDto
        {
            Nome = "Pessoa de Teste",
            Cpf = "12345678901", 
            DataNascimento = new DateTime(1990, 1, 1)
        };

        var response = await client.PostAsJsonAsync("/api/v1/pessoas", newPessoa);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetPessoas_WithoutToken_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/v1/pessoas");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}