using UnityEngine;

public class PinkyChaseStrategy : GhostChaseStrategyBase
{
    public override Vector2 GetTargetPosition(Ghost ghost)
    {
        Vector2 pacmanPosition = ghost.target.position;
        Vector2 pacmanDirection = ghost.target.GetComponent<Movement>().direction;

        // Pinky targets 4 tiles ahead of Pac-Man
        return pacmanPosition + pacmanDirection * 4f;
    }
}
