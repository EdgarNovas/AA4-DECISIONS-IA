using System.Collections.Generic;
using UnityEngine;

public class VendorManager : MonoBehaviour
{
    public static VendorManager Instance;

    // Listas separadas para optimizar la búsqueda (evita iterar lo que no necesitas)
    private List<Vendor> foodVendors = new List<Vendor>();
    private List<Vendor> funVendors = new List<Vendor>();

    private void Awake()
    {
        // Singleton pattern simple
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // OPEN/CLOSED: Los vendedores se registran ellos mismos. No arrastramos nada.
    public void RegisterVendor(Vendor vendor)
    {
        if (vendor.vendorType == VendorType.Food) foodVendors.Add(vendor);
        else funVendors.Add(vendor);
    }

    public void UnregisterVendor(Vendor vendor)
    {
        if (vendor.vendorType == VendorType.Food) foodVendors.Remove(vendor);
        else funVendors.Remove(vendor);
    }

    // Lógica pura de búsqueda espacial
    public Vendor GetClosestVendor(Vector3 seekerPosition, VendorType type)
    {
        List<Vendor> targetList = (type == VendorType.Food) ? foodVendors : funVendors;

        Vendor bestTarget = null;
        float closestDistSqr = Mathf.Infinity;

        foreach (Vendor v in targetList)
        {
            if (v == null) continue;

            // Usamos sqrMagnitude porque es más rápido que Distance (evita raíces cuadradas)
            Vector3 directionToTarget = v.transform.position - seekerPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistSqr)
            {
                closestDistSqr = dSqrToTarget;
                bestTarget = v;
            }
        }
        return bestTarget;
    }
}
