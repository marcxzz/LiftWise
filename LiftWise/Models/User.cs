namespace LiftWise.Models
{
    public class User
    {
        public int idUser { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string taxCode { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
    }
}
