using System;
using UnityEngine;
using Zenject;

public abstract class UnitBase : MonoBehaviour
{
    public event Action<UnitBase> OnDestroy;
    
    private GameFactory _factory;
    private PlayerData _playerData;
    private MeshRenderer[] _meshRenderers;
    private BounceScaleAnimation _bounceScaleAnimation;
    
    public PlayerType PlayerType => _playerData.Type;
    
    [Inject]
    public void Construct(GameFactory factory)
    {
        _factory = factory;
    }

    public virtual void Initialize(PlayerData playerData, UnitData unitData)
    {
        _playerData = playerData;
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _bounceScaleAnimation = GetComponent<BounceScaleAnimation>();
    }
    
    public void DestroyUnit()
    {
        var vfx = _factory.CreateVFX(transform.position);
        vfx.startColor = _playerData.Material.color;
        vfx.Play();
        Destroy(vfx.gameObject, 1f);
        Destroy(gameObject);
        OnDestroy?.Invoke(this);
    }
    
    public void ActivateBounceAnimation() => 
        _bounceScaleAnimation.Activate();
    
    protected void SetupView(PlayerData playerData)
    {
        foreach (var meshRenderer in _meshRenderers)
            meshRenderer.material.color = playerData.Material.color;
    }
}