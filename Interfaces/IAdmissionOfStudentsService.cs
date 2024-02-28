using CRM_system_for_training_centers.Models.AddmissionOfStudents;

namespace CRM_system_for_training_centers.Interfaces;

public interface IAdmissionOfStudentsService
{
    ValueTask<AdmissionOfStudentsViewModel> CreateAsync(AdmissionOfStudentsCreationModel admission);

    ValueTask<AdmissionOfStudentsViewModel> UpdateAsync(long id, AdmissionOfStudentsUpdateModel admission);

    ValueTask<bool> DeleteAsync(long id);

    ValueTask<AdmissionOfStudentsViewModel> GetByIdAsync(long id);

    ValueTask<IEnumerable<AdmissionOfStudentsViewModel>> GetAllAsync();
}
