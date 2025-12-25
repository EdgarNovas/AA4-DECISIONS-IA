using UnityEngine;

public class GirlStats : MonoBehaviour
{
    [Header("Necesidades Internas")]
    [Range(0, 100)] public float Hunger = 0f;
    [Range(0, 100)] public float Boredom = 0f;
    public bool HasItemToThrow = false; 

    [Header("Sensores / Entorno")]
    public Transform Player; 
    public float DistanceToPlayer; 

    
    public Transform NearestFoodVendor;
    public Transform NearestFunVendor;
    public Transform NearestLootItem;

    void Update()
    {
        
        if (Player != null)
        {
            DistanceToPlayer = Vector3.Distance(transform.position, Player.position);
        }

        // 2. Aumentar necesidades
        Hunger += Time.deltaTime * 3f;
        Boredom += Time.deltaTime * 5f;

        // Clamping
        Hunger = Mathf.Clamp(Hunger, 0, 100);
        Boredom = Mathf.Clamp(Boredom, 0, 100);
    }
}
