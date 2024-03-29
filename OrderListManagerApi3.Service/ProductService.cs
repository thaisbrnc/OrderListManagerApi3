﻿using System;
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

        public string Add(string productName, string groupName)
        {
            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(groupName))
                throw new Exception("Dados inválidos.");

            return _productRepository.Add(productName, groupName);
        }

        public ProductDto Edit(string group, ProductDto productDto, string description)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(group))
                throw new Exception("Dados inválidos.");

            return _productRepository.Edit(group, productDto, description);
        }

        public string Remove(string group, ProductDto productDto)
        {
            if (string.IsNullOrEmpty(group))
                throw new Exception("Dados inválidos.");

            return _productRepository.Remove(group, productDto);
        }

        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            if (string.IsNullOrEmpty(group))
                throw new Exception("Dados inválidos.");

            return _productRepository.UpdateChecked(group, productDto, isChecked);
        }
    }
}

