using CRM_system_for_training_centers.Enums;
using CRM_system_for_training_centers.Models.Users;

namespace CRM_system_for_training_centers.Models.Attendance;

public class AttendanceViewMoodel
{
    public List<User> Student { get; set; }

    public long CourseId { get; set; }

    public DateTime Day { get; set; }

    public AttendanceStatus Status { get; set; }
}
