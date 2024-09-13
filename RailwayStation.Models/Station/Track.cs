namespace RailwayStation.Models.Station
{
    public class Track
    {
        public string Name { get; set; }     
        public IReadOnlyList<Segment> Segments { get; }
        
        public Track(string name, List<Segment> segments) 
        {
            if (string.IsNullOrEmpty(name)) 
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
            }

            Name = name;
            Segments = segments;
        }

        public override string ToString() => Name;
    }
}
