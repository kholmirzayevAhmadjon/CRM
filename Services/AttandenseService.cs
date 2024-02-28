using CRM_system_for_training_centers.Extensions;
using CRM_system_for_training_centers.Helps;
using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Services;

public class AttandenseService : IAttendanceService
{
    private List<Attendance> attendances;

    public async ValueTask<AttendanceViewMoodel> CreateAsync(AttendanceCreationModel attendance)
    {
        attendances = await FileIO.ReadAsync<Attendance>(Constants.Attandenses_Path);

        var exisAttendense = attendances.Create(attendance.MapTo<Attendance>());
        await FileIO.WriteAsync(Constants.Attandenses_Path, attendances);
        return exisAttendense.MapTo<AttendanceViewMoodel>();
    }

    public async ValueTask<IEnumerable<AttendanceViewMoodel>> GetAllAsync(long courseId)
    {
        attendances = await FileIO.ReadAsync<Attendance>(Constants.Attandenses_Path);
        return attendances.Where(a => a.IsDeleted && a.Id == courseId).ToList().MapTo<AttendanceViewMoodel>();
    }

    public async ValueTask<AttendanceViewMoodel> GetByIdAsync(long courseId, DateTime time)
    {
        attendances = await FileIO.ReadAsync<Attendance>(Constants.Attandenses_Path);
        var exisAttandance = attendances.FirstOrDefault(a => !a.IsDeleted && a.Id == courseId && a.Day == time)
            ?? throw new Exception($"This Attandance is not found with CourseId = {courseId}");

        return exisAttandance.MapTo<AttendanceViewMoodel>();
    }
}
