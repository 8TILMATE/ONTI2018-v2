using ONTI2018v_.Models;
using ONTI2018v_.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONTI2018v_
{
    public class DatabaseHelper
    {
        public static List<LectiiModel>lectii = new List<LectiiModel>(); 
        public static List<UserModel> user = new List<UserModel>();
        public static UserModel utilizatorLogat=new UserModel();
        public static void InsertIntoDB()
        {
            DeleteAllFromDb();
            ResetAllFromDb();
            UploadAllToDb();
        }
        private static void DeleteAllFromDb()
        {
            DeleteFromTable("Utilizatori");
            DeleteFromTable("Lectii");
            
        }
        private static void ResetAllFromDb()
        {
            ResetFromTable("Utilizatori");
            ResetFromTable("Lectii");
        }
        private static void UploadAllToDb()
        {
            InsertLectii();
            InsertUtilizatori();
        }
        private static void DeleteFromTable(string tableName)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Delete From " + tableName,con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static void ResetFromTable(string tableName)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DBCC CHECKIDENT('" + tableName+"',RESEED,0)",con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private static void InsertUtilizatori()
        {
            using(StreamReader rdr = new StreamReader(Resources.utilizatoriString))
            {
                while (rdr.Peek() >= 1)
                {
                    var line = rdr.ReadLine().Split('*');
                    using (SqlConnection con = new SqlConnection(Resources.connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("Insert into utilizatori values(@n,@e,@p)",con))
                        {
                            cmd.Parameters.AddWithValue("n", line[0]);
                            cmd.Parameters.AddWithValue("e", line[1]);
                            cmd.Parameters.AddWithValue("p", line[2]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        private static void InsertLectii()
        {
            using (StreamReader rdr = new StreamReader(Resources.lectiiString))
            {
                while (rdr.Peek() >= 1)
                {
                    var line = rdr.ReadLine().Split('*');
                    using (SqlConnection con = new SqlConnection(Resources.connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("Insert into Lectii values(@i,@t,@r,@d,@n)", con))
                        {
                            if (line.Length == 5)
                            {
                                cmd.Parameters.AddWithValue("i", Int32.Parse(line[0]));
                                cmd.Parameters.AddWithValue("t", line[1]);
                                cmd.Parameters.AddWithValue("r", line[2]);
                                cmd.Parameters.AddWithValue("d", DateTime.ParseExact(line[4], "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                                cmd.Parameters.AddWithValue("n", line[3]);

                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("i", Int32.Parse(line[0]));
                                cmd.Parameters.AddWithValue("r", line[1]);
                                cmd.Parameters.AddWithValue("n", line[2]);
                                cmd.Parameters.AddWithValue("d", DateTime.ParseExact(line[3], "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                                cmd.Parameters.AddWithValue("t", string.Empty);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
        public static void GetLectii()
        {
            using(SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("Select * From Lectii",con))
                {
                    using(SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read()) 
                        {
                            lectii.Add(new LectiiModel
                            {
                                IdLectie = rdr.GetInt32(0),
                                IdUser = rdr.GetInt32(1),
                                TitluLectie = rdr.GetString(2),
                                Regiune = rdr.GetString(3),
                                DataCreare = rdr.GetDateTime(4),
                                NumeImagine = rdr.GetString(5)
                            });
                        }
                    }
                }
            }
        }
        public static void UpdatePassword(string password,string email)
        {
            
                
                    using (SqlConnection con = new SqlConnection(Resources.connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("UPDATE UTILIZATORI SET Parola=@a Where Email=@e", con))
                        {
                    cmd.Parameters.AddWithValue("a", password);
                    cmd.Parameters.AddWithValue("e", email);
                            cmd.ExecuteNonQuery();
                        }
                   }
                
            
        }
        public static void GetUtilizatori()
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Select * From Utilizatori", con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            user.Add(new UserModel
                            {
                                IdUtilizator = rdr.GetInt32(0),
                                Name = rdr.GetString(1),
                                Password = rdr.GetString(2),
                                Email = rdr.GetString(3),
                            });
                        }
                    }
                }
            }
        }
        public static void InsertLectie(string titlu, string regiune, string nume)
        {
            using (SqlConnection con = new SqlConnection(Resources.connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("Insert into Lectii values(@i,@t,@r,@d,@n)", con))
                {

                    cmd.Parameters.AddWithValue("i", utilizatorLogat.IdUtilizator);
                    cmd.Parameters.AddWithValue("t", titlu);
                    cmd.Parameters.AddWithValue("r", regiune);
                    cmd.Parameters.AddWithValue("d", DateTime.ParseExact(DateTime.Now.ToString("M/dd/yyyy h:mm:ss tt"), "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                    cmd.Parameters.AddWithValue("n", nume);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
