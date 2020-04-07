namespace PlayersAndMonsters.Core
{
    using System;

    using PlayersAndMonsters.Core.Contracts;
    using PlayersAndMonsters.IO;
    using PlayersAndMonsters.IO.Contracts;

    public class Engine : IEngine
    {
        private IWriter writer;
        private IReader reader;
        private IManagerController controller;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new ManagerController();
        }

        public void Run()
        {
            while (true)
            {
                string[] input = reader.ReadLine().Split();

                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    string result = string.Empty;

                    if (input[0] == "AddPlayer")
                    {
                        string playerType = input[1];
                        string playerUsername = input[2];

                        result = this.controller.AddPlayer(playerType, playerUsername);
                    }
                    else if (input[0] == "AddCard")
                    {
                        string cardType = input[1];
                        string cardName = input[2];

                        result = this.controller.AddCard(cardType, cardName);
                    }
                    else if (input[0] == "AddPlayerCard")
                    {
                        string username = input[1];
                        string cardName = input[2];

                        result = this.controller.AddPlayerCard(username, cardName);
                    }
                    else if (input[0] == "Fight")
                    {
                        string attackUser = input[1];
                        string enemyUser = input[2];

                        result = this.controller.Fight(attackUser, enemyUser);
                    }
                    else if (input[0] == "Report")
                    {
                        result = this.controller.Report();
                    }

                    this.writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
