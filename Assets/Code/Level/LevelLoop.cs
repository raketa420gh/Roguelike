public class LevelLoop : ILevelLoop
{
    private IStage _currentStage;

    public IStage CurrentStage => _currentStage;

    public void SetCurrentStage(IStage stage) => _currentStage = stage;
}