using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EJ.Entities.Models;
using EJ.Models.Interfaces;
using EJ.Models.UI;

namespace EJ.Domain.Services
{
    public interface IGroupService
    {
        IEnumerable<GroupUi> GetGroups();
        GroupUi GetGroup(int id);
        GroupUi AddGroup(GroupUi group);
        bool DeleteGroup(int id);
        GroupUi UpdateGroup(int id, GroupUi group);
    }
    public class GroupService : IGroupService
    {
        protected readonly IMapper Mapper;
        protected readonly IRepository<Group> GroupRepository;

        public GroupService(IMapper mapper, IRepository<Group> repository)
        {
            Mapper = mapper;
            GroupRepository = repository;
        }

        public GroupUi AddGroup(GroupUi group)
        {
            var groupToRepository = new Group
            {
                Number = group.Number,
                EndDate = group.EndDate,
                StartDate = group.StartDate,
                HalfGroup = group.HalfGroup,
                CourseId = group.CourseId
            };
            return Mapper.Map<GroupUi>(GroupRepository.Add(groupToRepository));
        }

        public bool DeleteGroup(int id)
        {
            var group = GroupRepository.Find(id);
            if (group != null)
            {
                GroupRepository.Remove(group);
                return (GroupRepository.Find(id) == null);
            }
            throw new System.Exception("Group not found");
        }

        public GroupUi GetGroup(int id)
        {
            var groupFromRepository = GroupRepository.Find(id);
            if (groupFromRepository != null)
            {
                return new GroupUi
                {
                    CourseId = groupFromRepository.CourseId ?? 0,
                    StartDate = groupFromRepository.StartDate,
                    EndDate = groupFromRepository.EndDate,
                    HalfGroup = groupFromRepository.HalfGroup,
                    Id = groupFromRepository.Id,
                    Number = groupFromRepository.Number,
                };
            }
            else
            {
                return new GroupUi();
            }
        }

        public IEnumerable<GroupUi> GetGroups()
        {
            return Mapper.Map<IEnumerable<GroupUi>>(GroupRepository.GetAllReadOnly()
                .OrderBy(x => x.Number).ThenBy(y => y.HalfGroup));
        }

        public GroupUi UpdateGroup(int id, GroupUi group)
        {
            var groupToRepository = new Group
            {
                Number = group.Number,
                EndDate = group.EndDate,
                StartDate = group.StartDate,
                HalfGroup = group.HalfGroup,
                CourseId = group.CourseId,
                Id = id
            };
            return Mapper.Map<GroupUi>(GroupRepository.Update(groupToRepository));
        }
    }
}
