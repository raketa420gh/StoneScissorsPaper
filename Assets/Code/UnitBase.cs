using System;
using UnityEngine;
using Zenject;

public abstract class UnitBase : MonoBehaviour
{
    public event Action<UnitBase> OnDestroy;
    
    private GameFactory _factory;
    private SoundPlayer _soundPlayer;
    private PlayerData _playerData;
    private MeshRenderer[] _meshRenderers;
    private BounceScaleAnimation _bounceScaleAnimation;
    
    public PlayerType PlayerType => _playerData.Type;
    
    [Inject]
    public void Construct(GameFactory factory, SoundPlayer soundPlayer)
    {
        _factory = factory;
        _soundPlayer = soundPlayer;
    }

    public virtual void Initialize(PlayerData playerData, UnitData unitData)
    {
        _playerData = playerData;
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _bounceScaleAnimation = GetComponent<BounceScaleAnimation>();
    }

    public virtual void DestroyUnit()
    {
        _soundPlayer.CreateSfxUnitDeath(gameObject.transform.position);
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }

    public void ActivateBounceAnimation() => 
        _bounceScaleAnimation.Activate();

    protected void SetupView(PlayerData playerData)
    {
        foreach (var meshRenderer in _meshRenderers)
            meshRenderer.material.color = playerData.Material.color;
    }

    protected void CreateVFX()
    {
        var vfx = _factory.CreateVFX(transform.position);
        vfx.startColor = _playerData.Material.color;
        vfx.Play();
        Destroy(vfx.gameObject, 1f);
    }
}