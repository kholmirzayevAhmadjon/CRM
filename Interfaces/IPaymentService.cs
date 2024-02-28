using CRM_system_for_training_centers.Models.Course;
using CRM_system_for_training_centers.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Interfaces;

public interface IPaymentService
{
    ValueTask<PaymentViewModel> CreateAsync(PaymentCreationModel payment);

    ValueTask<PaymentViewModel> UpdateAsync(long id, PaymentUpdateModel payment);

    ValueTask<bool> DeleteAsync(long id);

    ValueTask<IEnumerable<PaymentViewModel>> GetAllAsync(long? studentId);
}
