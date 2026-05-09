using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Empresa.Application.Services;
using Empresa.Domain.Interfaces.Repositories;
using GerEmpresa.Domain.Entities;
using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;

namespace Empresa.Test
{
    public class UsuarioServiceTests
    {
        [Fact]
        public async Task CriarUsuarioAsync_NullUsuario_ThrowsArgumentNullException()
        {
            // Arrange
            var repoMock = new Mock<IUsuarioRepository>();
            var service = new UsuarioService(repoMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CriarUsuarioAsync(null!));
        }

        [Fact]
        public async Task CriarUsuarioAsync_ValidUsuario_SetsPropertiesAndCallsAdd()
        {
            // Arrange
            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            var service = new UsuarioService(repoMock.Object);
            var usuario = new Usuario { Email = "test@example.com", Senha = "senha" };

            // Act
            await service.CriarUsuarioAsync(usuario);

            // Assert
            repoMock.Verify(r => r.AddAsync(It.Is<Usuario>(u => u.Email == "test@example.com" && u.Situacao == "Ativo")), Times.Once);
            Assert.Equal("Ativo", usuario.Situacao);
            Assert.False(string.IsNullOrWhiteSpace(usuario.Data));
        }

        [Fact]
        public async Task AutenticarAsync_UserNotFound_ReturnsNull()
        {
            // Arrange
            var repoMock = new Mock<IUsuarioRepository>();
            repoMock.Setup(r => r.AutenticarAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Usuario?)null);

            var service = new UsuarioService(repoMock.Object);
            var request = new LoginRequestDto { UserLogin = "u", UserPass = "p" };

            // Act
            var result = await service.AutenticarAsync(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AutenticarAsync_UserFound_ReturnsToken()
        {
            // Arrange
            var repoMock = new Mock<IUsuarioRepository>();
            var user = new Usuario { Email = "u@t.com", Senha = "p" };
            repoMock.Setup(r => r.AutenticarAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);

            var service = new UsuarioService(repoMock.Object);
            var request = new LoginRequestDto { UserLogin = "u", UserPass = "p" };

            // Act
            var result = await service.AutenticarAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result!.Token));
        }

        [Fact]
        public async Task ListarAsync_ReturnsListFromRepository()
        {
            // Arrange
            var repoMock = new Mock<IUsuarioRepository>();
            var lista = new List<Usuario> { new Usuario { Email = "a@a.com" } };
            repoMock.Setup(r => r.ListaAsync()).ReturnsAsync(lista);

            var service = new UsuarioService(repoMock.Object);

            // Act
            var result = await service.ListarAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("a@a.com", result[0].Email);
        }

        [Fact]
        public async Task CriarUsuarioAsync_DeveDefinirSituacaoEData_EChamarRepositorio()
        {
            var repoMock = new Mock<IUsuarioRepository>();
            var service = new UsuarioService(repoMock.Object);

            var usuario = new Usuario { Email = "teste@email.com" };

            await service.CriarUsuarioAsync(usuario);

            Assert.Equal("Ativo", usuario.Situacao);
            Assert.NotNull(usuario.Data);

            repoMock.Verify(x => x.AddAsync(usuario), Times.Once);
        }
    }
}
