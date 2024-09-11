namespace RailwayStation.Algorithms;

// Интерфейс хранилища еще не посещенных узлов
public interface IPointStorage
{
    void Insert(int pointIndex);
    int GetFirst();
    bool IsEmpty();
}
