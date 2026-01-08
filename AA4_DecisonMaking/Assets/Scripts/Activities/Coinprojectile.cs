using UnityEngine;

public class Coinprojectile : MonoBehaviour
{
    private Vector3 startPosition;
    private Transform target; // El objetivo a perseguir (El Player)
    private float flightDuration = 1.0f; // Tarda 1 segundo en llegar
    private float arcHeight = 2.0f; // Altura máxima del arco (2 metros)

    private float timeElapsed = 0f;
    private bool isLaunched = false;

    public void Launch(Transform _target)
    {
        target = _target;
        startPosition = transform.position;
        isLaunched = true;
        timeElapsed = 0f;
    }

    void Update()
    {
        if (!isLaunched || target == null)
        {
            if (isLaunched && target == null) Destroy(gameObject); // Si el player muere, la moneda se rompe
            return;
        }

        // 1. Calcular el progreso (de 0 a 1)
        timeElapsed += Time.deltaTime;
        float percentComplete = timeElapsed / flightDuration;

        // 2. Movimiento BASE (Lineal):
        // Interpolamos desde donde salimos hasta donde está el jugador AHORA MISMO.
        // Al usar 'target.position' en el Update, si el jugador se mueve, la moneda corrige el rumbo.
        Vector3 currentPos = Vector3.Lerp(startPosition, target.position + Vector3.up, percentComplete);

        // 3. Añadir el ARCO (Vertical):
        // Usamos la función Sin(x * PI). 
        // En 0 vale 0, en 0.5 (mitad camino) vale 1, en 1 (final) vale 0.
        float currentHeight = Mathf.Sin(percentComplete * Mathf.PI) * arcHeight;

        // Sumamos esa altura a la posición
        transform.position = currentPos + Vector3.up * currentHeight;

        // 4. Rotación visual (para que gire bonita)
        transform.Rotate(Vector3.right * 720 * Time.deltaTime);

        // 5. Impacto
        if (percentComplete >= 1.0f)
        {
            Debug.Log("¡Moneda recibida!");
            // Aquí puedes añadir sonido o restar vida
            Destroy(gameObject);
        }
    }
}
