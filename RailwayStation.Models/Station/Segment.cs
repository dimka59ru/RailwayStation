namespace RailwayStation.Models.Station;

// Edge
public class Segment
{
    public string Name { get; set; }
    public Point From { get; set; }
    public Point To { get; set; }
    public int Length { get; set; }

    public Segment(string name, Point from, Point to, int length) {
        if (string.IsNullOrEmpty(name)) {
            throw new ArgumentException($"\"{nameof(name)}\" cannot be undefined or empty.", nameof(name));
        }

        From = from ?? throw new ArgumentNullException(nameof(from));
        To = to ?? throw new ArgumentNullException(nameof(to));

        if (length <= 0) {
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than 0!");
        }

        Length = length;
        Name = name;
    }

    public override string ToString() => Name;
}
