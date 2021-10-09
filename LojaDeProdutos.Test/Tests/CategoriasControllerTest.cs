using System.Threading;
using System.Threading.Tasks;
using LojaDeProdutos.API.Context;
using LojaDeProdutos.API.Controllers;
using LojaDeProdutos.API.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LojaDeProdutos.Test.Tests
{
    public class CategoriasControllerTest
    {
        private readonly Mock<DbSet<Categoria>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Categoria _categoria;

        public CategoriasControllerTest()
        {
            _mockSet = new Mock<DbSet<Categoria>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _categoria = new Categoria { Id = 1, Descricao = "Teste Categoria" };

            _mockContext.Setup(m => m.Categorias).Returns(_mockSet.Object);
            _mockContext.Setup(m => m.Categorias.FindAsync(1)).ReturnsAsync(_categoria);

            _mockContext.Setup(m => m.SetModifield(_categoria));
            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Get_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);

            var testCategoria = await service.GetCategoria(1);

            // Verifica se executou o FindAsync apenas uma vez
            _mockSet.Verify(m => m.FindAsync(1), Times.Once());

            Assert.NotNull(testCategoria);
            Assert.Equal(_categoria.Id, testCategoria.Value.Id);
            Assert.Equal(_categoria.Descricao, testCategoria.Value.Descricao);
        }

        [Fact]
        public async Task Put_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);

            await service.PutCategoria(1, _categoria);

            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Post_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);

            await service.PostCategoria(_categoria);

            _mockSet.Verify(m => m.Add(_categoria), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Categoria()
        {
            var service = new CategoriasController(_mockContext.Object);

            await service.DeleteCategoria(1);

            _mockSet.Verify(m => m.FindAsync(1), Times.Once);
            _mockSet.Verify(m => m.Remove(_categoria), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
