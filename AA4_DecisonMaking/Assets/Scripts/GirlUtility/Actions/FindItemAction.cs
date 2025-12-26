using UnityEngine;

public class FindItemAction : UtilityAction
{
    public override float CalculateUtility(GirlStats stats)
    {
        // Si YA tiene un objeto, no busques más (Utilidad 0)
        if (stats.HasItemToThrow) return 0f;

        // Aquí deberías usar un "ItemManager" similar al "VendorManager" 
        // si quieres hacerlo 100% SOLID para encontrar objetos, 
        // o por ahora devolver un valor fijo si solo estás probando.
        return utilityCurve.Evaluate(0.5f);
    }

    public override void Execute(GirlStats stats)
    {
        base.Execute(stats);
        Debug.Log("¡He encontrado algo del suelo!");

        // Simplemente marcamos que lo tiene
        stats.HasItemToThrow = true;
    }
}
