using CRM_system_for_training_centers.Models.AddmissionOfStudents;
using CRM_system_for_training_centers.Models.Attendance;
using CRM_system_for_training_centers.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Interfaces;

public interface IAttendanceService
{
    ValueTask<AttendanceViewMoodel> CreateAsync(AttendanceCreationModel attendance);

    ValueTask<AttendanceViewMoodel> GetByIdAsync(long courseId, DateTime date);

    ValueTask<IEnumerable<AttendanceViewMoodel>> GetAllAsync(long courseId);
}
