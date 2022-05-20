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
    
    private UnitData _unitData;
    private PlayerData _playerData;
    private AIPath _aiPath;
    private Outline[] _outlines;
    private UnitsCounter _unitsCounter;

    public UnitType EnemyType => _unitData.EnemyType;
    public UnitType Type => _unitData.Type;
    public PlayerType PlayerType => _playerData.Type;
    
    public Unit UnitTarget { get; private set; }


    [Inject]
    public void Construct(UnitsCounter unitsCounter) => 
        _unitsCounter = unitsCounter;

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

        _outlines = GetComponentsInChildren<Outline>();
        _aiPath = GetComponent<AIPath>();

        SetupView(playerData);
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();

        FindingTargetState = new FindingTargetState(this, StateMachine, _unitsCounter);
        ChasingState = new ChasingState(this, StateMachine);

        StateMachine.ChangeState(FindingTargetState);
    }

    public void DestroyUnit()
    {
        Destroy(gameObject);
        OnDestroy?.Invoke(this);
    }

    public void SetUnitTarget(Unit unitTarget)
    {
        UnitTarget = unitTarget;
        UnitTarget.OnDestroy += OnDestoyTarget;
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
        foreach (var outline in _outlines)
        {
            outline.OutlineColor = playerData.Material.color;
            outline.OutlineWidth = 10f;
        }
    }
}