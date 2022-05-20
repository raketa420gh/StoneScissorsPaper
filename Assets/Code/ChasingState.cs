using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
        UpdateChasing();
    }

    private async Task UpdateChasing(float period = 1f, float startDelay = 0f)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(startDelay));

        while (true)
        {
            MoveToTarget(_ownerUnit.UnitTarget.transform);
            await UniTask.Delay(TimeSpan.FromSeconds(period));
        }
    }

    private void MoveToTarget(Transform target) => 
        _ownerUnit.MoveTo(target);
}