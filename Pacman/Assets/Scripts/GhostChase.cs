using UnityEngine;

public class GhostChase : GhostBehaviour
{
    [SerializeField] private GhostChaseStrategyBase chaseStrategy;

    private void Start()
    {
        if (chaseStrategy == null)
        {
            Debug.LogError($"{name}: No chase strategy assigned.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frightened.enabled && chaseStrategy != null)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            Vector2 target = chaseStrategy.GetTargetPosition(this.ghost);

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + (Vector3)availableDirection;
                float distance = (target - (Vector2)newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }

    private void OnDisable()
    {
        if (this.ghost != null && this.ghost.scatter != null)
        {
            this.ghost.scatter.Enable();
        }
    }
}
