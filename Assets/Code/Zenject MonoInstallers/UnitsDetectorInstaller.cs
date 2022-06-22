using Zenject;

public class UnitsDetectorInstaller : MonoInstaller
{
    public UnitsDetector unitsDetector;
    
    public override void InstallBindings() =>
        Bind();

    private void Bind()
    {
        Container
            .Bind<IUnitsDetector>()
            .FromInstance(unitsDetector)
            .AsSingle()
            .NonLazy();
    }
}