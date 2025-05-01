using LiftWise.Models.Helper;

namespace LiftWise.Data
{
    public interface IDataSource
    {
        public List<ViewedMembership> GetMemberships(int idUtente);
    }
}
