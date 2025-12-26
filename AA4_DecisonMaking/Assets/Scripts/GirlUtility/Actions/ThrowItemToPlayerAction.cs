using UnityEngine;

public class ThrowItemToPlayerAction : UtilityAction
{
    public override float CalculateUtility(GirlStats stats)
    {
        if (!stats.HasItemToThrow) return 0f;

        // Solo se lo tira si está relativamente cerca para no fallar
        // Usamos una curva inversa (más cerca = más ganas de dárselo)
        float normalizedDist = 1f - Mathf.Clamp01(stats.DistanceToPlayer / 10f);

        return utilityCurve.Evaluate(normalizedDist);
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);
        Debug.Log("¡Toma esto! (Lanza objeto)");
        stats.HasItemToThrow = false;
    }
}
