using LiftWise.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using System;
using System.Security.Cryptography;
using System.Text;

namespace LiftWise.Data
{
    public class UserHandler : IUserHandler
    {
        private User? activeUser = null;
        private string connString = @"Data Source=Data/dbLiftWise.db";

        /// <summary>
        /// Effettua il login
        /// </summary>
        /// <param name="email">email inserita dall'utente</param>
        /// <param name="password">password inserita dall'utente</param>
        /// <returns>
        ///     1 se il login ha avuto successo
        ///     0 se la password inserita è errata
        ///     -1 se è stata sollevata un'ecczzione durante l'accesso al database
        /// </returns>
        public int Login(string email, string password)
        {
            SqliteConnection conn = new SqliteConnection(connString);
            SqliteParameter parEmail = new SqliteParameter("@parEmail", email);
            SqliteCommand cmd = new SqliteCommand("SELECT * FROM tblUtenti WHERE email = @parEmail", conn);
            cmd.Parameters.Add(parEmail);

            try
            {
                conn.Open();
                SqliteDataReader dr = cmd.ExecuteReader();
                User user = new User();
                while (dr.Read())
                {
                    user = new User
                    {
                        idUser = Convert.ToInt32(dr["idUser"]),
                        email = dr["email"].ToString(),
                        passwordHash = dr["passwordHash"].ToString()
                    };
                }
                conn.Close();
                if(MD5Hash(password) == user.passwordHash)
                {
                    activeUser = user;
                    return 1;
                }
                else
                {
                    Console.WriteLine("Wrong password");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Effettua il logout
        /// </summary>
        /// <returns>
        ///     0 se il logout è andato a buon fine.
        /// </returns>
        public int Logout()
        {
            activeUser = null;
            return 0;
        }

        /// <summary>
        /// Aggiunge un nuovo utente al database cifrando la password, nel caso non sia presente
        /// </summary>
        /// <param name="email">email inserita dall'utente</param>
        /// <param name="password">password inserita dall'utente</param>
        /// <returns>
        ///     1 se l'utente è stato inserito.
        ///     0 se l'utente è gia presente.
        ///     -1 se le operazioni al db hanno sollevato un eccezione.
        /// </returns>
        public int Register(string name, string surname, string taxCode, string email, string password)
        {
            List<string> emails = new List<string>();
            SqliteConnection conn = new SqliteConnection(connString);

            SqliteCommand cmd = new SqliteCommand("SELECT email FROM tblUsers", conn);
            SqliteDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    emails.Add(dr["email"].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

            if (!emails.Contains(email))
            {
                string passwordHash = MD5Hash(password);
                SqliteParameter parName = new SqliteParameter("@parName", email);
                SqliteParameter parSurname = new SqliteParameter("@parSurname", email);
                SqliteParameter parTaxCode = new SqliteParameter("@parTaxCode", email);
                SqliteParameter parEmail = new SqliteParameter("@parEmail", email);
                cmd = new SqliteCommand($"INSERT INTO tblUsers(name, surname, taxCode, email, passwordHash) VALUES (@parName, @parSurname, @parTaxCode, @parEmail, '{passwordHash}')", conn);
                cmd.Parameters.Add(parName);
                cmd.Parameters.Add(parSurname);
                cmd.Parameters.Add(parTaxCode);
                cmd.Parameters.Add(parEmail);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private string MD5Hash(string source)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder sBuilder = new StringBuilder();
            for(int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();            
        }

        public User ActiveUser { get => activeUser; }
    }
}
