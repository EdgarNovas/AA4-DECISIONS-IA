using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UtilityBrain : MonoBehaviour
{
    private GirlStats stats;
    private List<IUtilityAction> availableActions;
    private IUtilityAction currentAction;

    [Header("Ajustes del Cerebro")]
    // Puntos extra para mantenerse en la tarea actual.
    
    [Range(0f, 1f)] public float inertiaBonus = 0.2f;

    void Start()
    {
        stats = GetComponent<GirlStats>();
        availableActions = GetComponents<IUtilityAction>().ToList();
    }

    void Update()
    {
        currentAction = DecideBestAction();

        if (currentAction != null)
        {
            currentAction.Execute(stats);
        }
    }

    IUtilityAction DecideBestAction()
    {
        IUtilityAction bestAction = null;
        float bestScore = -Mathf.Infinity; // Empezamos muy bajo

        foreach (var action in availableActions)
        {
            float score = action.CalculateUtility(stats);

            
            // Si esta acción es la que ya estamos haciendo, le sumamos el bonus.
            // Esto hace que "se comprometa" con su decisión.
            if (action == currentAction && score > 0)
            {
                score += inertiaBonus;
            }
            

            // Debug para ver cómo la inercia afecta a la decisión
            // if (action == currentAction) Debug.Log($"Manteniendo {action.Name} con inercia: {score}");

            if (score > bestScore)
            {
                bestScore = score;
                bestAction = action;
            }
        }

        return bestAction;
    }
}
