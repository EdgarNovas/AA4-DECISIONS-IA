using Unity.VisualScripting;
using UnityEngine;

public class GoToFoodVendorState : State
{
    private StateMachine fsm;
    private Unit3D unit;

    private Vendor targetVendor;
    private bool arrived;
    private float waitTimer;
    private float waitTime = 3f;

    public GoToFoodVendorState(StateMachine fsm, Unit3D unit)
    {
        this.fsm = fsm;
        this.unit = unit;
    }

    public override void Enter()
    {
        arrived = false;
        waitTimer = 0f;

        // Buscar una tienda de comida
        targetVendor = VendorManager.Instance.GetClosestVendor(unit.transform.position, VendorType.Food);

        if (targetVendor == null)
        {
            Debug.Log("No hay vendedores de comida");
            return;
        }
    }

    public override void Tick(float deltaTime)
    {
        if (targetVendor == null) targetVendor = VendorManager.Instance.GetClosestVendor(unit.transform.position, VendorType.Food);

        if (!arrived)
        {
            unit.MoveToPosition(targetVendor.customerStandPoint.position);

            float distance = Vector3.Distance(
                unit.transform.position,
                targetVendor.customerStandPoint.position
            );

            if (distance < 0.2f)
            {
                arrived = true;
            }
            return;
        }

        waitTimer += deltaTime;

        if (waitTimer >= waitTime)
        {
            fsm.SwitchState(typeof(IdleState));
        }
    }

    public override void Exit()
    {
        targetVendor = null;
    }
}