using System;
using Moq;
using OrderListManagerApi3.Infrastructure.Repository;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Tests
{
	public class UnitTestProductRepository
	{
        private readonly Mock<Database> _database = new Mock<Database>();

        public UnitTestProductRepository()
		{
		}

        [Theory]
        [InlineData("Detergente", "Limpeza")]
        public void AddProductTest_ProdutoJaAdicionado_Failure(string productName, string groupName)
        {
            //arrange
            ProductRepository productRepository = new ProductRepository(new ProductDto(), _database.Object);
            Group group = new Group() { Description = groupName };
            _database.Object.groups.Add(group);

            Product product = new Product() { Description = productName };
            _database.Object.groups[0].products.Add(product);


            //act
            string retorno = productRepository.Add(productName, groupName);

            //assert
            Assert.Matches($"Produto '{productName}' já consta na lista.", retorno);
        }

    }
}

