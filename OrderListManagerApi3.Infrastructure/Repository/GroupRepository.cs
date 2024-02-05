using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Repository
{
	public class GroupRepository
	{
		private readonly ITranslator<Group, GroupDto> _translator;
        private readonly Database _database;

		public GroupRepository(ITranslator<Group, GroupDto> translator, Database database)
		{
			_translator = translator;
            _database = database;
		}

		public string Add(string name)
		{
            var gr = _database.groups.FirstOrDefault(p => p.Description.ToLower() == name.ToLower());

            if (gr is not null)
                return "Grupo '" + gr.Description + "' já consta na lista.";
            else
            {
                var group = new GroupDto()
                {
                    Description = name,
                    Products = new List<ProductDto>()
                };

                _database.groups.Add(_translator.ToEntity(group));

                _database.Serialize();

                return "Grupo '" + group.Description + "' cadastrado com sucesso.";
            }
        }

        public GroupDto Edit(string group, string description)
        {
            var gr = _database.groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            int index = _database.groups.IndexOf(gr);

            _database.groups[index].Description = description;

            _database.Serialize();

            return _translator.ToDto(_database.groups[index]);
        }

        public List<GroupDto> Get()
        {
            _database.Deserialize();

            var groupsDto = new List<GroupDto>();

            foreach (Group g in _database.groups)
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

            _database.Deserialize();

            var groupDto = _translator.ToDto(_database.groups.FirstOrDefault(g => g.Description.ToLower() == description.ToLower()));

            return groupDto;
        }
    }
}

