namespace RailwayStation.Algorithms;
public class QueuePointStorage : IPointStorage
{
    private readonly Queue<int> pointIndices = new();

    public int GetFirst() => pointIndices.Dequeue();
    public void Insert(int pointIndex) => pointIndices.Enqueue(pointIndex);
    public bool IsEmpty() => pointIndices.Count == 0;
}
