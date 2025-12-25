using UnityEngine;

public abstract class UtilityAction : MonoBehaviour, IUtilityAction
{
    public string ActionName;
    public string Name => ActionName;

    [SerializeField] protected AnimationCurve utilityCurve;

    public abstract float CalculateUtility(GirlStats stats);

    public virtual void Execute(GirlStats stats)
    {
        Debug.Log($"Ejecutando acción: {ActionName} con Score alto.");
        // Aquí iría la lógica de movimiento o animación
    }
}
