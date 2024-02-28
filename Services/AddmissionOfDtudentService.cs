using CRM_system_for_training_centers.Extensions;
using CRM_system_for_training_centers.Helps;
using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.AddmissionOfStudents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Services;

public class AdmissionOfStudentService : IAdmissionOfStudentsService
{
    private List<AdmissionOfStudent> admissions;

    public async ValueTask<AdmissionOfStudentsViewModel> CreateAsync(AdmissionOfStudentsCreationModel admission)
    {
        admissions = await FileIO.ReadAsync<AdmissionOfStudent>(Constants.AddmissionOfStudents_Path);
        var result = admissions.Create(admission.MapTo<AdmissionOfStudent>());
        await FileIO.WriteAsync(Constants.AddmissionOfStudents_Path, admissions);
        return result.MapTo<AdmissionOfStudentsViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        admissions = await FileIO.ReadAsync<AdmissionOfStudent>(Constants.AddmissionOfStudents_Path);
        var exis = admissions.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This AdmissionOfStudents is not found with ID = {id}");

        exis.IsDeleted = true;
        exis.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.AddmissionOfStudents_Path, admissions);
        return true;
    }

    public async ValueTask<IEnumerable<AdmissionOfStudentsViewModel>> GetAllAsync()
    {
        admissions = await FileIO.ReadAsync<AdmissionOfStudent>(Constants.AddmissionOfStudents_Path);
        return admissions.Where(x => !x.IsDeleted).ToList().MapTo<AdmissionOfStudentsViewModel>();
    }

    public async ValueTask<AdmissionOfStudentsViewModel> GetByIdAsync(long id)
    {
        admissions = await FileIO.ReadAsync<AdmissionOfStudent>(Constants.AddmissionOfStudents_Path);
        var exis = admissions.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This AdmissionOfStudents is not found with ID = {id}");

        return exis.MapTo<AdmissionOfStudentsViewModel>();
    }

    public async ValueTask<AdmissionOfStudentsViewModel> UpdateAsync(long id, AdmissionOfStudentsUpdateModel admission)
    {
        admissions = await FileIO.ReadAsync<AdmissionOfStudent>(Constants.AddmissionOfStudents_Path);
        var exis = admissions.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This AdmissionOfStudents is not found with ID = {id}");

        exis.FirstName = admission.FirstName;
        exis.LastName = admission.LastName;
        exis.PhoneNumber    = admission.PhoneNumber;
        exis.Email = admission.Email;   
        exis.Description = admission.Description;
        exis.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.AddmissionOfStudents_Path, admissions);
        return exis.MapTo<AdmissionOfStudentsViewModel>();
    }
}
