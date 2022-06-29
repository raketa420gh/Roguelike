public interface ILevelLoop
{
    IStage CurrentStage { get; }
    void SetCurrentStage(IStage stage);
}