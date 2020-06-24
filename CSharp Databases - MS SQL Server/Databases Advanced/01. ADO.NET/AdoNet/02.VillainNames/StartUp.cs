using Microsoft.Data.SqlClient;
using System;

namespace VillainNames
{
    class StartUp
    {
        static void Main()
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                string command = @"SELECT *
	                                    FROM (
	                                    	  SELECT Villains.Name,
	                                    	         COUNT(*) AS [MinionsCount]
	                                    	      FROM Villains
	                                    	 	  JOIN MinionsVillains ON Villains.Id = MinionsVillains.VillainId
	                                    	      GROUP BY Villains.Name
	                                    	 ) AS TEMP
	                                    WHERE [MinionsCount] > 3
	                                    ORDER BY [MinionsCount] DESC";

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    string villainName = (string)reader["Name"];
                    int numberOfMinions = (int)reader["MinionsCount"];

                    Console.WriteLine(villainName + " - " + numberOfMinions);
                }
            }
        }
    }
}
