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
    private UnitType _enemyType;
    private Outline[] _outlines;
    private UnitsCounter _unitsCounter;

    public UnitType EnemyType => _enemyType;
    public UnitType Type => _unitData.Type;
    public PlayerType PlayerType => _playerData.Type;
    public Transform Target { get; private set; }
    

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

        SetEnemyType(unitData);
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

    public void SetTarget(Transform target) => 
        Target = target;

    public void MoveTo(Transform target) => 
        _aiPath.destination = target.position;

    private void SetEnemyType(UnitData unitData)
    {
        if (unitData.Type == UnitType.Scissors)
            _enemyType = UnitType.Paper;
        if (unitData.Type == UnitType.Stone)
            _enemyType = UnitType.Scissors;
        if (_unitData.Type == UnitType.Paper)
            _enemyType = UnitType.Stone;
    }

    private void SetupView(PlayerData playerData)
    {
        foreach (var outline in _outlines)
        {
            outline.OutlineColor = playerData.Material.color;
            outline.OutlineWidth = 10f;
        }
    }
}