using System;
using OrderListManagerApi3.Infrastructure.Repository;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Services
{
	public class ProductService
	{
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDto Add(string productName, string groupName)
        {
            return _productRepository.Add(productName, groupName);
        }

        public ProductDto Edit(string group, ProductDto productDto, string description)
        {
            return _productRepository.Edit(group, productDto, description);
        }

        public string Remove(string group, ProductDto productDto)
        {
            return _productRepository.Remove(group, productDto);
        }

        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            return _productRepository.UpdateChecked(group, productDto, isChecked);
        }
    }
}

