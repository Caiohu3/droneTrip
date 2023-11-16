internal class Program
{
    public static void Main(string[] args)
    {
        List<string> lines = Helper.GetData();
        List<Drone> drones = Helper.GetDrones(lines);
        List<Location> locations = Helper.GetLocations(lines);
        int sumOfWeight = locations.Select(location => location.PackageWeight).Sum();

        while (sumOfWeight > 0)
        {
            Drone bestDrone = Helper.SelectBestDrone(drones, sumOfWeight);
            List<string> trips = new List<string>();
            for (int i = 0; i < locations.Count; i++)
            {
                if (bestDrone.CurrentWeight == 0 || locations[i].PackageWeight + bestDrone.CurrentWeight <= bestDrone.MaxWeight)
                {
                    bestDrone.CurrentWeight += locations[i].PackageWeight;
                    trips.Add(locations[i].Name);
                    sumOfWeight -= locations[i].PackageWeight;
                }
            }
           
            for (int i = 0; i < trips.Count; i++)
            {
                locations.RemoveAll(location => location.Name.Equals(trips[i]));
            }
            bestDrone.Trips.Add(String.Join(", ", trips));
            bestDrone.CurrentWeight = 0;
        }

    foreach (Drone drone in drones)
    {
        Console.WriteLine($"[{drone.Name}]");
           
        for (int i = 0; i < drone.Trips.Count; i++)
        {
            string tripLocations = drone.Trips[i];
            Console.WriteLine($"Trip #{i+1}: \n[{tripLocations}]");
        }
        Console.WriteLine(); // Espaço extra após as informações de cada drone
    }
  }
}