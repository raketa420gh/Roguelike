using System.Collections.Generic;

public interface IStage
{
    IDoor Door { get; }
    List<SpawnPoint> FreeSpawnPoints { get; }
    bool InArea { get; }
    void LoadStage();
    void CompleteStage();
}