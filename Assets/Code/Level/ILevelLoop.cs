public interface ILevelLoop
{
    Stage CurrentStage { get; }
    void SetCurrentStage(Stage stage);
}