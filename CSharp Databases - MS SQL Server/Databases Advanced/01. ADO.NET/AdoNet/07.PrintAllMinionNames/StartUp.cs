using System;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace PrintAllMinionNames
{
    class StartUp
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";

        static void Main()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            string result = PrintAllMinionNames(sqlConnection);

            Console.WriteLine(result);
        }

        private static string PrintAllMinionNames(SqlConnection sqlConnection)
        {
            StringBuilder output = new StringBuilder();

            List<string> allMinionNames = new List<string>();

            string selectAllMinionsQuery = @"SELECT Name FROM Minions";

            using SqlCommand selectAllMinionsCommand = new SqlCommand(selectAllMinionsQuery, sqlConnection);

            using SqlDataReader reader = selectAllMinionsCommand.ExecuteReader();

            while (reader.Read())
            {
                string minionName = (string)reader["Name"];
                allMinionNames.Add(minionName);
            }

            if (allMinionNames.Count % 2 == 0)
            {
                ReturnNewMinionArrangement(output, allMinionNames);
            }
            else
            {
                ReturnNewMinionArrangement(output, allMinionNames);

                double midPerson = Math.Floor(allMinionNames.Count / 2.0);
                output.AppendLine(allMinionNames[(int)midPerson]);
            }

            return output.ToString().TrimEnd();
        }

        private static void ReturnNewMinionArrangement(StringBuilder output, List<string> allMinionNames)
        {
            for (int i = 0; i < allMinionNames.Count / 2; i++)
            {
                output.AppendLine(allMinionNames[i]);
                output.AppendLine(allMinionNames[allMinionNames.Count - i - 1]);
            }
        }
    }
}
