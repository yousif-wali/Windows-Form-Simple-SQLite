using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Sqlite
{
    public static class Database
    {
        private readonly static string path = "Database.db";
        private readonly static string cs = @"URI=file:" + Application.StartupPath + "\\" + path;
        private static SQLiteConnection con = new SQLiteConnection();
        private static SQLiteDataReader dr;
        public static void Query(string command, DataGridView dataGridView)
        {
            Query(command);
            int i = 0;
            while(i < 3)
            {
                Thread.Sleep(1);
                i++;
            }
            Get(dataGridView);
        }
        public static void Query(string command)
        {
            if (command.ToLower().Contains("select"))
            {
                throw new ArgumentException();
            }
            Connect();
            con.Open();
            try
            {
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = command;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw new ArgumentException();
            }
            finally
            {
                con.Close();
            }
        }
        public static void Get(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            Connect();
            con.Open();
            string stm = "SELECT * FROM test";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dataGridView.Rows.Add(dr.GetValue(0), dr.GetValue(1));
            }
            con.Close();
        }
        private static async void Connect()
        {
            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using var sqlite = new SQLiteConnection(@"Data Source=" + path);
                await sqlite.OpenAsync();
                string sql = "create table test(id int(11), Name varchar(20))";
                SQLiteCommand cmd = new(sql, sqlite);
                cmd.ExecuteNonQuery();
            }
            con = new SQLiteConnection(cs);

        }
    }
}
