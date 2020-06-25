using System;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;

namespace AddMinion
{
    public class StartUp
    {
        private static string connectionString = "Server=.\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";

        //private const string DB_NAME = "MinionsDB";

        public static void Main()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            string[] minionInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            string[] villainInfo = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            string result = AddMinionToDatabase(sqlConnection, minionInfo, villainInfo);

            Console.WriteLine(result);
        }

        private static string AddMinionToDatabase(SqlConnection sqlConnection, string[] minionInfo, string[] villainInfo)
        {
            StringBuilder output = new StringBuilder();

            string minionName = minionInfo[1];
            int minionAge = int.Parse(minionInfo[2]);
            string minionTown = minionInfo[3];

            string villainName = villainInfo[1];

            string townId = EnsureTownExists(sqlConnection, minionTown, output);

            string villainId = EnsureVillainExists(sqlConnection, villainName, output);

            string insertMinionQuery = $@"INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";

            using SqlCommand insertMinionCommand = new SqlCommand(insertMinionQuery, sqlConnection);

            insertMinionCommand.Parameters.AddWithValue("@name", minionName);
            insertMinionCommand.Parameters.AddWithValue("@age", minionAge);
            insertMinionCommand.Parameters.AddWithValue("@townId", townId);

            insertMinionCommand.ExecuteNonQuery();

            string getMinionIdQuery = $@"SELECT Id FROM Minions WHERE Name = @minionName";

            using SqlCommand getMinionIdCommand = new SqlCommand(getMinionIdQuery, sqlConnection);

            getMinionIdCommand.Parameters.AddWithValue("@minionName", minionName);

            string minionId = getMinionIdCommand.ExecuteScalar()?.ToString();

            string insertIntoMinionsVillainsQuery = $@"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";

            using SqlCommand insertIntoMinionsVillains = new SqlCommand(insertIntoMinionsVillainsQuery, sqlConnection);

            insertIntoMinionsVillains.Parameters.AddWithValue("@villainId", villainId);
            insertIntoMinionsVillains.Parameters.AddWithValue("@minionId", minionId);

            insertIntoMinionsVillains.ExecuteNonQuery();

            output.AppendLine($"Successfully added {minionName} to be minion of {villainName}.");

            return output.ToString().TrimEnd();
        }

        private static string EnsureVillainExists(SqlConnection sqlConnection, string villainName, StringBuilder output)
        {
            string getVillainIdQuery = $@"SELECT Id FROM Villains WHERE Name = @Name";

            using SqlCommand getVillainIdCommand = new SqlCommand(getVillainIdQuery, sqlConnection);

            getVillainIdCommand.Parameters.AddWithValue("@Name", villainName);

            string villainId = getVillainIdCommand.ExecuteScalar()?.ToString();

            if (villainId == null)
            {
                string insertVillainQuery = $@"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@Name, 4)";

                using SqlCommand insertVillainCommand = new SqlCommand(insertVillainQuery, sqlConnection);

                insertVillainCommand.Parameters.AddWithValue("@Name", villainName);

                insertVillainCommand.ExecuteNonQuery();

                villainId = getVillainIdCommand.ExecuteScalar()?.ToString();

                output.AppendLine($"Villain {villainName} was added to the database.");
            }

            return villainId;
        }

        private static string EnsureTownExists(SqlConnection sqlConnection, string minionTown, StringBuilder output) 
        {
            string getTownIdQuery = $@"SELECT Id FROM Towns WHERE Name = @townName";

            using SqlCommand getTownIdCommand = new SqlCommand(getTownIdQuery, sqlConnection);

            getTownIdCommand.Parameters.AddWithValue("@townName", minionTown);

            string townId = getTownIdCommand.ExecuteScalar()?.ToString();

            if (townId == null)
            {
                string insertTownQuery = $@"INSERT INTO Towns (Name) VALUES (@townName)";

                using SqlCommand insertTownCommand = new SqlCommand(insertTownQuery, sqlConnection);

                insertTownCommand.Parameters.AddWithValue("@townName", minionTown);

                insertTownCommand.ExecuteNonQuery();

                townId = getTownIdCommand.ExecuteScalar()?.ToString();

                output.AppendLine($"Town {minionTown} was added to the database.");
            }

            return townId;
        }
    }
}
