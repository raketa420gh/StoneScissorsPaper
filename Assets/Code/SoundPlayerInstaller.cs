using Zenject;

public class SoundPlayerInstaller : MonoInstaller
{
    public SoundPlayer SoundPlayer;
    
    public override void InstallBindings()
    {
        BindSoundPlayer();
    }

    private void BindSoundPlayer()
    {
        Container
            .BindInstance(SoundPlayer)
            .AsSingle();
    }
}
