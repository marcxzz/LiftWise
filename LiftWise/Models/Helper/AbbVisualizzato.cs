namespace LiftWise.Models.Helper
{
    public class AbbVisualizzato
    {
        public int idAbbonamento { get; set; }
        public int utenteId { get; set; }
        public int palestraId { get; set; }
        public DateOnly dataInizio { get; set; }
        public DateOnly dataFine { get; set; }
        public int idPalestra { get; set; }
        public string nomePalestra { get; set; }
    }
}
