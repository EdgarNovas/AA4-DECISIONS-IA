using UnityEngine;

public class IdleState : State
{
    private StateMachine fsm;
    private Unit3D unit;

    private Vendor targetVendor;
    private bool arrived;
    private float waitTimer;
    private float waitTime;
    private float minWaitTime = 3f;
    private float maxWaitTime = 10f;
    private float originalSpeed;

    public IdleState(StateMachine fsm, Unit3D unit)
    {
        this.fsm = fsm;
        this.unit = unit;
    }

    public override void Enter()
    {
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        originalSpeed = unit.GetSpeed();
        unit.SetSpeed(originalSpeed / 2f);
    }

    public override void Tick(float deltaTime)
    {
        waitTimer += deltaTime;
        if (waitTimer > waitTime)
        {
            //fsm.SwitchState()
        }
    }

    public override void Exit()
    {

    }
}
