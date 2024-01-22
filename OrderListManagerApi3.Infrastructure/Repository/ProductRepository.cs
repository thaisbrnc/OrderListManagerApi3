using System;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;
using System.Text.Json;

namespace OrderListManagerApi3.Infrastructure.Repository
{
	public class ProductRepository
	{
        private readonly ITranslator<Product, ProductDto> _translator;
        private GroupRepository _groupRepository;
        private ITranslator<Group, GroupDto> _translatorGroup;

        public ProductRepository(ITranslator<Product, ProductDto> translator)
        {
            _translator = translator;
        }

        public string Add(ProductDto productDto, string groupDto)
        {
            _translatorGroup = new GroupDto();

            var group = Database.groups.FirstOrDefault(p => p.Description.ToLower() == groupDto.ToLower());

            if (group is not null)
            {
                var prod = group.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

                if (prod is not null)
                {
                    return "Produto '" + prod.Description + "' já consta na lista.";
                }
                else
                {
                    int index = Database.groups.IndexOf(group);
                    Database.groups[index].products.Add(_translator.ToEntity(productDto));

                    Database.Serialize();

                    return "Produto '" + group.products.Last().Description + "' cadastrado com sucesso.";
                    
                }
            }
            else
            {
                return "Não foi possível cadastrar o produto, pois o grupo '" + groupDto + "' não está registrado.";
            }
        }

        public ProductDto Edit(string group, ProductDto productDto, string description)
        {
            var gr = Database.groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            int index = gr.products.IndexOf(product);

            gr.products[index].Description = description;

            Database.Serialize();

            return _translator.ToDto(gr.products[index]);
        }

        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        { 
            var gr = Database.groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            int index = gr.products.IndexOf(product);

            gr.products[index].IsChecked = isChecked;

            Database.Serialize();

            return _translator.ToDto(gr.products[index]);
            
        }

        public string Remove(string group, ProductDto productDto)
        {
            var gr = Database.groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            if (gr is not null && product is not null)
            {
                gr.products.Remove(product);

                Database.Serialize();

                return "Produto '" + product.Description + "' removido com sucesso.";
            }
            else
            {
                return "Não foi possível remover o produto '" + product.Description + "'.";
            }
        }

        //retorna todos os produtos, sem separaçao por grupo
        public List<ProductDto> Get()
        {
            var productDtoList = new List<ProductDto>();

            _groupRepository = new GroupRepository(new GroupDto());

            var groupsDto = _groupRepository.Get();

            foreach (GroupDto g in groupsDto)
            {
                foreach (ProductDto p in g.Products)
                {
                    productDtoList.Add(p);
                }
            }
            return productDtoList;
        }
    }
}

