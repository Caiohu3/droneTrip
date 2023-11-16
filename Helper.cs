public static class Helper
{
    public static List<string> GetData()
    {
        return File.ReadAllLines("./input/input.txt")
                                 .Select(format => format.Replace("[", "")
                                                         .Replace("]", ""))
                                                         .ToList();
    }
}