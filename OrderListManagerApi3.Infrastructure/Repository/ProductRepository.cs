using System;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;
using System.Text.Json;

namespace OrderListManagerApi3.Infrastructure.Repository
{
	public class ProductRepository
	{
        private readonly ITranslator<Product, ProductDto> _translator;
        private readonly Database _database;
        private GroupRepository _groupRepository;
        private ITranslator<Group, GroupDto> _translatorGroup;

        public ProductRepository(ITranslator<Product, ProductDto> translator, Database database)
        {
            _translator = translator;
            _database = database;
        }

        public string Add(string productName, string groupName)
        {
            _translatorGroup = new GroupDto();

            _database.Deserialize();

            var group = _database.groups.FirstOrDefault(g => g.Description.ToLower() == groupName.ToLower());

            if (group is not null)
            {
                var prod = group.products.FirstOrDefault(p => p.Description.ToLower() == productName.ToLower());

                if (prod is not null)
                {
                    return "Produto '" + prod.Description + "' já consta na lista.";
                }
                else
                {
                    int index = _database.groups.IndexOf(group);
                    ProductDto productDto = new ProductDto() { Name = productName, IsChecked = false };
                    _database.groups[index].products.Add(_translator.ToEntity(productDto));

                    _database.Serialize();

                    return "Produto '" + group.products.Last().Description + "' cadastrado com sucesso.";
                    
                }
            }
            else
            {
                return "Não foi possível cadastrar o produto, pois o grupo '" + groupName + "' não está registrado.";
            }
        }

        public ProductDto Edit(string group, ProductDto productDto, string description)
        {
            _database.Deserialize();

            var gr = _database.groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            int index = gr.products.IndexOf(product);

            gr.products[index].Description = description;

            _database.Serialize();

            return _translator.ToDto(gr.products[index]);
        }

        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            _database.Deserialize();

            var gr = _database.groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            int index = gr.products.IndexOf(product);

            gr.products[index].IsChecked = isChecked;

            _database.Serialize();

            return _translator.ToDto(gr.products[index]);
            
        }

        public string Remove(string group, ProductDto productDto)
        {
            _database.Deserialize();

            var gr = _database.groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            if (gr is not null && product is not null)
            {
                gr.products.Remove(product);

                _database.Serialize();

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

            _groupRepository = new GroupRepository(new GroupDto(), _database);

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

