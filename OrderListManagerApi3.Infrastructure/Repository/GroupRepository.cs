using OrderListManagerApi3.Models;
using OrderListManagerApi3.Translate;

namespace OrderListManagerApi3.Infrastructure.Repository
{
	public class GroupRepository
	{
		private readonly ITranslator<Group, GroupDto> _translator;
        private readonly IJsonLocalFileGenerator _jsonGenerator;
        private IList<Group> _groups;

		public GroupRepository(ITranslator<Group, GroupDto> translator, IJsonLocalFileGenerator jsonGenerator)
		{
			_translator = translator;
            _jsonGenerator = jsonGenerator;
            _groups = _jsonGenerator.Deserialize();
        }

		public GroupDto Add(string name)
		{
            var gr = _groups.FirstOrDefault(p => p.Description.ToLower() == name.ToLower());

            if (gr is not null)
                throw new Exception("Grupo '" + gr.Description + "' já consta na lista.");
            else
            {
                var group = new GroupDto()
                {
                    Description = name,
                    Products = new List<ProductDto>()
                };

                _groups.Add(_translator.ToEntity(group));
                _jsonGenerator.Serialize(_groups);

                return group;
            }
        }

        public GroupDto Edit(string group, string description)
        {
            //_jsonGenerator.Deserialize();

            var searchDescription = _groups.FirstOrDefault(g => g.Description.ToLower() == description.ToLower());

            if (searchDescription is not null)
                throw new Exception("Grupo '" + searchDescription + "' já consta na lista.");
            else
            {
                var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

                int index = _groups.IndexOf(gr);

                _groups[index].Description = description;

                _jsonGenerator.Serialize(_groups);

                return _translator.ToDto(_groups[index]);
            }
        }

        public string Remove(GroupDto groupDto)
        {
            //_jsonGenerator.Deserialize();

            var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == groupDto.Description.ToLower());

            if (gr is not null)
            {
                _groups.Remove(gr);

                _jsonGenerator.Serialize(_groups);

                return "Grupo removido com sucesso.";
            }
            else
            {
                throw new Exception("Não foi possível remover o grupo '" + gr.Description + "'.");
            }
        }

        public List<GroupDto> Get()
        {
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

            //_jsonGenerator.Deserialize();

            var groupDto = _translator.ToDto(_groups.FirstOrDefault(g => g.Description.ToLower() == description.ToLower()));

            return groupDto;
        }
    }
}