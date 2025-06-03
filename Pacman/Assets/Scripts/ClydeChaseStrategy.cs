using UnityEngine;

public class ClydeChaseStrategy : GhostChaseStrategyBase
{
    public Transform scatterCorner;

    public override Vector2 GetTargetPosition(Ghost ghost)
    {
        Vector2 pacmanPosition = ghost.target.position;
        Vector2 clydePosition = ghost.transform.position;

        float distance = (pacmanPosition - clydePosition).sqrMagnitude;

        // If farther than 8 tiles, chase Pac-Man; otherwise, scatter
        return distance > 64f ? pacmanPosition : scatterCorner.position;
    }
}
