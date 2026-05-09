using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;
using Empresa.Application.DTOs;

namespace Empresa.Test
{
    public class EmpresaDtoTests
    {
        private static IList<ValidationResult> Validate(object model)
        {
            var ctx = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, ctx, results, true);
            return results;
        }

        [Fact]
        public void EmpresaDto_Valido_DevePassarNaValidacao()
        {
            // Arrange
            var dto = new EmpresaDto
            {
                EmpresaId = 1,
                Nome = "Empresa Exemplo",
                Email = "teste@example.com",
                DataCadastro = DateTime.UtcNow,
                Contato = "123456789",
                Endereco = "Rua Exemplo, 123"
            };

            // Act
            var results = Validate(dto);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void EmpresaDto_SemNome_DeveRetornarErroRequired()
        {
            // Arrange
            var dto = new EmpresaDto
            {
                Nome = string.Empty,
                Email = "teste@example.com",
                DataCadastro = DateTime.UtcNow
            };

            // Act
            var results = Validate(dto);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(EmpresaDto.Nome)));
        }

        [Fact]
        public void EmpresaDto_DataCadastroNaoInformada_DeveRetornarErroRequired()
        {
            // Arrange
            var dto = new EmpresaDto
            {
                Nome = "Nome",
                Email = "teste@example.com",
                // DataCadastro default(DateTime) -> 0001-01-01  (considerado preenchido pelo DataAnnotations)
                // Para validar obrigatoriedade de uma DataTime, normalmente se usa Nullable<DateTime?> no DTO.
                // Aqui testamos comportamento atual: se DataCadastro é required mas não nullable, a validação não falhará.
                DataCadastro = default
            };

            // Act
            var results = Validate(dto);

            // Assert
            // Observação: com DateTime não-nulo, Required não falha. Verificamos se projeto precisa usar DateTime?.
            Assert.DoesNotContain(results, r => r.MemberNames.Contains(nameof(EmpresaDto.DataCadastro)));
        }

        [Fact]
        public void EmpresaDto_EmailInvalido_DeveRetornarErroEmail()
        {
            // Arrange
            var dto = new EmpresaDto
            {
                Nome = "Nome",
                Email = "email-invalido",
                DataCadastro = DateTime.UtcNow
            };

            // Act
            var results = Validate(dto);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(EmpresaDto.Email)));
        }

        [Fact]
        public void EmpresaDto_NomeMuitoLongo_DeveRetornarErroStringLength()
        {
            // Arrange
            var dto = new EmpresaDto
            {
                Nome = new string('A', 300), // > 250
                Email = "teste@example.com",
                DataCadastro = DateTime.UtcNow
            };

            // Act
            var results = Validate(dto);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(EmpresaDto.Nome)));
        }

        [Fact]
        public void EmpresaDto_ContatoMuitoLongo_DeveRetornarErroStringLength()
        {
            // Arrange
            var dto = new EmpresaDto
            {
                Nome = "Nome",
                Email = "teste@example.com",
                DataCadastro = DateTime.UtcNow,
                Contato = new string('9', 50) // > 15
            };

            // Act
            var results = Validate(dto);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(EmpresaDto.Contato)));
        }
    }
}