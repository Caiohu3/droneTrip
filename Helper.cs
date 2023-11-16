public static class Helper
{
    public static List<string> GetData()
    {
        return File.ReadAllLines("./input/input.txt")
                                 .Select(format => format.Replace("[", "")
                                                         .Replace("]", ""))
                                                         .ToList();
    }

    public static Drone SelectBestDrone(List<Drone> drones, int sumOfWeight)
    {
        Drone selectedDrone = new();
        foreach (Drone drone in drones)
        {
            if (sumOfWeight <= drone.MaxWeight)
                return drone;
            else
                selectedDrone = drones.Last();
        }
        return selectedDrone;
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