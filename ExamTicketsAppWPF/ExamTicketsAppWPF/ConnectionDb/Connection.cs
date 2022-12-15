using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace ExamTicketsAppWPF.ConnectionDb
{
	public class Connection
	{
        static async Task Main(string[] args)
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=TicketsDb;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                MessageBox.Show("Подключение открыто" +
                    "Свойства подключения: " +
                    "Строка подключения: {connection.ConnectionString}" +
                    "База данных: {connection.Database}" +
                    "Сервер: {connection.DataSource}" +
                    "Версия сервера: {connection.ServerVersion}" +
                    "Состояние: {connection.State}" +
                    "Workstationld: {connection.WorkstationId}");
            }
            MessageBox.Show("Подключение закрыто..." +
                            "Программа завершила работу.");
        }
    }
}
	

