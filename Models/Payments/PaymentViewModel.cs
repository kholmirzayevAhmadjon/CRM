namespace CRM_system_for_training_centers.Models.Payment;

public class PaymentViewModel
{
    public long Id { get; set; }

    public long StudentId { get; set; }

    public long CourseId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }
}
