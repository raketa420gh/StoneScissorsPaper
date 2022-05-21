using System;
using Pathfinding;
using UnityEngine;
using Zenject;

public class Unit : MonoBehaviour
{
    public event Action<Unit> OnDestroy;

    public StateMachine StateMachine;
    public FindingTargetState FindingTargetState;
    public ChasingState ChasingState;

    [SerializeField] private BounceScaleAnimation _bounceScaleAnimation;
    private UnitData _unitData;
    private PlayerData _playerData;
    private AIPath _aiPath;
    private MeshRenderer[] _meshRenderers;
    private GameFactory _factory;
    private UnitsCounter _unitsCounter;

    public UnitType EnemyType => _unitData.EnemyType;
    public UnitType Type => _unitData.Type;
    public PlayerType PlayerType => _playerData.Type;
    
    public Unit UnitTarget { get; private set; }


    [Inject]
    public void Construct(UnitsCounter unitsCounter, GameFactory factory)
    {
        _unitsCounter = unitsCounter;
        _factory = factory;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var unit = collision.gameObject.GetComponent<Unit>();

        if (unit)
            if (_playerData.Type != unit._playerData.Type)
                if (EnemyType == unit._unitData.Type)
                    unit.DestroyUnit();
    }

    public void Initialize(PlayerData playerData, UnitData unitData)
    {
        _playerData = playerData;
        _unitData = unitData;
        
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _aiPath = GetComponent<AIPath>();

        SetupView(playerData);
        InitializeStateMachine();
    }

    public void ActivateBounceAnimation() => 
        _bounceScaleAnimation.Activate();

    public void DestroyUnit()
    {
        var vfx = _factory.CreateVFX(transform.position);
        vfx.startColor = _playerData.Material.color;
        vfx.Play();
        Destroy(vfx.gameObject, 1f);
        Destroy(gameObject);
        OnDestroy?.Invoke(this);
    }

    public void SetUnitTarget(Unit unitTarget)
    {
        UnitTarget = unitTarget;
        UnitTarget.OnDestroy += OnDestoyTarget;
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();

        FindingTargetState = new FindingTargetState(this, StateMachine, _unitsCounter);
        ChasingState = new ChasingState(this, StateMachine);

        StateMachine.ChangeState(FindingTargetState);
    }

    private void OnDestoyTarget(Unit unitTarget)
    {
        UnitTarget = null;
        StateMachine.ChangeState(FindingTargetState);
    }

    public void MoveTo(Transform target) => 
        _aiPath.destination = target.position;

    private void SetupView(PlayerData playerData)
    {
        foreach (var meshRenderer in _meshRenderers)
        {
            meshRenderer.material.color = playerData.Material.color;
        }
    }
}