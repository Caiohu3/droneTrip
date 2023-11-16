internal class Program
{
    public static void Main(string[] args)
    {
        List<string> lines = Helper.GetData();
        List<Drone> drones = GetDrones(lines);
        List<Location> locations = GetLocations(lines);
        int sumOfWeight = locations.Select(location => location.PackageWeight).Sum();

        while (sumOfWeight > 0)
        {
            Drone bestDrone = SelectBestDrone(drones, sumOfWeight);
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
        Console.WriteLine();
    }
    Console.ReadLine();
  }
  
    public static Drone SelectBestDrone(List<Drone> drones, int sumOfWeight)
    {
        return drones.FirstOrDefault(drone => sumOfWeight <= drone.MaxWeight) ?? drones.Last();
    }

    public static List<Location> GetLocations(List<string> lines)
    {
        return lines.Skip(1)
                    .Select(line =>
                    {
                        string[] locationInfo = line.Split(", ");
                        return new Location(locationInfo[0], int.Parse(locationInfo[1]));
                    })
                    .ToList();
    }

    public static List<Drone> GetDrones(List<string> lines)
    {
        List<Drone> deliveryDrones = new List<Drone>();
        string[] droneInfo = lines.First().Split(", ");
        for (int i = 0; i < droneInfo.Length; i += 2)
        {
            deliveryDrones.Add(new Drone(droneInfo[i], int.Parse(droneInfo[i + 1])));
        }
        return deliveryDrones.OrderBy(drone => drone.MaxWeight).ToList();
    }
}

