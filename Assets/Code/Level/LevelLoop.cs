public class LevelLoop : ILevelLoop
{
    private Stage _currentStage;

    public Stage CurrentStage => _currentStage;

    public void SetCurrentStage(Stage stage) => _currentStage = stage;
}