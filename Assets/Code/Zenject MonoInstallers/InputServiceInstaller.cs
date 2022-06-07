using Zenject;

public class InputServiceInstaller : MonoInstaller
{
    public override void InstallBindings() =>
        BindInputService();

    private void BindInputService()
    {
        Container
            .Bind<IInputService>()
            .To<MobileInputService>()
            .AsSingle();
    }
}