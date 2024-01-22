using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Repository
{
	public class GroupRepository
	{
		private readonly ITranslator<Group, GroupDto> _translator;

		public GroupRepository(ITranslator<Group, GroupDto> translator)
		{
			_translator = translator;
		}

		public string Add(string name)
		{
            var gr = Database.groups.FirstOrDefault(p => p.Description.ToLower() == name.ToLower());

            if (gr is not null)
                return "Grupo '" + gr.Description + "' já consta na lista.";
            else
            {
                var group = new GroupDto()
                {
                    Description = name,
                    Products = new List<ProductDto>()
                };

                Database.groups.Add(_translator.ToEntity(group));

                Database.Serialize();

                return "Grupo '" + group.Description + "' cadastrado com sucesso.";
            }
        }

        public GroupDto Edit(string group, string description)
        {
            var gr = Database.groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            int index = Database.groups.IndexOf(gr);

            Database.groups[index].Description = description;

            Database.Serialize();

            return _translator.ToDto(Database.groups[index]);
        }

        public List<GroupDto> Get()
        {
            Database.Deserialize();

            var groupsDto = new List<GroupDto>();

            foreach (Group g in Database.groups)
            {
                groupsDto.Add(_translator.ToDto(g));
            }

            return groupsDto;
        }

        public GroupDto GetByDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new Exception();
            }

            Database.Deserialize();

            var groupDto = _translator.ToDto(Database.groups.FirstOrDefault(g => g.Description.ToLower() == description.ToLower()));

            return groupDto;
        }
    }
}

