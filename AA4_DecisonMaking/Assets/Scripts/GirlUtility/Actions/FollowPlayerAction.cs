using UnityEngine;

public class FollowPlayerAction : UtilityAction
{
    public override float CalculateUtility(GirlStats stats)
    {
        // Definimos una distancia máxima de "correa".
        // Digamos que a 15 metros la urgencia es máxima (1.0)
        // A 3 metros la urgencia es mínima (0.0) para dejarla explorar.

        float maxDistance = 15f;
        float normalizedDist = Mathf.Clamp01(stats.DistanceToPlayer / maxDistance);

        // Usamos la curva. 
        // TIP: Configura la curva en Unity para que sea plana al principio 
        // y suba exponencialmente al final.
        return utilityCurve.Evaluate(normalizedDist);
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);
        // Lógica de movimiento hacia stats.Player.position
        Debug.Log("¡Esperame! Corriendo hacia el jugador.");
    }
}
