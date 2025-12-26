using UnityEngine;

public class FollowPlayerAction : UtilityAction
{
    // Referencia al sistema de movimiento
    private Unit3D movementController;

    private void Start()
    {
        movementController = GetComponent<Unit3D>();
    }

    public override float CalculateUtility(GirlStats stats)
    {
        if (stats.Player == null) return 0f;

        // Distancia máxima donde la urgencia es 100% (ej. 15 metros)
        float maxDistance = 15f;

        // Normalizamos: 0 si está pegada, 1 si está lejos
        float normalizedDist = Mathf.Clamp01(stats.DistanceToPlayer / maxDistance);

        // Usamos la curva (debe subir exponencialmente)
        return utilityCurve.Evaluate(normalizedDist);
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);

        // ¡AQUÍ ES DONDE LE DECIMOS QUE SE MUEVA!
        if (movementController != null && stats.Player != null)
        {
            movementController.MoveToPosition(stats.Player.position);
        }
        else
        {
            Debug.LogWarning("Falta Unit3D o el Player no está asignado en GirlStats");
        }
    }
}
