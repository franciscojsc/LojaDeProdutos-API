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
    public class ProdutosControllerTest
    {
        private readonly Mock<DbSet<Produto>> _mockSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Categoria _categoria;
        private readonly Produto _produto;

        public ProdutosControllerTest()
        {
            _mockSet = new Mock<DbSet<Produto>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _categoria = new Categoria { Id = 1, Descricao = "Teste Categoria" };
            _produto = new Produto { Id = 1, Descricao = "Teste Produto", Quantidade = 10, CategoriaId = _categoria.Id };

            _mockContext.Setup(m => m.Produtos).Returns(_mockSet.Object);
            _mockContext.Setup(m => m.Produtos.FindAsync(1)).ReturnsAsync(_produto);

            _mockContext.Setup(m => m.SetModifield(_produto));
            _mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task Get_Produto()
        {
            var service = new ProdutosController(_mockContext.Object);

            await service.GetProduto(1);

            _mockSet.Verify(m => m.FindAsync(1), Times.Once());
        }

        [Fact]
        public async Task Put_produto()
        {
            var service = new ProdutosController(_mockContext.Object);

            await service.PutProduto(1, _produto);

            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Post_produto()
        {
            var service = new ProdutosController(_mockContext.Object);

            await service.PostProduto(_produto);

            _mockSet.Verify(m => m.Add(_produto), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_produto()
        {
            var service = new ProdutosController(_mockContext.Object);

            await service.DeleteProduto(1);

            _mockSet.Verify(m => m.FindAsync(1), Times.Once);
            _mockSet.Verify(m => m.Remove(_produto), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
