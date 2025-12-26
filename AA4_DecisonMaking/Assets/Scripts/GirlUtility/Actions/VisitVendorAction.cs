using Edgar;
using UnityEngine;

public class VisitVendorAction : UtilityAction
{
    public VendorType targetType;

    // Inyección de dependencias (Referencias)
    private Unit3D movementController;
    private Grid3D grid; // Para calcular distancia Octil real

    // --- CACHÉ PARA OPTIMIZACIÓN 
    private Vendor currentTargetVendor;
    private float lastSearchTime = 0f;
    private float searchInterval = 1.0f; // Buscar nuevo vendedor solo cada 1 segundo

    void Start()
    {
        movementController = GetComponent<Unit3D>();
        grid = FindObjectOfType<Grid3D>();
    }

    public override float CalculateUtility(GirlStats stats)
    {
        // 1. ¿Qué tan fuerte es la necesidad?
        float needScore = (targetType == VendorType.Food) ? stats.Hunger : stats.Boredom;
        needScore /= 100f;

        // Si no tiene hambre, ni te molestes en buscar vendedores (Ahorro de CPU brutal)
        if (needScore < 0.1f) return 0f;

        // 2. Encontrar al vendedor más cercano (Usando Cache)
        if (currentTargetVendor == null || Time.time > lastSearchTime + searchInterval)
        {
            currentTargetVendor = VendorManager.Instance.GetClosestVendor(transform.position, targetType);
            lastSearchTime = Time.time + Random.Range(0f, 0.5f); // Offset aleatorio para que no busquen todas en el mismo frame
        }

        // Si no existe ningún vendedor en el mundo de ese tipo
        if (currentTargetVendor == null) return 0f;

        // 3. Calcular distancia usando tu Grid System (Octil)
        // Usamos Vector3 distance rápido primero para la utilidad general
        float dist = Vector3.Distance(transform.position, currentTargetVendor.transform.position);

        // Normalización inversa: Más cerca (0) = Más utilidad (1)
        // Asumimos que 50 metros es "demasiado lejos"
        float distanceFactor = 1f - Mathf.Clamp01(dist / 50f);

        // Score Final: Promedio ponderado (La necesidad importa más que la distancia)
        float finalScore = (needScore * 0.7f) + (distanceFactor * 0.3f);

        return utilityCurve.Evaluate(finalScore);
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);

        // Seguridad: Si el vendedor desapareció (se destruyó) justo ahora
        if (currentTargetVendor == null) return;

        // Usamos el punto de compra si existe, si no, la posición del vendedor
        Vector3 targetPos = (currentTargetVendor.customerStandPoint != null)
                            ? currentTargetVendor.customerStandPoint.position
                            : currentTargetVendor.transform.position;

        // Moverse usando tu sistema Pathfinding
        if (movementController != null)
        {
            movementController.MoveToPosition(targetPos);

            // Interacción simple por distancia
            if (Vector3.Distance(transform.position, targetPos) < 1.5f)
            {
                currentTargetVendor.ServeCustomer(stats);
                currentTargetVendor = null; // Reseteamos target para buscar uno nuevo la próxima vez si nos movemos
            }
        }
    }
}
