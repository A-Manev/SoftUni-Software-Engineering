namespace SpaceStation.Core
{
    using System;

    using SpaceStation.IO;
    using SpaceStation.IO.Contracts;
    using SpaceStation.Core.Contracts;
    using System.Linq;

    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly Controller controller;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new Controller();
        }
        public void Run()
        {
            while (true)
            {
                var input = this.reader.ReadLine().Split();

                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    string result = string.Empty;

                    if (input[0] == "AddAstronaut")
                    {
                        string type = input[1];
                        string astronautName = input[2];

                        result = this.controller.AddAstronaut(type, astronautName);
                    }
                    else if (input[0] == "AddPlanet")
                    {
                        string planetName = input[1];
                        string[] items = input.Skip(2).ToArray();

                        //string[] items = new string[input.Length - 2];

                        //if (input.Length > 2)
                        //{
                        //    for (int i = 2; i < input.Length; i++)
                        //    {
                        //        items[i - 2] = input[i];
                        //    }
                        //}

                        result = this.controller.AddPlanet(planetName, items);
                    }
                    else if (input[0] == "RetireAstronaut")
                    {
                        string astronautName = input[1];

                        result = this.controller.RetireAstronaut(astronautName);
                    }
                    else if (input[0] == "ExplorePlanet")
                    {
                        string planetName = input[1];

                        result = this.controller.ExplorePlanet(planetName);
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
