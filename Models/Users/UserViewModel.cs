using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_system_for_training_centers.Enums;

namespace CRM_system_for_training_centers.Models.Users;

public class UserViewModel
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public Role Role { get; set; }
}