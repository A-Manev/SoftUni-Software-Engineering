using System;
using Microsoft.Data.SqlClient;

namespace MinionNames
{
    class StartUp
    {
        static void Main()
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                int inputMinionId = int.Parse(Console.ReadLine());

                string queryForVillainName = $@"SELECT Name FROM Villains WHERE Id = {inputMinionId}";

                SqlCommand findVillainName = new SqlCommand(queryForVillainName, sqlConnection);

                string villainName = (string)findVillainName.ExecuteScalar();

                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {inputMinionId} exists in the database.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Villain: {villainName}");
                }

                string queryForVillainMinions = $@"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = {inputMinionId}
                                ORDER BY m.Name";

                SqlCommand findVillainMinions = new SqlCommand(queryForVillainMinions, sqlConnection);

                using SqlDataReader reader = findVillainMinions.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        long rowNumber = (long)reader["RowNum"];
                        string minionName = (string)reader["Name"];
                        int minionAge = (int)reader["Age"];

                        Console.WriteLine($"{rowNumber}. {minionName} {minionAge}");
                    }
                }
                else
                {
                    Console.WriteLine("(no minions)");
                }
            }
        }
    }
}
