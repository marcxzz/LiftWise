namespace LiftWise.Models
{
    public class Abbonamento
    {
        public int idAbbonamento { get; set; }
        public int utenteId { get; set; }
        public int palestraId { get; set; }
        public DateOnly dataInizio { get; set; }
        public DateOnly dataFine { get; set; }
    }
}
