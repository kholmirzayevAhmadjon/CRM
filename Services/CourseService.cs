using CRM_system_for_training_centers.Extensions;
using CRM_system_for_training_centers.Helps;
using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.Course;

namespace CRM_system_for_training_centers.Services;

public class CourseService : ICourseService
{
    private readonly UserService _userService;
    private List<Course> courses;

    public CourseService(UserService userService)
    {
        _userService = userService;
    }
    public async ValueTask<CourseViewModel> CreateAsync(CourseCreationModel course)
    {
        courses = await FileIO.ReadAsync<Course>(Constants.Course_Path);

        var exisUser = await _userService.GetByIdAsync(course.Teacher_id);

        if (exisUser.Role == Enums.Role.Teacher)
        {
            var result = courses.Create(course.MapTo<Course>());
            await FileIO.WriteAsync(Constants.Course_Path, courses);
            return result.MapTo<CourseViewModel>();
        }

        else
            throw new Exception($"This Teacher is not found with ID = {course.Teacher_id}");
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        courses = await FileIO.ReadAsync<Course>(Constants.Course_Path);
        var existCourse = courses.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This course is not found with ID = {id}");

        existCourse.IsDeleted = true;
        existCourse.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.Course_Path, courses);
        return true;
    }

    public async ValueTask<IEnumerable<CourseViewModel>> GetAllAsync()
    {
        courses = await FileIO.ReadAsync<Course>(Constants.Course_Path);
        return courses.Where(u => !u.IsDeleted).ToList().MapTo<CourseViewModel>();
    }

    public async ValueTask<CourseViewModel> GetByIdAsync(long id)
    {
        courses = await FileIO.ReadAsync<Course>(Constants.Course_Path);
        var existCourse = courses.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This course is not found with ID = {id}");

        return existCourse.MapTo<CourseViewModel>();
    }

    public async ValueTask<CourseViewModel> UpdateAsync(long id, CourseUpdateModel course)
    {
        courses = await FileIO.ReadAsync<Course>(Constants.Course_Path);
        var existCourse = courses.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"This course is not found with ID = {id}");

        existCourse.Id = id;
        existCourse.Weekdays = course.Weekdays;
        existCourse.Name = course.Name;
        existCourse.Description = course.Description;
        existCourse.Prise = course.Prise;
        existCourse.StartTime = course.StartTime;
        existCourse.UpdatedAt = DateTime.UtcNow;
        existCourse.EndTime = course.EndTime;
        existCourse.Duration = course.Duration;
        existCourse.Teacher_id = course.Teacher_id;

        await FileIO.WriteAsync(Constants.Course_Path, courses);

        return existCourse.MapTo<CourseViewModel>();
    }
}
