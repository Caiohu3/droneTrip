public class Location
{
    public string Name { get; set; }
    public int PackageWeight { get; set; }

    public Location(string name, int packageWeight)
    {
        Name = name;
        PackageWeight = packageWeight;
    }
}