using LiftWise.Models.Helper;

namespace LiftWise.Data
{
    public interface IDataSource
    {
        public List<ViewedSubscription> GetSubscriptions(int idUtente);
    }
}
