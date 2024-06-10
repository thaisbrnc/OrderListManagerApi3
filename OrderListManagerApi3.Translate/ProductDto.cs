using OrderListManagerApi3.Models;

namespace OrderListManagerApi3.Translate
{
	public class ProductDto : ITranslator<Product, ProductDto>
	{
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public ProductDto ToDto(Product product)
        {
            try
            {
                var productDto = new ProductDto
                {
                    Name = product.Description,
                    IsChecked = product.IsChecked
                };

                return productDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public Product ToEntity(ProductDto productDto)
        {
            try
            {
                var product = new Product
                {
                    Description = productDto.Name,
                    IsChecked = productDto.IsChecked
                };

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }
    }
}

