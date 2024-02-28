using CRM_system_for_training_centers.Extensions;
using CRM_system_for_training_centers.Helps;
using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Services;

public class PaymentService : IPaymentService
{
    private List<Payment> payments;

    public async ValueTask<PaymentViewModel> CreateAsync(PaymentCreationModel payment)
    {
        payments = await FileIO.ReadAsync<Payment>(Constants.Payments_Path);

        var result = payments.Create(payment.MapTo<Payment>());
        await FileIO.WriteAsync(Constants.Payments_Path, payments);

        return result.MapTo<PaymentViewModel>();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        payments = await FileIO.ReadAsync<Payment>(Constants.Payments_Path);
        var existPayment = payments.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This payment is not found with ID = {id}");

        existPayment.IsDeleted = true;
        existPayment.DeletedAt = DateTime.UtcNow;
        await FileIO.WriteAsync(Constants.Payments_Path, payments);
        return true;
    }

    public async ValueTask<IEnumerable<PaymentViewModel>> GetAllAsync(long? studentId)
    {
        payments = await FileIO.ReadAsync<Payment>(Constants.Payments_Path);

        if (studentId == null)
        {
            return payments.Where(p => !p.IsDeleted).ToList().MapTo<PaymentViewModel>();
        }
        else
        {
            return payments.Where(p => !p.IsDeleted && p.StudentId == studentId).ToList().MapTo<PaymentViewModel>();
        }
    }

    public async ValueTask<PaymentViewModel> UpdateAsync(long id, PaymentUpdateModel payment)
    {
        payments = await FileIO.ReadAsync<Payment>(Constants.Payments_Path);
        var existPayment = payments.FirstOrDefault(x => x.Id == id && !x.IsDeleted)
            ?? throw new Exception($"This payment is not found with ID = {id}");

        existPayment.StudentId = payment.StudentId;
        existPayment.CourseId = payment.CourseId;
        existPayment.Amount = payment.Amount;
        existPayment.UpdatedAt  = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.Payments_Path, payments);

        return existPayment.MapTo<PaymentViewModel>();
    }
}
