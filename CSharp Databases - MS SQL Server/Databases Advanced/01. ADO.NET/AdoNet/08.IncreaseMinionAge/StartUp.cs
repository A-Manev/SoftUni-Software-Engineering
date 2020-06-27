using System;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace IncreaseMinionAge
{
    class StartUp
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;MultipleActiveResultSets=True";

        static void Main()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            int[] inputIds = Console.ReadLine().Split().Select(int.Parse).ToArray();

            string result = IncreaseMinionAge(sqlConnection, inputIds);

            Console.WriteLine(result);
        }

        private static string IncreaseMinionAge(SqlConnection sqlConnection, int[] inputIds)
        {
            StringBuilder output = new StringBuilder();

            string selectMinionNameAndAgeQuery = @"SELECT Id, Name, Age FROM Minions";

            using SqlCommand selectMinionNameAndAgeCommand = new SqlCommand(selectMinionNameAndAgeQuery, sqlConnection);

            using SqlDataReader reader = selectMinionNameAndAgeCommand.ExecuteReader();

            while (reader.Read())
            {
                int minionId = (int)reader["Id"];
                string minionName = (string)reader["Name"];
                int minionAge = (int)reader["Age"];

                if (inputIds.Contains(minionId))
                {
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                    minionName = textInfo.ToTitleCase(minionName);
                    output.AppendLine($"{minionName.Trim()} {minionAge + 1}");

                    string increaseMinionAgeQuery = @" UPDATE Minions
                                                       SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                                       WHERE Id = @Id";
                    
                    using (SqlCommand increaseMinionAgeCommand = new SqlCommand(increaseMinionAgeQuery, sqlConnection))
                    {
                        increaseMinionAgeCommand.Parameters.AddWithValue("@Id", minionId);

                        increaseMinionAgeCommand.ExecuteNonQuery();
                    }
                    
                    continue;
                }

                output.AppendLine($"{minionName.Trim()} {minionAge}");
            }

            return output.ToString().TrimEnd();
        }
    }
}
