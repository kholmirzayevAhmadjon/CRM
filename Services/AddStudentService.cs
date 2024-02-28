using CRM_system_for_training_centers.Enums;
using CRM_system_for_training_centers.Extensions;
using CRM_system_for_training_centers.Helps;
using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.AddStudentToCourse;
using CRM_system_for_training_centers.Models.Course;

namespace CRM_system_for_training_centers.Services;

public class AddStudentService : IAddStusentService
{
    private readonly UserService _userService;
    private readonly CourseService _courseService;
    private List<AddStudent> addStudents;

    public AddStudentService(UserService userService, CourseService courseService)
    {
        this._userService = userService;
        this._courseService = courseService;
    }
    public async ValueTask<AddStudentViewModel> CreateAsync(AddStudentCreationModel addStudent)
    {
        addStudents = await FileIO.ReadAsync<AddStudent>(Constants.AddStudentsToCourse_Path);
        var exe = addStudents.FirstOrDefault(a => a.CourseId == addStudent.CourseId && a.StudentId == addStudent.StudentId);
        if (exe != null)
        {
           throw new Exception($"There is a student with this StudentId = {addStudent.StudentId}");
        }

        var res = await _courseService.GetByIdAsync(addStudent.CourseId);
        var exisUser = await _userService.GetByIdAsync(addStudent.StudentId);

        if (exisUser.Role == Role.Student)
        {
            var result = addStudents.Create(addStudent.MapTo<AddStudent>());
            await FileIO.WriteAsync(Constants.AddStudentsToCourse_Path, addStudents);
            return result.MapTo<AddStudentViewModel>();
        }

        else
        {
            throw new Exception($"This Students is not found with StudentId = {addStudent.StudentId}");
        }
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        addStudents = await FileIO.ReadAsync<AddStudent>(Constants.AddStudentsToCourse_Path);
        var exisAddStudent = addStudents.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This AddStuntToCourse is not found with Id = {id}");

        exisAddStudent.IsDeleted = true;
        exisAddStudent.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.AddStudentsToCourse_Path, addStudents);
        return true;
    }

    public async ValueTask<IEnumerable<AddStudentViewModel>> GetAllAsync(long courseId)
    {
        addStudents = await FileIO.ReadAsync<AddStudent>(Constants.AddStudentsToCourse_Path);
        return addStudents.Where(a => !a.IsDeleted && a.CourseId == courseId).ToList().MapTo<AddStudentViewModel>();
    }

    public async ValueTask<AddStudentViewModel> UpdateAsync(long id, AddStudentUpdateModel addStudent)
    {
        addStudents = await FileIO.ReadAsync<AddStudent>(Constants.AddStudentsToCourse_Path);
        var exisAddStudent = addStudents.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This AddStuntToCourse is not found with Id = {id}");
        var exe = addStudents.FirstOrDefault(a => a.CourseId == addStudent.CourseId && a.StudentId == addStudent.StudentId);
        if (exe != null)
        {
            throw new Exception($"There is a student with this StudentId = {addStudent.StudentId}");
        }

        exisAddStudent.Id = id;
        exisAddStudent.CourseId = addStudent.CourseId;
        exisAddStudent.StudentId = addStudent.StudentId;
        exisAddStudent.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.AddStudentsToCourse_Path, addStudents);
        return exisAddStudent.MapTo<AddStudentViewModel>();
    }
}
