﻿using System;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;
using System.Text.Json;

namespace OrderListManagerApi3.Infrastructure.Repository
{
	public class ProductRepository
	{
        private readonly ITranslator<Product, ProductDto> _translator;
        private readonly IJsonLocalFileGenerator _fileJsonGenerator;
        private readonly List<Group> _groups;
        private GroupRepository _groupRepository;
        private ITranslator<Group, GroupDto> _translatorGroup;

        public ProductRepository(ITranslator<Product, ProductDto> translator, IJsonLocalFileGenerator fileJsonGenerator, List<Group> groups)
        {
            _translator = translator;
            _fileJsonGenerator = fileJsonGenerator;
            _groups = groups;
        }

        public string Add(string productName, string groupName)
        {
            _translatorGroup = new GroupDto();

            _fileJsonGenerator.Deserialize();

            var group = _groups.FirstOrDefault(g => g.Description.ToLower() == groupName.ToLower());

            if (group is not null)
            {
                var prod = group.products.FirstOrDefault(p => p.Description.ToLower() == productName.ToLower());

                if (prod is not null)
                {
                    return "Produto '" + prod.Description + "' já consta na lista.";
                }
                else
                {
                    int index = _groups.IndexOf(group);
                    ProductDto productDto = new ProductDto() { Name = productName, IsChecked = false };
                    _groups[index].products.Add(_translator.ToEntity(productDto));

                    _fileJsonGenerator.Serialize();

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
            _fileJsonGenerator.Deserialize();

            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            int index = gr.products.IndexOf(product);

            gr.products[index].Description = description;

            _fileJsonGenerator.Serialize();

            return _translator.ToDto(gr.products[index]);
        }

        public ProductDto UpdateChecked(string group, ProductDto productDto, bool isChecked)
        {
            _fileJsonGenerator.Deserialize();

            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            int index = gr.products.IndexOf(product);

            gr.products[index].IsChecked = isChecked;

            _fileJsonGenerator.Serialize();

            return _translator.ToDto(gr.products[index]);
            
        }

        public string Remove(string group, ProductDto productDto)
        {
            _fileJsonGenerator.Deserialize();

            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            var product = gr.products.FirstOrDefault(p => p.Description.ToLower() == productDto.Name.ToLower());

            if (gr is not null && product is not null)
            {
                gr.products.Remove(product);

                _fileJsonGenerator.Serialize();

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

            _groupRepository = new GroupRepository(new GroupDto(), _fileJsonGenerator, _groups);

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

