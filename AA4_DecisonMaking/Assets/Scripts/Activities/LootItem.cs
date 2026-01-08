using UnityEngine;

public class LootItem : MonoBehaviour
{
    
    void OnEnable()
    {
        if (LootManager.Instance != null) LootManager.Instance.RegisterItem(this);
    }

    void OnDisable()
    {
        if (LootManager.Instance != null) LootManager.Instance.UnregisterItem(this);
    }

    public void PickUp()
    {
        // Aquí podrías poner efectos de sonido o partículas
        Debug.Log("¡Moneda recogida!");

        // Destruimos el objeto (y al morir se desregistra solo)
        Destroy(gameObject);
    }
}
