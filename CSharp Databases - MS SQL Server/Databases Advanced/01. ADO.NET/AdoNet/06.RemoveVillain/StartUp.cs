using System;
using System.Text;
using Microsoft.Data.SqlClient;

namespace RemoveVillain
{
    class StartUp
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";

        static void Main()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            int villainId = int.Parse(Console.ReadLine());

            string result = RemoveVillainFromTheDatabaseAndReleasesHisMinionsFromServingToHim(sqlConnection, villainId);

            Console.WriteLine(result);
        }

        private static string RemoveVillainFromTheDatabaseAndReleasesHisMinionsFromServingToHim(SqlConnection sqlConnection, int villainId)
        {
            StringBuilder output = new StringBuilder();

            string selectTheVillainNameQuery = @"SELECT Name FROM Villains WHERE Id = @villainId";

            using SqlCommand selectTheVillainNameCommand = new SqlCommand(selectTheVillainNameQuery, sqlConnection);

            selectTheVillainNameCommand.Parameters.AddWithValue("@villainId", villainId);

            string villainName = (string)selectTheVillainNameCommand.ExecuteScalar();

            if (villainName == null)
            {
                output.AppendLine("No such villain was found.");
                return output.ToString().TrimEnd();
            }

            output.AppendLine($"{villainName} was deleted.");

            string selectVillainServingMinionsCountQuery = @"SELECT COUNT(*) FROM Villains
	                                                            JOIN MinionsVillains ON Villains.Id = MinionsVillains.VillainId
	                                                            WHERE Id = @villainId";

            using SqlCommand selectVillainServingMinionsCountCommand = new SqlCommand(selectVillainServingMinionsCountQuery, sqlConnection);

            selectVillainServingMinionsCountCommand.Parameters.AddWithValue("@villainId", villainId);

            int villainServingMinionsCount = (int)selectVillainServingMinionsCountCommand.ExecuteScalar();

            output.AppendLine($"{villainServingMinionsCount} minions were released.");

            string deleteFromMinionsVillainsQuery = @"DELETE FROM MinionsVillains 
                                                        WHERE VillainId = @villainId";

            using SqlCommand deleteFromMinionsVillainsCommand = new SqlCommand(deleteFromMinionsVillainsQuery, sqlConnection);

            deleteFromMinionsVillainsCommand.Parameters.AddWithValue("@villainId", villainId);

            deleteFromMinionsVillainsCommand.ExecuteNonQuery();

            string deleteVillainWithTheGivenIdQuery = @"DELETE FROM Villains
                                                         WHERE Id = @villainId";

            using SqlCommand deleteVillainWithTheGivenIdCommand = new SqlCommand(deleteVillainWithTheGivenIdQuery, sqlConnection);

            deleteVillainWithTheGivenIdCommand.Parameters.AddWithValue("@villainId", villainId);

            deleteVillainWithTheGivenIdCommand.ExecuteNonQuery();

            return output.ToString().TrimEnd();
        }
    }
}
