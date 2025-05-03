namespace LiftWise.Models.Helper
{
    public class ViewedMembership
    {
        public int idMembership { get; set; }
        public int userId { get; set; }
        public int gymId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string gymName { get; set; }

        //Aggiungere altri campi in caso di necessità
    }
}
