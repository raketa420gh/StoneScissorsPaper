public class UnitState : BaseState
{
    protected Unit unit;
    protected StateMachine stateMachine;

    protected UnitState(Unit unit, StateMachine stateMachine)
    {
        this.unit = unit;
        this.stateMachine = stateMachine;
    }
}