using Zenject;

public class GameFactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGameFactory();
    }

    private void BindGameFactory()
    {
        Container
            .Bind<GameFactory>()
            .AsSingle();
    }
}