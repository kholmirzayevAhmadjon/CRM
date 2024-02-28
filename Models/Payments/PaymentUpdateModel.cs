namespace CRM_system_for_training_centers.Models.Payment;

public class PaymentUpdateModel
{
    public long StudentId { get; set; }

    public long CourseId { get; set; }

    public decimal Amount { get; set; }
}
