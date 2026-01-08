using Edgar;
using TMPro;
using UnityEditor;
using UnityEngine;

public class IdleState : State
{
    private StateMachine fsm;
    private Unit3D unit;

    private Vendor targetVendor;
    private bool arrived;
    private float waitTimer;
    private float waitTime;
    private float totalWaitTimer;
    private float totalWaitTime;
    private float minTotalWaitTime = 5f;
    private float maxTotalWaitTime = 15f;
    private float minWaitTime = 1f;
    private float maxWaitTime = 4f;
    private float originalSpeed;

    public IdleState(StateMachine fsm, Unit3D unit)
    {
        this.fsm = fsm;
        this.unit = unit;
    }

    public override void Enter()
    {
        Debug.Log("NPC Entra en idle");
        totalWaitTime = Random.Range(minTotalWaitTime, maxTotalWaitTime);
        originalSpeed = unit.GetSpeed();
        unit.SetSpeed(originalSpeed / 2f);
        GoToRandomPoint();
    }

    public override void Tick(float deltaTime)
    {
        totalWaitTimer += deltaTime;
        if (totalWaitTimer > totalWaitTime)
        {
            int nextState = Random.Range(0, 3);
            switch(nextState)
            {
                case 0:
                    fsm.SwitchState(typeof(IdleState));
                    break;
                case 1:
                    fsm.SwitchState(typeof(GoToFoodVendorState));
                    break;
                case 2:
                    fsm.SwitchState(typeof(GoToFunVendorState));
                    break;
            }
            
        }

        if (unit.path == null)
        {
            waitTimer += deltaTime;
            if (waitTimer > waitTime)
            {
                GoToRandomPoint();
            }
        }
    }

    public override void Exit()
    {
        unit.SetSpeed(originalSpeed);
    }

    private void GoToRandomPoint()
    {
        do
        {
            float minX = -4, maxX = 6, minY = 6, maxY = 15;
            Vector3 randomPoint = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            unit.MoveToPosition(randomPoint);
        } while (unit.path == null);

        waitTime = Random.Range(minWaitTime, maxWaitTime);
    }
}
