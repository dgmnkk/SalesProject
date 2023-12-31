﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access
{
    public class SalesManager
    {
        private SqlConnection connection = null;

        public SalesManager(string? connectionStr = null) 
        {
            connectionStr ??= ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;
            connection= new SqlConnection(connectionStr);
            connection.Open();
        }
        private void ShowTable(SqlDataReader reader)
        {
            
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write($"{reader.GetName(i),-10}\t");
            }
            Console.WriteLine("\n-------------------------------------------------------------------------------------");

            
            while (reader.Read()) // go to the next row
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write($"{reader[i],-10}\t");
                }
                Console.WriteLine();
            }
        }
        public void AddSale(Sale sale)
        {
            string cmd = $"insert Sales values(@customer, @seller, @date, @amount)";
            SqlCommand command = new SqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@customer", sale.CustomerId);
            command.Parameters.AddWithValue("@seller", sale.SellerId);
            command.Parameters.AddWithValue("@date", sale.Date);
            command.Parameters.AddWithValue("@amount", sale.Amount);

            command.ExecuteNonQuery();

        }

        public void ShowAllSales(DateOnly start, DateOnly end)
        {
            string cmd = $"select * from Sales where Date between @start and @end";
            SqlCommand command = new SqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@start", start.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@end", end.ToString("yyyy-MM-dd"));
            using var reader = command.ExecuteReader();
            ShowTable(reader);
        }

        public void DeleteCustomer(int id)
        {
            string cmd = $"delete from Customers where Id = @id";
            SqlCommand command = new SqlCommand(cmd, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public void ShowLastPurchase(string name, string surname)
        {
            string cmd = $"select * from Sales as s JOIN Customers as c ON s.CustomerId = c.Id where c.Name = @name and c.Surname = @surname order by s.Date desc";
            SqlCommand command = new SqlCommand (cmd, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.ExecuteNonQuery();
        }

        public void ShowLeaderSeller(string name, string surname)
        {
            string cmd = $"select sl.Name, sl.Surname, SUM(s.Amount) as TotalAmount from Sellers as sl JOIN Sales as s ON s.SellerId = sl.Id where sl.Name = @name and sl.Surname = @surname group by sl.Name, sl.Surname order by TotalAmount desc";
            SqlCommand command =new SqlCommand (cmd, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.ExecuteNonQuery();
        }
    }
}
