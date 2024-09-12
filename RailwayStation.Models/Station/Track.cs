namespace RailwayStation.Models.Station
{
    public class Track
    {
        public string Name { get; set; }
        public List<Segment> Segments { get; } = [];

        public override string ToString() => Name;
        public Track(string name) 
        {
            if (string.IsNullOrEmpty(name)) 
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
            }

            Name = name;
        }
    }
}
