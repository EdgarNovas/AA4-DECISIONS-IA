using UnityEngine;

public class NPCStateMachine : StateMachine
{
    public Unit3D unit;

    private void Start()
    {
        unit = GetComponent<Unit3D>();

        //Crear y registrar estados
        AddState(new IdleState(this, unit));
        AddState(new GoToFoodVendorState(this, unit));
        AddState(new GoToFunVendorState(this, unit));

        //Estado inicial
        SwitchState(typeof(GoToFunVendorState));
    }
}
