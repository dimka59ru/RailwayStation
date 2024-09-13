namespace RailwayStation.Helpers;
public static class EnumerableExtensions
{
    public static int FindIndex<T>(this IEnumerable<T> list, Func<T, bool> predicate) {
        var matchingIndices = list.Select((value, index) => new { value, index }).Where(x => predicate(x.value)).Select(x => (int?) x.index);
        return matchingIndices.FirstOrDefault() ?? -1;
    }
}
