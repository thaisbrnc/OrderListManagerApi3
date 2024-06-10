using OrderListManagerApi3.Models;

namespace OrderListManagerApi3.Translate;

public class GroupDto : ITranslator<Group, GroupDto>
{
    public string Description { get; set; }
    public List<ProductDto> Products { get; set; }

    public GroupDto()
    {
        Products = new();
    }

    public GroupDto ToDto(Group group)
    {
        try
        {
            var list = new List<ProductDto>();
            var translator = new ProductDto();

            foreach (Product p in group.products)
            {
                list.Add(translator.ToDto(p));
            }

            var groupDto = new GroupDto
            {
                Description = group.Description,
                Products = list
            };

            return groupDto;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public Group ToEntity(GroupDto groupDto)
    {
        try
        {
            var list = new List<Product>();
            var translator = new ProductDto();

            foreach (ProductDto p in groupDto.Products)
            {
                list.Add(translator.ToEntity(p));
            }

            var group = new Group
            {
                Description = groupDto.Description,
                products = list
            };

            return group;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }
}