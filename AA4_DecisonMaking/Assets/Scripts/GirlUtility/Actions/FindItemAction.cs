using UnityEngine;

public class FindItemAction : UtilityAction
{
    private Unit3D movementController;
    private LootItem currentTargetItem;
    private float lastSearchTime;

    void Start()
    {
        movementController = GetComponent<Unit3D>();
    }

    public override float CalculateUtility(GirlStats stats)
    {
        // 1. Si YA tengo una moneda, la utilidad de buscar otra es 0.
        if (stats.HasItemToThrow) return 0f;

        // 2. Buscamos moneda cercana cada cierto tiempo (optimización)
        if (currentTargetItem == null || Time.time > lastSearchTime + 1.0f)
        {
            if (LootManager.Instance != null)
                currentTargetItem = LootManager.Instance.GetClosestItem(transform.position);

            lastSearchTime = Time.time + Random.Range(0f, 0.5f);
        }

        // Si no hay monedas en todo el mapa, utilidad 0
        if (currentTargetItem == null) return 0f;

        // 3. Calculamos utilidad basada en distancia
        // Si está cerca, da muchas ganas de cogerla.
        float dist = Vector3.Distance(transform.position, currentTargetItem.transform.position);
        float normalizedDist = 1f - Mathf.Clamp01(dist / 30f); // 30 metros max visión

        return utilityCurve.Evaluate(normalizedDist);
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);

        // Seguridad: ¿Alguien cogió la moneda antes que yo?
        if (currentTargetItem == null) return;

        // Moverse a la moneda
        movementController.MoveToPosition(currentTargetItem.transform.position);

        // Si llegamos, recogerla
        if (Vector3.Distance(transform.position, currentTargetItem.transform.position) < 1.5f)
        {
            currentTargetItem.PickUp();
            stats.HasItemToThrow = true; // ¡Ahora tengo munición!
            currentTargetItem = null;    // Olvido la referencia porque ya no existe
        }
    }
}
