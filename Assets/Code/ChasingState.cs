using UnityEngine;

public class ChasingState : UnitState
{
    private Unit _ownerUnit;
    
    public ChasingState(Unit unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        _ownerUnit = unit;
    }

    public override void Enter()
    {
        base.Enter();
        var target = _ownerUnit.Target;
        MoveToTarget(target);
    }

    private void MoveToTarget(Transform target) => 
        _ownerUnit.MoveTo(target);
}