using UnityEngine;
using Zenject;

public class GameFactory : IGameFactory
{
    private AssetProvider _assetProvider;

    [Inject]
    public void Construct(AssetProvider assetProvider) =>
        _assetProvider = assetProvider;

    public Unit CreateUnit(Vector3 position, string path = AssetPath.Debug, Transform parent = null)
    {
        GameObject obj = _assetProvider.Instantiate(path, position, Quaternion.identity);
        obj.transform.SetParent(parent);
        var unit = obj.GetComponent<Unit>();
        return unit ? unit : null;
    }

    public Pointer CreatePointer(Vector3 position, string path = AssetPath.Pointer, Transform parent = null)
    {
        GameObject obj = _assetProvider.Instantiate(path, position, Quaternion.identity);
        obj.transform.SetParent(parent);
        var pointer = obj.GetComponent<Pointer>();
        return pointer;
    }

    public ParticleSystem CreateVFX(Vector3 position, string path = AssetPath.UnitFX, Transform parent = null)
    {
        GameObject obj = _assetProvider.Instantiate(path, position, Quaternion.identity);
        obj.transform.SetParent(parent);
        var particleSystem = obj.GetComponentInChildren<ParticleSystem>();
        return particleSystem;
    }
}