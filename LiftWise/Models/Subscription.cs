namespace LiftWise.Models
{
    public class Subscription
    {
        public int idSubscription { get; set; }
        public int userId { get; set; }
        public int gymId { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
    }
}
