namespace HiveMind.Server.Endpoints;

public static class Helper
{
    public delegate T1 Transformer<T1, T2>(T1 value1, T2 value2);

    public static List<T1> Resolve<T1, T2>(List<(T1, T2)> updates, Transformer<T1, T2> transformer)
    {
        var results = new List<T1>();

        foreach (var item in updates)
        {
            results.Add(transformer(item.Item1, item.Item2));
        }

        return results;
    }
}
