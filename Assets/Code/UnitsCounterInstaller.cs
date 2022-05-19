using Zenject;

public class UnitsCounterInstaller : MonoInstaller
{
    public UnitsCounter UnitsCounter;
    
    public override void InstallBindings()
    {
        BindUnitsCounter();
    }

    private void BindUnitsCounter()
    {
        Container
            .BindInstance(UnitsCounter)
            .AsSingle();
    }
}