using CRM_system_for_training_centers.Enums;
using CRM_system_for_training_centers.Models.Commons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Models.Users;

public class User : Auditable
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }
}
