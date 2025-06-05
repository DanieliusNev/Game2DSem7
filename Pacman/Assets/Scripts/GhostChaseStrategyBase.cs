using UnityEngine;

public abstract class GhostChaseStrategyBase : MonoBehaviour
{
    public abstract Vector2 GetTargetPosition(Ghost ghost);
}
