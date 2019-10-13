using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EJ.Domain.Services.DbContextScopeFactory;
using EJ.Entities;
using EJ.Entities.Models;
using EJ.Models.Interfaces;
using EJ.Models.UI;

namespace EJ.Domain.Services
{
    public interface IGroupService
    {
        IEnumerable<GroupViewModel> GetGroups();
        GroupViewModel GetGroup(int id);
        GroupViewModel AddGroup(GroupViewModel group);
        bool DeleteGroup(int id);
        GroupViewModel UpdateGroup(int id, GroupViewModel group);
    }
    public class GroupService : IGroupService
    {
        protected readonly IMapper Mapper;
        private readonly EJContext _eJContext;

        public GroupService(IMapper mapper,
             IDbContextFactory contextFactory)
        {
            Mapper = mapper;
            _eJContext = contextFactory.CreateReadonlyDbContext<EJContext>();
        }

        public GroupViewModel AddGroup(GroupViewModel group)
        {
            var groupToRepository = new Group
            {
                Number = group.Number,
                EndDate = group.EndDate,
                StartDate = group.StartDate,
                HalfGroup = group.HalfGroup,
                CourseId = group.CourseId
            };
            var groupAdded = Mapper.Map<GroupViewModel>(_eJContext.Groups.Add(groupToRepository));
            _eJContext.SaveChanges();
            return groupAdded;
        }

        public bool DeleteGroup(int id)
        {
            var group = _eJContext.Groups.Find(id);
            if (group != null)
            {
                _eJContext.Groups.Remove(group);
                _eJContext.SaveChanges();
                return _eJContext.Groups.Find(id) == null;
            }
            throw new System.Exception("Group not found");
        }

        public GroupViewModel GetGroup(int id)
        {
            var groupFromRepository = _eJContext.Groups.Find(id);
            if (groupFromRepository != null)
            {
                return new GroupViewModel
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
                return new GroupViewModel();
            }
        }

        public IEnumerable<GroupViewModel> GetGroups()
        {
            return Mapper.Map<IEnumerable<GroupViewModel>>(_eJContext.Groups
                .OrderBy(x => x.Number).ThenBy(y => y.HalfGroup));
        }

        public GroupViewModel UpdateGroup(int id, GroupViewModel group)
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
            var groupUpdated = Mapper.Map<GroupViewModel>(_eJContext.Groups.Update(groupToRepository));
            _eJContext.SaveChanges();
            return groupUpdated;
        }
    }
}
