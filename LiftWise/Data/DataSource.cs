using LiftWise.Models;
using LiftWise.Models.Helper;
using Microsoft.Data.Sqlite;
using System.Linq.Expressions;

namespace LiftWise.Data
{
    public class DataSource : IDataSource
    {
        public string connString = @"Data Source=Data/dbLiftWise.db";
        public List<ViewedMembership> GetMemberships(int idUser)
        {
            SqliteConnection conn = new SqliteConnection(connString);
            SqliteCommand cmd = new SqliteCommand($"SELECT * FROM tblSubscriptions INNER JOIN tblGym ON tblMemberships.gymId = tblGyms.idGym WHERE tblSubsciptions.userId = {idUser}", conn);
            try
            {
                conn.Open();
                SqliteDataReader dr = cmd.ExecuteReader();
                List<ViewedMembership> abbonamenti = new List<ViewedMembership>();
                while (dr.Read())
                {
                    abbonamenti.Add(new ViewedMembership
                    {
                        idMembership = Convert.ToInt32(dr["idMembership"]),
                        userId = Convert.ToInt32(dr["userId"]),
                        gymId = Convert.ToInt32(dr["gymId"]),
                        startDate = DateOnly.Parse(dr["startDate"].ToString()),
                        endDate = DateOnly.Parse(dr["endDate"].ToString()),
                        gymName = dr["name"].ToString()
                    });
                }
                conn.Close();
                return abbonamenti;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ViewedMembership>();
            }
        }
    }
}
