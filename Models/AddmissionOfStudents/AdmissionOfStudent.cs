using CRM_system_for_training_centers.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Models.AddmissionOfStudents;

public class AdmissionOfStudent : Auditable
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Description { get; set; }
}
