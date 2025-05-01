using LiftWise.Models.Helper;

namespace LiftWise.Data
{
    public interface IDataSource
    {
        public List<AbbVisualizzato> GetAbbonamenti(int idUtente);
    }
}
