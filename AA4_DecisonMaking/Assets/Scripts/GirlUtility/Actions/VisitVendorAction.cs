using UnityEngine;

public class VisitVendorAction : UtilityAction
{
    public enum VendorType { Food, Fun }
    public VendorType type;

    public override float CalculateUtility(GirlStats stats)
    {
        float score = 0f;
        Transform target = null;

        // 1. Evaluamos qué necesidad estamos cubriendo
        if (type == VendorType.Food)
        {
            score = stats.Hunger / 100f; // Normalizamos
            target = stats.NearestFoodVendor;
        }
        else if (type == VendorType.Fun)
        {
            score = stats.Boredom / 100f;
            target = stats.NearestFunVendor;
        }

        // 2. FACTOR DE CORRECCIÓN IMPORTANTE:
        // Si no hay un vendedor cerca, la utilidad debería ser 0 
        // (no puede comprar si no hay tienda).
        if (target == null) return 0f;

        // 3. Evaluamos con la curva (ej. si tiene poca hambre, el score será bajo)
        return utilityCurve.Evaluate(score);
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);

        // Determinar destino
        Transform destination = (type == VendorType.Food) ? stats.NearestFoodVendor : stats.NearestFunVendor;

        if (destination != null)
        {
            Debug.Log($"Yendo al vendedor de {type}");
            // Moverse al vendedor e interactuar
            // Al llegar: stats.Hunger = 0;
        }
    }
}
