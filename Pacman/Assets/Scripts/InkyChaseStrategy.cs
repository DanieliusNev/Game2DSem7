using UnityEngine;

public class InkyChaseStrategy : GhostChaseStrategyBase
{
    public Ghost blinky;

    public override Vector2 GetTargetPosition(Ghost ghost)
    {
        Vector2 pacmanPosition = ghost.target.position;
        Vector2 pacmanDirection = ghost.target.GetComponent<Movement>().direction;

        Vector2 intermediate = pacmanPosition + pacmanDirection * 2f;
        Vector2 blinkyPosition = blinky.transform.position;

        // Reflect vector from Blinky to intermediate point
        return intermediate + (intermediate - blinkyPosition);
    }
}
