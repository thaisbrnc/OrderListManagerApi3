using System;
using Moq;
using OrderListManagerApi3.Infrastructure.Repository;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Tests
{
	public class ProductRepositoryTests
	{
        private readonly Mock<IJsonLocalFileGenerator> _fileGeneratorMock;
        private List<Group> _groups;
        public ProductRepositoryTests()
		{
		}

        [Theory]
        [InlineData("Detergente", "Limpeza")]
        public void AddProductTest_ProdutoJaAdicionado_Failure(string productName, string groupName)
        {
            //arrange
            
            ProductRepository productRepository = new ProductRepository(new ProductDto(), _fileGeneratorMock.Object, _groups);
            Group group = new Group() { Description = groupName };
            _groups.Add(group);

            Product product = new Product() { Description = productName };
            _groups[0].products.Add(product);


            //act
            string retorno = productRepository.Add(productName, groupName);

            //assert
            Assert.Matches($"Produto '{productName}' já consta na lista.", retorno);
        }

    }
}

