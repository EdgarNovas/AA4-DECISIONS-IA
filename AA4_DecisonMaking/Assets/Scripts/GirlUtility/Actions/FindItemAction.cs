using UnityEngine;

public class FindItemAction : UtilityAction
{
    public override float CalculateUtility(GirlStats stats)
    {
        // Si ya tiene algo en la mano, la utilidad de buscar otro es 0.
        if (stats.HasItemToThrow) return 0f;

        // Si no hay objetos cerca, utilidad 0.
        if (stats.NearestLootItem == null) return 0f;

        // Si no tiene nada, le damos una utilidad base constante o aleatoria
        // para simular "curiosidad".
        return utilityCurve.Evaluate(0.7f); // Un valor fijo moderadamente alto
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);
        Debug.Log("¡mira! He encontrado una moneda/munición.");
        // Ir al objeto, cogerlo, marcar stats.HasItemToThrow = true;
    }
}
