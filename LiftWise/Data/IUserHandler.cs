using LiftWise.Models;

namespace LiftWise.Data
{
    public interface IUserHandler
    {
        public int Login(string email, string password);

        public int Logout();

        public int Register(string name, string surname, string taxCode, string email, string password);

        public User ActiveUser { get; }
    }
}
