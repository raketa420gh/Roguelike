public interface IStage
{
    IDoor Door { get; }
    SpawnPoint[] SpawnPoints { get; }
    bool InArea { get; }
    void LoadStage();
    void CompleteStage();
}