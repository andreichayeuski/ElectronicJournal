using System.Collections.Generic;
using AutoMapper;
using EJ.Domain.Services.DbContextScopeFactory;
using EJ.Entities;
using EJ.Entities.Models;
using EJ.Models.Interfaces;
using EJ.Models.UI;

namespace EJ.Domain.Services
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModel> GetCourses();
        CourseViewModel GetCourse(int id);
        CourseViewModel AddCourse(CourseViewModel course);
        bool DeleteCourse(int id);
        CourseViewModel UpdateCourse(int id, CourseViewModel course);
    }
    public class CourseService : ICourseService
    {
        protected readonly IMapper Mapper;
        private readonly EJContext _eJContext;

        public CourseService(IMapper mapper,
             IDbContextFactory contextFactory)
        {
            Mapper = mapper;
            _eJContext = contextFactory.CreateReadonlyDbContext<EJContext>();
        }

        public CourseViewModel AddCourse(CourseViewModel course)
        {
            var courseToRepository = new Course
            {
                Number = course.Number,
                EndDate = course.EndDate,
                StartDate = course.StartDate
            };
            var courseAdded = Mapper.Map<CourseViewModel>(_eJContext.Courses.Add(courseToRepository));
            _eJContext.SaveChanges();
            return courseAdded;
        }

        public bool DeleteCourse(int id)
        {
            var course = _eJContext.Courses.Find(id);
            if (course != null)
            {
                _eJContext.Courses.Remove(course);
                _eJContext.SaveChanges();
                return _eJContext.Courses.Find(id) == null;
            }
            throw new System.Exception("Course not found");
        }

        public CourseViewModel GetCourse(int id)
        {
            var course = _eJContext.Courses.Find(id);
            if (course != null) 
            {
                return Mapper.Map<CourseViewModel>(course);
            }
            else
            {
                return new CourseViewModel();
            }
        }

        public IEnumerable<CourseViewModel> GetCourses()
        {
            return Mapper.Map<IEnumerable<CourseViewModel>>(_eJContext.Courses);
        }

        public CourseViewModel UpdateCourse(int id, CourseViewModel course)
        {
            var courseToRepository = new Course
            {
                Number = course.Number,
                EndDate = course.EndDate,
                StartDate = course.StartDate,
                Id = id
            };
            var courseUpdated = Mapper.Map<CourseViewModel>(_eJContext.Courses.Update(courseToRepository));
            _eJContext.SaveChanges();
            return courseUpdated;
        }
    }
}
