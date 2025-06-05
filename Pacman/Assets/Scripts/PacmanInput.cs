using UnityEngine;
using UnityEngine.InputSystem;

public class PacmanInput : MonoBehaviour
{
    private Vector2 direction;

    public Movement movement; 

    public void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();

        if (direction != Vector2.zero)
        {
            movement.SetDirection(direction);
        }
    }
}
