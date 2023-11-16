internal class Program
{
    public static void Main(string[] args)
    {
        //preparing the data
        List<string> lines = Helper.GetData();
        List<Drone> drones = Helper.GetDrones(lines);
        List<Location> locations = Helper.GetLocations(lines);
        //get the total weight to use as a controler 
        int sumOfWeight = locations.Select(location => location.PackageWeight).Sum();

        while (sumOfWeight > 0)
        {
            Drone bestDrone = Helper.SelectBestDrone(drones, sumOfWeight);
            int TripCount = bestDrone.Trips.Count + 1;
            bestDrone.Trips.Add(TripCount, new List<string>());
            // iterate over the locations list to fill the drone until you max capacity
            for (int i = 0; i < locations.Count; i++)
            {
                //if the total trip weight is not full find more locations to add
                if (bestDrone.CurrentWeight == 0 || locations[i].PackageWeight + bestDrone.CurrentWeight <= bestDrone.MaxWeight)
                {
                    bestDrone.CurrentWeight += locations[i].PackageWeight;
                    bestDrone.Trips[TripCount].Add(locations[i].Name);
                    sumOfWeight -= locations[i].PackageWeight;
                }
            }
            //remove the selected locations in the trip
            for (int i = 0; i < bestDrone.Trips[TripCount].Count; i++)
            {
                locations.RemoveAll(location => location.Name.Equals(bestDrone.Trips[TripCount][i]));
            }
            //resset the drone wight for the next trip
            bestDrone.CurrentWeight = 0;
        }


        foreach (Drone drone in drones)
        {
            Console.WriteLine($"[{drone.Name}]");
            for (int i = 0; i < drone.Trips.Count; i++)
            {
                string printTrip = "";
                if (drone.Trips.TryGetValue(i + 1, out List<string> tripLocations))
                {
                    Console.WriteLine($"Trip #{i + 1}");
                    foreach (string location in tripLocations)
                    {
                        printTrip += $"[{location}], ";
                    }
                }
                Console.WriteLine(printTrip);
            }
            Console.WriteLine("");
        }
    }
}