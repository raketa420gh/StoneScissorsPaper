using System.Linq;
using UnityEngine;

public class FindingTargetState : UnitState
{
    private Unit _ownerUnit;
    private UnitsCounter _unitsCounter;

    public FindingTargetState(Unit unit, StateMachine stateMachine, UnitsCounter unitsCounter) : base(unit, stateMachine)
    {
        _ownerUnit = unit;
        _unitsCounter = unitsCounter;
    }

    public override void Enter()
    {
        base.Enter();
        _ownerUnit.SetTarget(CheckNewEnemyTarget());
        stateMachine.ChangeState(_ownerUnit.ChasingState);
    }

    private Transform CheckNewEnemyTarget()
    {
        var allUnits = _unitsCounter.AllUnitsOnScene;

        foreach (var unit in allUnits.Where(unit => _ownerUnit.PlayerType != unit.PlayerType)
                     .Where(unit => _ownerUnit.EnemyType == unit.Type))
            return unit.transform;

        return FindEnemyTower().transform;
    }

    private Tower FindEnemyTower() => 
        _unitsCounter.AllTowers.FirstOrDefault(tower => 
            tower.PlayerType != _ownerUnit.PlayerType);
}