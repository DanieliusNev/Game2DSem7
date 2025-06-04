using UnityEngine;

public class SpeedPellet : Pellet
{
    public float duration = 7.0f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().SpeedPelletEaten(this);
    }
}
