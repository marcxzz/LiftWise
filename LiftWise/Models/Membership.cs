namespace LiftWise.Models
{
    public class Membership
    {
        public int idMembership { get; set; }
        public int userId { get; set; }
        public int gymId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
