namespace LiftWise.Models.Helper
{
    public class ViewedSubscription
    {
        public int idSubscription { get; set; }
        public int userId { get; set; }
        public int gymId { get; set; }
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public string gymName { get; set; }

        //Aggiungere altri campi in caso di necessità
    }
}
