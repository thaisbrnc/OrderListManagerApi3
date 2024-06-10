using System;
using OrderListManagerApi3.Infrastructure.Repository;
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

        public GroupDto Add(string name)
        {
            return _groupRepository.Add(name);
        }

        public GroupDto Edit(string group, string description)
        {
            return _groupRepository.Edit(group, description);
        }

        public string Remove(GroupDto groupDto)
        {
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
    }
}