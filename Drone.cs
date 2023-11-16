public class Drone
{
    public string Name { get; set; }
    public int MaxWeight { get; set; }
    public int CurrentWeight { get; set; }
    public List<string> Trips { get; set; }

    public Drone(string name, int maxWeight)
    {
        Name = name;
        MaxWeight = maxWeight;
        CurrentWeight = 0;
        Trips = new List<string>();
    }
}