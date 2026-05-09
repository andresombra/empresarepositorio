using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;
using Empresa.Application.DTOs;
using Empresa.Application.DTOs.Response;

namespace Empresa.Test
{
    public class DtoValidationTests
    {
        private static IList<ValidationResult> Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, ctx, results, true);
            return results;
        }

        // FornecedorDto
        [Fact]
        public void FornecedorDto_Valido_DevePassarNaValidacao()
        {
            var dto = new FornecedorDto
            {
                Id = 1,
                Nome = "Fornecedor Exemplo",
                Email = "fornecedor@example.com",
                Cnpj = "12345678901234",
                DataCadastro = DateTime.UtcNow,
                Contato = "11999999999",
                Endereco = "Rua X, 123"
            };

            var results = Validate(dto);
            Assert.Empty(results);
        }

        [Fact]
        public void FornecedorDto_SemNome_RetornaErroRequired()
        {
            var dto = new FornecedorDto
            {
                Nome = string.Empty,
                Email = "ok@example.com",
                DataCadastro = DateTime.UtcNow
            };

            var results = Validate(dto);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(FornecedorDto.Nome)));
        }

        [Fact]
        public void FornecedorDto_EmailInvalido_RetornaErro()
        {
            var dto = new FornecedorDto
            {
                Nome = "Nome",
                Email = "invalido",
                DataCadastro = DateTime.UtcNow
            };

            var results = Validate(dto);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(FornecedorDto.Email)));
        }

        [Fact]
        public void FornecedorDto_CnpjMuitoLongo_RetornaErro()
        {
            var dto = new FornecedorDto
            {
                Nome = "Nome",
                Email = "ok@example.com",
                Cnpj = new string('9', 30), // > 20
                DataCadastro = DateTime.UtcNow
            };

            var results = Validate(dto);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(FornecedorDto.Cnpj)));
        }

        [Fact]
        public void FornecedorDto_ContatoMuitoLongo_RetornaErro()
        {
            var dto = new FornecedorDto
            {
                Nome = "Nome",
                Email = "ok@example.com",
                Contato = new string('1', 100), // >50
                DataCadastro = DateTime.UtcNow
            };

            var results = Validate(dto);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(FornecedorDto.Contato)));
        }

        [Fact]
        public void FornecedorDto_EnderecoMuitoLongo_RetornaErro()
        {
            var dto = new FornecedorDto
            {
                Nome = "Nome",
                Email = "ok@example.com",
                Endereco = new string('E', 500), // >300
                DataCadastro = DateTime.UtcNow
            };

            var results = Validate(dto);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(FornecedorDto.Endereco)));
        }

        // EmpresaResponseDto
        [Fact]
        public void EmpresaResponseDto_Defaults_AreInitialized()
        {
            var dto = new EmpresaResponseDto();
            Assert.NotNull(dto.Nome);
            Assert.NotNull(dto.Email);
            Assert.NotNull(dto.Contato);
            Assert.NotNull(dto.Endereco);
            Assert.NotNull(dto.Usuarios);
            Assert.Empty(dto.Usuarios);
        }

        [Fact]
        public void EmpresaResponseDto_CanSetProperties()
        {
            var usuario = new UsuarioResponseDto { Id = 1, Email = "u@u.com", EmpresaId = 2 };
            var dto = new EmpresaResponseDto
            {
                EmpresaId = 5,
                Nome = "X",
                Email = "e@e.com",
                DataCadastro = DateTime.UtcNow,
                Contato = "cont",
                Endereco = "end",
                Usuarios = new List<UsuarioResponseDto> { usuario }
            };

            Assert.Single(dto.Usuarios);
            Assert.Equal(5, dto.EmpresaId);
            Assert.Equal("X", dto.Nome);
        }

        // UsuarioResponseDto
        [Fact]
        public void UsuarioResponseDto_Defaults_AreInitialized()
        {
            var dto = new UsuarioResponseDto();
            Assert.NotNull(dto.Email);
            Assert.NotNull(dto.Data);
            Assert.NotNull(dto.Situacao);
        }

        [Fact]
        public void UsuarioResponseDto_CanSetProperties()
        {
            var dto = new UsuarioResponseDto
            {
                Id = 10,
                EmpresaId = 20,
                Email = "x@x.com",
                Data = "2023-01-01",
                Situacao = "Ativo",
                Plano = 1,
                Adm = 0
            };

            Assert.Equal(10, dto.Id);
            Assert.Equal(20, dto.EmpresaId);
            Assert.Equal("Ativo", dto.Situacao);
        }
    }
}
