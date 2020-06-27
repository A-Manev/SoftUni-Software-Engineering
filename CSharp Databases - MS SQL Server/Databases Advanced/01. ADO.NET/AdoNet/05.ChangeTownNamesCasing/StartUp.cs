using System;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ChangeTownNamesCasing
{
    public class StartUp
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";

        public static void Main()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            string country = Console.ReadLine();

            string result = ChangeTownNamesCasing(sqlConnection, country);

            Console.WriteLine(result);
        }

        private static string ChangeTownNamesCasing(SqlConnection sqlConnection, string country)
        {
            StringBuilder output = new StringBuilder();

            string selectCountryTownsQuery = @"SELECT Towns.[Name]
	                                        FROM Towns 
	                                        JOIN Countries ON Countries.Id = Towns.CountryCode
	                                        	WHERE Countries.[Name] = @countryName";

            using SqlCommand selectCountryTownsCommand = new SqlCommand(selectCountryTownsQuery, sqlConnection);

            selectCountryTownsCommand.Parameters.AddWithValue("@countryName", country);
            
            List<string> allTowns = new List<string>();

            using (SqlDataReader reader = selectCountryTownsCommand.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    output.AppendLine("No town names were affected.");
                    return output.ToString().TrimEnd();
                }

                while (reader.Read())
                {
                    string townName = (string)reader["Name"];
                    allTowns.Add(townName.ToUpper());
                }
            }

            string updateTownNamesCasingQuery = @"UPDATE Towns
	                                                SET [Name] = UPPER([Name])
	                                                WHERE CountryCode = (
					                                                     SELECT Countries.Id
							                                                FROM Countries 
						                                                    WHERE Countries.[Name] = @countryName
						                                                )";

            using SqlCommand updateTownNamesCasingCommand = new SqlCommand(updateTownNamesCasingQuery, sqlConnection);

            updateTownNamesCasingCommand.Parameters.AddWithValue("@countryName", country);

            updateTownNamesCasingCommand.ExecuteNonQuery();

            output.AppendLine($"{allTowns.Count} town names were affected.");
            output.AppendLine($"[{string.Join(", ", allTowns)}]");

            return output.ToString().TrimEnd();
        }
    }
}
