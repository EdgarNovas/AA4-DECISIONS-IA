using UnityEngine;

public class ThrowItemToPlayerAction : UtilityAction
{
    private Unit3D movementController;
    public GameObject coinPrefab;
    float cooldownTimer = 0f;

    void Start()
    {
        movementController = GetComponent<Unit3D>();
    }

    public override float CalculateUtility(GirlStats stats)
    {
        // 1. REQUISITO: Si NO tengo nada que tirar, utilidad 0.
        if (!stats.HasItemToThrow) return 0f;

        if (stats.Player == null) return 0f;

        // 2. Distancia al jugador
        // Queremos acercarnos un poco para tirar, pero no pegados.
        // Usamos una curva simple: cuanto más cerca del jugador, más ganas de tirar.
        float distToPlayer = Vector3.Distance(transform.position, stats.Player.position);
        float score = 1f - Mathf.Clamp01(distToPlayer / 20f);

        return utilityCurve.Evaluate(score);
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);

        if (stats.Player == null) return;

        movementController.MoveToPosition(stats.Player.position);
        float dist = Vector3.Distance(transform.position, stats.Player.position);

        // Si estamos a distancia de tiro
        if (dist < 8.0f && dist > 2.0f)
        {
            if (coinPrefab != null)
            {
                // Instanciamos la moneda
                GameObject coin = Instantiate(coinPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);

                // Obtenemos el script NUEVO
                Coinprojectile projectile = coin.GetComponent<Coinprojectile>();

                if (projectile != null)
                {
                    // CAMBIO AQUÍ: Pasamos el 'Transform' (stats.Player), no el Vector3.
                    projectile.Launch(stats.Player);
                }
            }

            stats.HasItemToThrow = false;
            cooldownTimer = Time.time + 2.0f;
        }
    }
}
