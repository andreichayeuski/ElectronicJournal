using System.Collections.Generic;
using AutoMapper;
using EJ.Entities.Models;
using EJ.Models.Interfaces;
using EJ.Models.UI;

namespace EJ.Domain.Services
{
    public interface ICourseService
    {
        IEnumerable<CourseUi> GetCourses();
        CourseUi GetCourse(int id);
        CourseUi AddCourse(CourseUi course);
        bool DeleteCourse(int id);
        CourseUi UpdateCourse(int id, CourseUi course);
    }
    public class CourseService : ICourseService
    {
        protected readonly IMapper Mapper;
        protected readonly IRepository<Course> CourseRepository;

        public CourseService(IMapper mapper, IRepository<Course> repository)
        {
            Mapper = mapper;
            CourseRepository = repository;
        }

        public CourseUi AddCourse(CourseUi course)
        {
            var courseToRepository = new Course
            {
                Number = course.Number,
                EndDate = course.EndDate,
                StartDate = course.StartDate
            };
            return Mapper.Map<CourseUi>(CourseRepository.Add(courseToRepository));
        }

        public bool DeleteCourse(int id)
        {
            var course = CourseRepository.Find(id);
            if (course != null)
            {
                CourseRepository.Remove(course);
                return (CourseRepository.Find(id) == null);
            }
            throw new System.Exception("Course not found");
        }

        public CourseUi GetCourse(int id)
        {
            var course = CourseRepository.Find(id);
            if (course != null) 
            {
                return Mapper.Map<CourseUi>(course);
            }
            else
            {
                return new CourseUi();
            }
        }

        public IEnumerable<CourseUi> GetCourses()
        {
            return Mapper.Map<IEnumerable<CourseUi>>(CourseRepository.GetAll());
        }

        public CourseUi UpdateCourse(int id, CourseUi course)
        {
            var courseToRepository = new Course
            {
                Number = course.Number,
                EndDate = course.EndDate,
                StartDate = course.StartDate,
                Id = id
            };
            return Mapper.Map<CourseUi>(CourseRepository.Update(courseToRepository));
        }
    }
}
