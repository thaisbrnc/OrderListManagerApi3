using System;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;
using System.Text.Json;

namespace OrderListManagerApi3.Infrastructure.Repository
{
	public class ProductRepository
	{
        private readonly ITranslator<Product, ProductDto> _translator;
        private readonly IJsonLocalFileGenerator _jsonGenerator;
        private readonly IList<Group> _groups;

        public ProductRepository(ITranslator<Product, ProductDto> translator, IJsonLocalFileGenerator jsonGenerator)
        {
            _translator = translator;
            _jsonGenerator = jsonGenerator;
            _groups = _jsonGenerator.Deserialize();
        }

        public ProductDto Add(string productName, string groupName)
        {
            var group = _groups.FirstOrDefault(g => g.Description.ToLower() == groupName.ToLower());

            if (group is not null)
            {
                var prod = group.products.FirstOrDefault(p => p.Description.ToLower() == productName.ToLower());

                if (prod is not null)
                {
                    throw new Exception("Produto '" + prod.Description + "' já consta na lista.");
                }
                else
                {
                    int index = _groups.IndexOf(group);
                    ProductDto productDto = new ProductDto() { Name = productName, IsChecked = false };
                    _groups[index].products.Add(_translator.ToEntity(productDto));

                    _jsonGenerator.Serialize(_groups);

                    return _translator.ToDto(group.products.Last());
                }
            }
            else
            {
                throw new Exception("Não foi possível cadastrar o produto, pois o grupo '" + groupName + "' não está registrado.");
            }
        }

        public ProductDto Edit(string group, ProductDto productDto, string description)
        {
            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var searchDescription = gr.products.FirstOrDefault(p => p.Description.ToLower() == description.ToLower());

            if (searchDescription is not null)
                throw new Exception("Produto '" + searchDescription + "' já consta na lista.");
            else
            {
                var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

                int index = gr.products.IndexOf(product);

                gr.products[index].Description = description;

                _jsonGenerator.Serialize(_groups);

                return _translator.ToDto(gr.products[index]);
            }
        }

        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            int index = gr.products.IndexOf(product);

            gr.products[index].IsChecked = isChecked;

            _jsonGenerator.Serialize(_groups);

            return _translator.ToDto(gr.products[index]);
        }

        public string Remove(string group, ProductDto productDto)
        {
            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            if (gr is not null && product is not null)
            {
                gr.products.Remove(product);

                _jsonGenerator.Serialize(_groups);

                return "Produto removido com sucesso.";
            }
            else
            {
                throw new Exception("Não foi possível remover o produto '" + product.Description + "'.");
            }
        }
    }
}