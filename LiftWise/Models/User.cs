namespace LiftWise.Models
{
    public class User
    {
        public int idUser { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string taxCode { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }

        //Inserire altre proprietà in accordo con il CD
    }
}
