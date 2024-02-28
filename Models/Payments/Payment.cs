using CRM_system_for_training_centers.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Models.Payment;

public class Payment : Auditable
{
    public long StudentId { get; set; }

    public long CourseId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }
}
