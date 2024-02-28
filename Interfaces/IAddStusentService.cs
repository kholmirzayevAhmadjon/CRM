using CRM_system_for_training_centers.Models.AddStudentToCourse;

namespace CRM_system_for_training_centers.Interfaces;

public interface IAddStusentService 
{
    ValueTask<AddStudentViewModel> CreateAsync(AddStudentCreationModel addStudent);

    ValueTask<AddStudentViewModel> UpdateAsync(long id, AddStudentUpdateModel addStudent);

    ValueTask<bool> DeleteAsync(long id);

    ValueTask<IEnumerable<AddStudentViewModel>> GetAllAsync(long courseId);
}
