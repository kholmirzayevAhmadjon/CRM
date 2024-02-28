using CRM_system_for_training_centers.Enums;
using CRM_system_for_training_centers.Models.Course;
using CRM_system_for_training_centers.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Interfaces;

public interface ICourseService
{
    ValueTask<CourseViewModel> CreateAsync (CourseCreationModel course );

    ValueTask<CourseViewModel> UpdateAsync(long id, CourseUpdateModel course);

    ValueTask<bool> DeleteAsync(long id);

    ValueTask<CourseViewModel> GetByIdAsync(long id);

    ValueTask<IEnumerable<CourseViewModel>> GetAllAsync();
}
