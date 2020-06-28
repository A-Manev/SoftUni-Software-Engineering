using System;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient;

namespace IncreaseAgeStoredProcedure
{
    class StartUp
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";

        static void Main()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            int inputMinionId = int.Parse(Console.ReadLine());

            string result = IncreaseMinionAgeById(sqlConnection, inputMinionId);

            Console.WriteLine(result);
        }

        private static string IncreaseMinionAgeById(SqlConnection sqlConnection, int inputMinionId)
        {
            StringBuilder output = new StringBuilder();

            string procedureName = @"usp_GetOlder";

            using SqlCommand increaseAgeCommand = new SqlCommand(procedureName, sqlConnection);

            increaseAgeCommand.CommandType = CommandType.StoredProcedure;
            increaseAgeCommand.Parameters.AddWithValue("@minionId", inputMinionId);
            increaseAgeCommand.ExecuteNonQuery();

            string getMinionInfoQuery = @"SELECT Name, Age 
                                            FROM Minions 
                                            WHERE Id = @minionId";

            using SqlCommand getMinionInfoCommand = new SqlCommand(getMinionInfoQuery, sqlConnection);

            getMinionInfoCommand.Parameters.AddWithValue("@minionId", inputMinionId);

            using SqlDataReader reader = getMinionInfoCommand.ExecuteReader();

            reader.Read();

            string minionName = (string)reader["Name"];
            int minionAge = (int)reader["Age"];

            output.AppendLine($"{minionName.Trim()} - {minionAge} years old");

            return output.ToString().TrimEnd();
        }
    }
}
