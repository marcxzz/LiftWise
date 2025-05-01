using LiftWise.Models;
using LiftWise.Models.Helper;
using Microsoft.Data.Sqlite;
using System.Linq.Expressions;

namespace LiftWise.Data
{
    public class DataSource : IDataSource
    {
        public string connString = @"Data Source=Data/dbLiftWise.db";
        public List<AbbVisualizzato> GetAbbonamenti(int idUtente)
        {
            SqliteConnection conn = new SqliteConnection(connString);
            SqliteCommand cmd = new SqliteCommand($"SELECT * FROM tblAbbonamenti INNER JOIN tblPalestre ON tblAbbonamenti.palestraId = tblPalestre.idPalestra WHERE tblAbbonamenti.utenteId = {idUtente}", conn);
            try
            {
                conn.Open();
                SqliteDataReader dr = cmd.ExecuteReader();
                List<AbbVisualizzato> abbonamenti = new List<AbbVisualizzato>();
                while (dr.Read())
                {
                    abbonamenti.Add(new AbbVisualizzato
                    {
                        idAbbonamento = Convert.ToInt32(dr["idAbbonamento"]),
                        utenteId = Convert.ToInt32(dr["utenteId"]),
                        palestraId = Convert.ToInt32(dr["palestraId"]),
                        dataInizio = DateOnly.Parse(dr["dataInizio"].ToString()),
                        dataFine = DateOnly.Parse(dr["dataFine"].ToString()),
                        nomePalestra = dr["nome"].ToString()
                    });
                }
                conn.Close();
                return abbonamenti;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AbbVisualizzato>();
            }
        }
    }
}
