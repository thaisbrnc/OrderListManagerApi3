using System.Xml.Linq;
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
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var gr = _groups.FirstOrDefault(p => p.Description?.ToLower() == name.ToLower());

                    if (gr == null)
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
                    else
                    {
                        throw new Exception($"Grupo '{gr.Description}' já consta na lista.");
                    }
                }
                else
                    throw new ArgumentNullException("Nome do grupo não pode ser vazio.", innerException: null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public GroupDto Edit(string group, string description)
        {
            try
            {
                if (!string.IsNullOrEmpty(description))
                {
                    var searchDescription = _groups.FirstOrDefault(g => g.Description.ToLower() == description.ToLower());

                    if (searchDescription == null)
                    {
                        var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == group.ToLower());

                        int index = _groups.IndexOf(gr);

                        _groups[index].Description = description;

                        _jsonGenerator.Serialize(_groups);

                        return _translator.ToDto(_groups[index]);
                    }  
                    else
                        throw new Exception($"Grupo '{searchDescription.Description}' já consta na lista.");
                }
                else
                    throw new ArgumentNullException("Nome do grupo não pode ser vazio.", innerException: null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public string Remove(GroupDto groupDto)
        {
            try
            {
                var gr = _groups.FirstOrDefault(g => g.Description.ToLower() == groupDto.Description.ToLower());

                _groups.Remove(gr);

                _jsonGenerator.Serialize(_groups);

                return "Grupo removido com sucesso.";
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro: Não foi possível remover o grupo.\n\r{ex.Message}");
            }
        }

        public List<GroupDto> Get()
        {
            try
            {
                var groupsDto = new List<GroupDto>();

                foreach (Group g in _groups)
                {
                    groupsDto.Add(_translator.ToDto(g));
                }

                return groupsDto;
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }

        public GroupDto GetByDescription(string description)
        {
            try
            {
                if (!string.IsNullOrEmpty(description))
                {
                    var groupDto = _translator.ToDto(_groups.FirstOrDefault(g => g.Description.ToLower() == description.ToLower()));

                    return groupDto;
                }
                else
                    throw new ArgumentNullException("Nome do grupo não pode ser vazio.", innerException: null);
            }
            catch(Exception ex)
            {
                throw new Exception($"Erro: {ex.Message}");
            }
        }
    }
}