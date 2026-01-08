using UnityEngine;

public enum VendorType { Food, Fun }

public class Vendor : MonoBehaviour
{
    
    [Header("Configuración")]
    public VendorType vendorType;
    public Transform customerStandPoint;
    public float recoveryAmount = 100f;

    void Start()
    {
        // Se auto-registra en el sistema global
        if (VendorManager.Instance != null)
            VendorManager.Instance.RegisterVendor(this);
    }

    void OnDestroy()
    {
        // Limpieza automática
        if (VendorManager.Instance != null)
            VendorManager.Instance.UnregisterVendor(this);
    }

    public void ServeCustomer(GirlStats stats)
    {
        if (vendorType == VendorType.Food) stats.Hunger = 0;
        else stats.Boredom = 0;

        Debug.Log($"Vendedor: Servido cliente en {name}");
    }

    private void OnDrawGizmos()
    {
        if (customerStandPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(customerStandPoint.position, 0.5f);
            Gizmos.DrawLine(transform.position, customerStandPoint.position);
        }
    }
}
