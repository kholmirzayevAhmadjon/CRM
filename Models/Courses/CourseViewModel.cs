using CRM_system_for_training_centers.Enums;

namespace CRM_system_for_training_centers.Models.Course;

public class CourseViewModel
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Prise { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public string Duration { get; set; }

    public long Teacher_id { get; set; }

    public List<Weekdays> Weekdays { get; set; }
}
