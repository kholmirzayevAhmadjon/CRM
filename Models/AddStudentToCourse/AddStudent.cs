using CRM_system_for_training_centers.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Models.AddStudentToCourse;

public class AddStudent : Auditable
{
    public long CourseId { get; set; }
    
    public long StudentId { get; set; }
}
