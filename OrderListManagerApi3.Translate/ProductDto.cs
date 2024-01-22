using OrderListManagerApi3.Models;

namespace OrderListManagerApi3.Translate
{
	public class ProductDto : ITranslator<Product, ProductDto>
	{
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public ProductDto ToDto(Product product)
        {
            ProductDto _productDto = null;

            if (product is not null)
            {
                _productDto = new ProductDto
                {
                    Name = product.Description,
                    IsChecked = product.IsChecked
                };
            }

            return _productDto;
        }

        public Product ToEntity(ProductDto productDto)
        {
            Product _product = null;

            if (productDto is not null)
            {
                _product = new Product
                {
                    Description = productDto.Name,
                    IsChecked = productDto.IsChecked
                };
            }

            return _product;
        }
    }
}

