namespace LiftWise.Models
{
    public class Membership
    {
        public int idMembership { get; set; }
        public int userId { get; set; }
        public int gymId { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
    }
}
