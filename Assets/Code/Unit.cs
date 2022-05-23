using Pathfinding;
using UnityEngine;
using Zenject;

public class Unit : UnitBase
{
    public StateMachine StateMachine;
    public FindingTargetState FindingTargetState;
    public ChasingState ChasingState;

    private UnitsCounter _unitsCounter;
    private UnitData _unitData;
    private AIPath _aiPath;

    public UnitType EnemyType => _unitData.EnemyType;
    public UnitType Type => _unitData.Type;
    public UnitBase UnitTarget { get; private set; }

    [Inject]
    public void Construct(UnitsCounter unitsCounter)
    {
        _unitsCounter = unitsCounter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var unitBase = collision.gameObject.GetComponent<UnitBase>();

        if (!unitBase)
            return;
        if (PlayerType == unitBase.PlayerType)
            return;
        
        var unit = unitBase.GetComponent<Unit>();
        if (!unit)
            return;

        if (EnemyType == unit._unitData.Type)
            unitBase.DestroyUnit();
    }

    public override void Initialize(PlayerData playerData, UnitData unitData)
    {
        base.Initialize(playerData, unitData);
        _unitData = unitData;
        _aiPath = GetComponent<AIPath>();

        InitializeStateMachine();
        SetupView(playerData);
    }

    public void SetUnitTarget(UnitBase unitTarget)
    {
        UnitTarget = unitTarget;
        UnitTarget.OnDestroy += OnDestroyTarget;
    }

    private void InitializeStateMachine()
    {
        StateMachine = new StateMachine();
        FindingTargetState = new FindingTargetState(this, StateMachine, _unitsCounter);
        ChasingState = new ChasingState(this, StateMachine);
        StateMachine.ChangeState(FindingTargetState);
    }

    private void OnDestroyTarget(UnitBase unitTarget)
    {
        UnitTarget = null;
        StateMachine.ChangeState(FindingTargetState);
    }

    public void MoveTo(Transform target) =>
        _aiPath.destination = target.position;
}