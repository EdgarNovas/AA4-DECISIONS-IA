using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance;

    // Lista de todas las monedas del mapa
    private List<LootItem> activeItems = new List<LootItem>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterItem(LootItem item)
    {
        if (!activeItems.Contains(item)) activeItems.Add(item);
        Debug.Log("item registrado");
    }

    public void UnregisterItem(LootItem item)
    {
        if (activeItems.Contains(item)) activeItems.Remove(item);
    }

    public LootItem GetClosestItem(Vector3 seekerPos)
    {
        LootItem bestTarget = null;
        float closestDistSqr = Mathf.Infinity;

        foreach (LootItem item in activeItems)
        {
            if (item == null) continue;

            Vector3 directionToTarget = item.transform.position - seekerPos;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistSqr)
            {
                closestDistSqr = dSqrToTarget;
                bestTarget = item;
            }
        }
        return bestTarget;
    }
}
