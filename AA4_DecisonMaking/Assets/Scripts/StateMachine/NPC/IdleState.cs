using UnityEngine;

public class IdleState : State
{
    private StateMachine fsm;
    private Unit3D unit;

    private Vendor targetVendor;
    private bool arrived;
    private float waitTimer;
    private float waitTime = 3f;

    public IdleState(StateMachine fsm, Unit3D unit)
    {
        this.fsm = fsm;
        this.unit = unit;
    }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {

    }
}
