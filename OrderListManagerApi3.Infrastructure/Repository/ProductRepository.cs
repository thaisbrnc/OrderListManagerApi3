using System;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;
using System.Text.Json;
using System.Xml.Linq;

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
            try
            {
                if (!string.IsNullOrEmpty(productName))
                {
                    var group = _groups.FirstOrDefault(g => g.Description.ToLower() == groupName.ToLower());

                    if (group != null)
                    {
                        var prod = group.products.FirstOrDefault(p => p.Description.ToLower() == productName.ToLower());

                        if (prod == null)
                        {
                            int index = _groups.IndexOf(group);
                            var productDto = new ProductDto() { Name = productName, IsChecked = false };
                            _groups[index].products.Add(_translator.ToEntity(productDto));

                            _jsonGenerator.Serialize(_groups);

                            return _translator.ToDto(group.products.Last());
                        }
                        else
                        {
                            throw new Exception($"Produto '{prod.Description}' já consta na lista.");
                        }
                    }
                    else
                        throw new ArgumentNullException($"Não foi possível cadastrar o produto, pois o grupo '{groupName}' não está registrado.", innerException: null);
                }
                else
                    throw new ArgumentNullException("Nome do produto não pode ser vazio.", innerException: null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public ProductDto Edit(string group, ProductDto productDto, string description)
        {
            try
            {
                if (!string.IsNullOrEmpty(description))
                {
                    var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

                    var searchDescription = gr.products.FirstOrDefault(p => p.Description.ToLower() == description.ToLower());

                    if (searchDescription == null)
                    {
                        var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

                        int index = gr.products.IndexOf(product);

                        gr.products[index].Description = description;

                        _jsonGenerator.Serialize(_groups);

                        return _translator.ToDto(gr.products[index]);
                    }
                    else
                        throw new Exception($"Produto '{searchDescription.Description}' já consta na lista.");
                }
                else
                    throw new ArgumentNullException("Nome do produto não pode ser vazio.", innerException: null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            try
            {
                var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

                var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

                int index = gr.products.IndexOf(product);

                gr.products[index].IsChecked = isChecked;

                _jsonGenerator.Serialize(_groups);

                return _translator.ToDto(gr.products[index]);
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public string Remove(string group, ProductDto productDto)
        {
            try
            {
                var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

                var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

                gr.products.Remove(product);

                _jsonGenerator.Serialize(_groups);

                return "Produto removido com sucesso.";
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }
    }
}