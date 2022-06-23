using Zenject;

public class LevelLoopInstaller : MonoInstaller
{
    public override void InstallBindings() =>
        Bind();

    private void Bind()
    {
        Container
            .Bind<ILevelLoop>()
            .To<LevelLoop>()
            .AsSingle();
    }
}