using OrderListManagerApi3.Models;

namespace OrderListManagerApi3.Translate;

public class GroupDto : ITranslator<Group, GroupDto>
{
    public string Description { get; set; }
    public List<ProductDto> Products { get; set; }

    public GroupDto()
    {
        Products = new List<ProductDto>();
    }

    public GroupDto ToDto(Group group)
    {
        GroupDto _groupDto = null;

        if (group is not null)
        {
            var list = new List<ProductDto>();
            var translator = new ProductDto();

            foreach (Product p in group.products)
            {
                list.Add(translator.ToDto(p));
            }

            _groupDto = new GroupDto
            {
                Description = group.Description,
                Products = list
            };
        }

        return _groupDto;
    }

    public Group ToEntity(GroupDto groupDto)
    {
        Group _group = null;

        if (groupDto is not null)
        {
            var list = new List<Product>();
            var translator = new ProductDto();

            foreach (ProductDto p in groupDto.Products)
            {
                list.Add(translator.ToEntity(p));
            }

            _group = new Group
            {
                Description = groupDto.Description,
                products = list
            };
        }

        return _group;
    }
}

