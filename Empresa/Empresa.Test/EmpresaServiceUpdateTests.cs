using System.Linq.Expressions;
using Empresa.Application.DTOs;
using Empresa.Application.Services;
using Empresa.Domain.Interfaces;
using Empresa.Domain.Interfaces.Repositories;
using MapsterMapper;
using Moq;
using EmpresaEntity = GerEmpresa.Domain.Entities.Empresa;

namespace Empresa.Test;

public class EmpresaServiceUpdateTests
{
    [Fact]
    public async Task UpdateAsync_EmpresaExistente_AlteraInstanciaESalvaPeloUnitOfWork()
    {
        var empresa = new EmpresaEntity { Id = 1, Nome = "Anterior" };
        var repo = new Mock<IEmpresaRepository>(MockBehavior.Strict);
        repo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<EmpresaEntity, bool>>>()))
            .ReturnsAsync(empresa);
        var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
        unitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        var service = new EmpresaService(repo.Object, Mock.Of<IMapper>(), unitOfWork.Object);
        var dto = new EmpresaDto
        {
            Nome = "Atualizada", Email = "empresa@exemplo.com",
            Contato = "123", Endereco = "Rua A", DataCadastro = new DateTime(2025, 1, 1)
        };

        var result = await service.UpdateAsync(1, dto);

        Assert.True(result);
        Assert.Equal(dto.Nome, empresa.Nome);
        Assert.Equal(dto.Email, empresa.Email);
        Assert.Equal(dto.Contato, empresa.Contato);
        Assert.Equal(dto.Endereco, empresa.Endereco);
        Assert.Equal(dto.DataCadastro, empresa.DataCadastro);
        unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        repo.Verify(r => r.UpdateAsync(It.IsAny<EmpresaEntity>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_EmpresaInexistente_NaoSalva()
    {
        var repo = new Mock<IEmpresaRepository>(MockBehavior.Strict);
        repo.Setup(r => r.GetAsync(It.IsAny<Expression<Func<EmpresaEntity, bool>>>()))
            .ReturnsAsync((EmpresaEntity?)null);
        var unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
        var service = new EmpresaService(repo.Object, Mock.Of<IMapper>(), unitOfWork.Object);

        Assert.False(await service.UpdateAsync(1, new EmpresaDto()));

        unitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
