namespace RailwayStation.Models.Station;

public class Park
{
    public string Name { get; set; }
    //public List<Track> Traks { get; set; }

    //public Park(List<Track> traks) 
    //{
    //    if (traks.Count == 0) 
    //    {
    //        throw new ArgumentException("The park must contain at least one path!");
    //    }
    //}


    public override string ToString() => Name;
}
