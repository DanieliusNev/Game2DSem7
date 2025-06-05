using UnityEngine;

public class BlinkyChaseStrategy : GhostChaseStrategyBase
{
    public override Vector2 GetTargetPosition(Ghost ghost)
    {
        // Chases Pac-Man directly
        return ghost.target.position;
    }
}
