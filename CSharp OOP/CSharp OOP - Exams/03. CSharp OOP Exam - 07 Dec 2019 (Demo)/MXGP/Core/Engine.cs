namespace MXGP.Core
{
    using System;

    using MXGP.IO;
    using MXGP.IO.Contracts;
    using MXGP.Core.Contracts;

    public class Engine : IEngine
    {
        private IWriter writer;
        private IReader reader;
        private IChampionshipController controller; 

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new ChampionshipController();
        }

        public void Run()
        {
            while (true)
            {
                string[] input = reader.ReadLine().Split();

                if (input[0] == "End")
                {
                    Environment.Exit(0);
                }

                try
                {
                    string result = string.Empty;

                    if (input[0] == "CreateRider")
                    {
                        string riderName = input[1];

                        result = controller.CreateRider(riderName);
                    }
                    else if (input[0] == "CreateMotorcycle")
                    {
                        string type = input[1];
                        string model = input[2];
                        int horsePower = int.Parse(input[3]);

                        result = controller.CreateMotorcycle(type, model, horsePower);
                    }
                    else if (input[0] == "AddMotorcycleToRider")
                    {
                        string riderName = input[1];
                        string motorcycleModel = input[2];

                        result = controller.AddMotorcycleToRider(riderName, motorcycleModel);
                    }
                    else if (input[0] == "AddRiderToRace")
                    {
                        string raceName = input[1];
                        string riderName = input[2];

                        result = controller.AddRiderToRace(raceName, riderName);
                    }
                    else if (input[0] == "CreateRace")
                    {
                        string name = input[1];
                        int laps = int.Parse(input[2]);

                        result = controller.CreateRace(name, laps);
                    }
                    else if (input[0] == "StartRace")
                    {
                        string raceName = input[1];

                        result = controller.StartRace(raceName);
                    }

                    writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
