using Zenject;

public class AssetProviderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindAssetProvider();
    }

    private void BindAssetProvider()
    {
        Container
            .Bind<AssetProvider>()
            .AsSingle();
    }
}