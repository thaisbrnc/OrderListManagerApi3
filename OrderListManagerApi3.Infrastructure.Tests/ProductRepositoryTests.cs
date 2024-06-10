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
            _fileGeneratorMock = new Mock<IJsonLocalFileGenerator>();
            _groups = new();
        }

        [Theory]
        [InlineData("Detergente", "Limpeza")]
        public void AddProductTest_ProductAlreadyExists_Failure(string productName, string groupName)
        {
            try
            {
                //arrange
                var group = new Group() { Description = groupName };
                _groups.Add(group);
                var product = new Product() { Description = productName, IsChecked = false };
                _groups[0].products.Add(product);

                //setup
                _fileGeneratorMock.Setup(j => j.Serialize(_groups));
                _fileGeneratorMock.Setup(j => j.Deserialize()).Returns(_groups);
                var productRepository = new ProductRepository(new ProductDto(), _fileGeneratorMock.Object);

                //act
                var product2 = productRepository.Add(productName, groupName);
            }
            catch(Exception ex)
            {
                //assert
                Assert.Matches($"Erro: Produto '{productName}' já consta na lista.", ex.Message);
            }
        }

        [Theory]
        [InlineData("Sabonete", "Higiene")]
        public void AddProductTest_AddedSuccessfully_Succes(string productName, string groupName)
        {
            //arrange
            var group = new Group() { Description = groupName };
            _groups.Add(group);
            var product1 = new ProductDto() { Name = productName, IsChecked = false };

            //setup
            _fileGeneratorMock.Setup(j => j.Deserialize()).Returns(_groups);
            var productRepository = new ProductRepository(new ProductDto(), _fileGeneratorMock.Object);

            //act
            var product2 = productRepository.Add(productName, groupName);

            //assert
            Assert.Equal(product1.Name, product2.Name);
        }
    }
}