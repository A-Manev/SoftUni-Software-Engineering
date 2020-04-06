namespace MXGP.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using MXGP.Models;
    using MXGP.Repositories;
    using MXGP.Models.Races;
    using MXGP.Models.Riders;
    using MXGP.Core.Contracts;
    using MXGP.Utilities.Messages;
    using MXGP.Models.Motorcycles;
    using MXGP.Models.Races.Contracts;
    using MXGP.Models.Riders.Contracts;
    using MXGP.Models.Motorcycles.Contracts;

    public class ChampionshipController : IChampionshipController
    {
        private const int MINIMUM_RACE_PARTICIPANTS = 3;

        private RaceRepository races; 
        private RiderRepository riders;
        private MotorcycleRepository motorcycles;

        public ChampionshipController()
        {
            this.races = new RaceRepository();
            this.riders = new RiderRepository();
            this.motorcycles = new MotorcycleRepository();
        }

        public string CreateRider(string riderName)
        {
            if (this.riders.GetAll().Any(r => r.Name == riderName))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RiderExists, riderName));
            }

            IRider rider = new Rider(riderName);

            this.riders.Add(rider);

            return string.Format(OutputMessages.RiderCreated, rider.Name);
        }

        public string CreateMotorcycle(string type, string model, int horsePower)
        {
            if (this.motorcycles.GetAll().Any(m => m.Model == model))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.MotorcycleExists, model));
            }

            IMotorcycle motorcycle = null;

            if (type == "Speed")
            {
                motorcycle = new SpeedMotorcycle(model, horsePower);
            }
            else if (type == "Power")
            {
                motorcycle = new PowerMotorcycle(model, horsePower);
            }

            this.motorcycles.Add(motorcycle);

            return string.Format(OutputMessages.MotorcycleCreated, motorcycle.GetType().Name, motorcycle.Model);
        }

        public string AddMotorcycleToRider(string riderName, string motorcycleModel)
        {
            var targetRider = this.riders.GetByName(riderName);
            var targetMotorcycle = this.motorcycles.GetByName(motorcycleModel);

            if (targetRider == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RiderNotFound, riderName));
            }

            if (targetMotorcycle == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.MotorcycleNotFound, motorcycleModel));
            }

            targetRider.AddMotorcycle(targetMotorcycle);

            return string.Format(OutputMessages.MotorcycleAdded, targetRider.Name, targetMotorcycle.Model);
        }

        public string AddRiderToRace(string raceName, string riderName)
        {
            var targetRace = this.races.GetByName(raceName);
            var targetRider = this.riders.GetByName(riderName);

            if (targetRace == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (targetRider == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RiderNotFound, riderName));
            }

            targetRace.AddRider(targetRider);

            return string.Format(OutputMessages.RiderAdded, targetRider.Name, targetRace.Name);
        }

        public string CreateRace(string name, int laps)
        {
            if (this.races.GetAll().Any(r => r.Name == name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }

            IRace race = new Race(name, laps);

            this.races.Add(race);

            return string.Format(OutputMessages.RaceCreated, race.Name);
        }

        public string StartRace(string raceName)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var targetRace = this.races.GetByName(raceName);

            if (targetRace == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (targetRace.Riders.Count() < MINIMUM_RACE_PARTICIPANTS)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, MINIMUM_RACE_PARTICIPANTS));
            }

            this.races.Remove(targetRace);

            var allParticipants = targetRace.Riders.OrderByDescending(r => r.Motorcycle.CalculateRacePoints(targetRace.Laps));

            var laps = targetRace.Laps;

            int count = 1;

            foreach (var rider in allParticipants)
            {
                if (count == 1)
                {
                    stringBuilder.AppendLine(string.Format(OutputMessages.RiderFirstPosition, rider.Name, targetRace.Name));
                }
                else if (count == 2)
                {
                    stringBuilder.AppendLine(string.Format(OutputMessages.RiderSecondPosition, rider.Name, targetRace.Name));
                }
                else if (count == 3)
                {
                    stringBuilder.AppendLine(string.Format(OutputMessages.RiderThirdPosition, rider.Name, targetRace.Name));
                }

                count++;
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
