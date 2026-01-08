using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit3D : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] float speed = 5f; 
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] bool isPlayerControlled = true;
    public bool isAIControlled = false;
    // Variables de Pathfinding A*
    private Vector3[] path;
    private Vector3 offSet;
    private int targetIndex;

    private Vector3 lastTargetPosition;


    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        offSet = new Vector3 (0, 1, 0);
    }

    // Acciones le digan a dónde ir
    public void MoveToPosition(Vector3 targetPosition)
    {
        if (path != null && Vector3.Distance(targetPosition, lastTargetPosition) < 1.0f)
        {
            return;
        }

        // Solo pedimos camino si la distancia es relevante para no saturar
        if (Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            lastTargetPosition = targetPosition;
            PathRequestManager.RequestPath(transform.position, targetPosition, OnPathFound);
        }
    }


    private void Update()
    {
        if(isPlayerControlled && !isAIControlled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit, 1000))
                {
                    PathRequestManager.RequestPath(transform.position, hit.point, OnPathFound);
                }
            }
        }
       
    }

    /// <summary>
    /// Callback que recibe el camino desde el PathRequestManager.
    /// </summary>
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful && newPath.Length > 0)
        {
            path = newPath;
            targetIndex = 0; // Reiniciamos el índice para empezar a seguir el nuevo camino
        }
        else
        {
            path = null; // No se encontró camino
        }
    }


    private void FixedUpdate()
    {
        //  Seguir el camino
        Vector3 velocity = HandleMovement();
    }

    /// <summary>
    /// Mueve la unidad a lo largo de los waypoints del 'path' y devuelve la velocidad actual.
    /// </summary>
    Vector3 HandleMovement()
    {
        // Si no hay camino, no nos movemos.
        if (path == null || path.Length == 0)
        {
            return Vector3.zero;
        }

        // Obtener el waypoint actual
        Vector3 currentWaypoint = path[targetIndex];
        Vector3 oldPos = transform.position;

        // Moverse hacia el waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint + offSet, speed * Time.fixedDeltaTime);

        // Calcular la velocidad real para la animación
        Vector3 velocity = (transform.position - oldPos) / Time.fixedDeltaTime;

        // Comprobar si hemos llegado al waypoint
        if (Vector3.Distance(transform.position, currentWaypoint + offSet) < 0.01f)
        {
            targetIndex++; // Ir al siguiente waypoint
            if (targetIndex >= path.Length)
            {
                path = null; // Hemos llegado al final del camino
            }
        }

        return velocity;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // OnDrawGizmos sigue siendo útil para depurar el camino
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one * 0.5f);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}