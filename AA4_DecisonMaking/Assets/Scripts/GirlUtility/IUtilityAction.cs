using UnityEngine;

public interface IUtilityAction
{
    string Name { get; }
    float CalculateUtility(GirlStats stats);
    void Execute(GirlStats stats);
}
