namespace RailwayStation.Models.Station;

public class Park
{
    public string Name { get; set; }

    private readonly List<Track> traks = []; 
    public IReadOnlyList<Track> Traks => traks;

    public Park(string name, List<Track> traks) 
    {
        if (string.IsNullOrEmpty(name)) 
        {
            throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
        }

        if (traks.Count == 0) 
        {
            throw new ArgumentException("The park must contain at least one path!");
        }

        Name = name;
        this.traks = traks;
    }

    public override string ToString() => Name;
}
