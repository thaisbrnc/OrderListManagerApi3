using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Repository
{
	public class GroupRepository
	{
		private readonly ITranslator<Group, GroupDto> _translator;
        private readonly IJsonLocalFileGenerator _fileJson;
        private List<Group> _groups;
		public GroupRepository(ITranslator<Group, GroupDto> translator, IJsonLocalFileGenerator fileJsonGenerator, List<Group> groups)
		{
			_translator = translator;
            _fileJson = fileJsonGenerator;
            _groups = groups;
		}

		public string Add(string name)
		{
            _fileJson.Deserialize();

            var gr = _groups.FirstOrDefault(p => p.Description.ToLower() == name.ToLower());

            if (gr is not null)
                return "Grupo '" + gr.Description + "' já consta na lista.";
            else
            {
                var group = new GroupDto()
                {
                    Description = name,
                    Products = new List<ProductDto>()
                };

                _groups.Add(_translator.ToEntity(group));

                _fileJson.Serialize();

                return "Grupo '" + group.Description + "' cadastrado com sucesso.";
            }
        }

        public GroupDto Edit(string group, string description)
        {
            _fileJson.Deserialize();

            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

            int index = _groups.IndexOf(gr);

            _groups[index].Description = description;

            _fileJson.Serialize();

            return _translator.ToDto(_groups[index]);
        }

        public string Remove(GroupDto groupDto)
        {
            _fileJson.Deserialize();

            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == groupDto.Description.ToLower());

            if (gr is not null)
            {
                _groups.Remove(gr);

                _fileJson.Serialize();

                return "Grupo '" + gr.Description + "' removido com sucesso.";
            }
            else
            {
                return "Não foi possível remover o grupo '" + gr.Description + "'.";
            }
        }

        public List<GroupDto> Get()
        {
            _fileJson.Deserialize();

            var groupsDto = new List<GroupDto>();

            foreach (Group g in _groups)
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

            _fileJson.Deserialize();

            var groupDto = _translator.ToDto(_groups.FirstOrDefault(g => g.Description.ToLower() == description.ToLower()));

            return groupDto;
        }
    }
}

