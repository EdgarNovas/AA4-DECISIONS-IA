using UnityEngine;

public class GirlStats : MonoBehaviour
{
    [Header("Necesidades Internas")]
    [Range(0, 100)] public float Hunger = 0f;
    [Range(0, 100)] public float Boredom = 0f;

    public bool HasItemToThrow = false;

    // Eliminamos NearestFoodVendor y NearestFunVendor.
    // La niña no necesita "guardar" al vendedor, solo necesita encontrarlo cuando tenga hambre.

    [Header("Referencias Fijas")]
    public Transform Player; // Esto se podría desacoplar también, pero lo dejaremos por ahora.
    public float DistanceToPlayer;

    void Update()
    {
        if (Player != null)
            DistanceToPlayer = Vector3.Distance(transform.position, Player.position);

        Hunger += Time.deltaTime * 3f;
        Boredom += Time.deltaTime * 5f;

        Hunger = Mathf.Clamp(Hunger, 0, 100);
        Boredom = Mathf.Clamp(Boredom, 0, 100);
    }
}
