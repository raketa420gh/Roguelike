using System.Collections.Generic;
using UnityEngine;

public interface IStage
{
    Vector3 Position { get; }
    Vector3 CameraPosition { get; }
    IDoor Door { get; }
    List<SpawnPoint> FreeSpawnPoints { get; }
    bool InArea { get; }
    void LoadStage();
    void CompleteStage();
    void OccupySpawnPoint(int index);
}