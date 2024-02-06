using System;
using OrderListManagerApi3.Infrastructure.Repository;
using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Services
{
	public class GroupService
	{
        private readonly GroupRepository _groupRepository;

        public GroupService(GroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public string Add(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "Dados inválidos.";

            return _groupRepository.Add(name);
        }

        public GroupDto Edit(string group, string description)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(group))
                throw new Exception("Dados inválidos.");

            return _groupRepository.Edit(group, description);
        }

        public string Remove(GroupDto groupDto)
        {
            if (groupDto is null)
                throw new Exception("Dados inválidos.");

            return _groupRepository.Remove(groupDto);
        }

        public List<GroupDto> Get()
        {
            return _groupRepository.Get();
        }

        public GroupDto GetByDescription(string description)
        {
            return _groupRepository.GetByDescription(description);
        }

        /*public List<GroupDto> GetProducts()
        {
            var lista = new List<GroupDto>();

            string products = "";

            foreach (GroupDto g in _groupRepository.Get())
            {
                products += "Grupo " + g.Description + ": \n";

                foreach (ProductDto p in g.Products)
                {
                    products += p.IsChecked + " " + p.Name + "\n";
                }

                //lista.Groups.Add(group);
            }

            return lista;



            /*var lista = new ListDto();

            string products = "";

            foreach (GroupDto g in _groupRepository.Get())
            {
                var group = new GroupDto
                {
                    Description = g.Description
                };

                products += "Grupo " + g.Description + ": \n";

                group.products = new List<ProductDto>();

                foreach (ProductDto p in g.products) {
                    var product = new ProductDto()
                    {
                        IsChecked = p.IsChecked,
                        Name = p.Name
                    };

                    group.products.Add(product);

                    products += p.IsChecked + " " + p.Name + "\n";
                }

                lista.Groups.Add(group);
            }
            
            return lista;
            
        }*/
    }
}

