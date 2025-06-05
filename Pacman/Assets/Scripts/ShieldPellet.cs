using UnityEngine;

public class ShieldPellet : Pellet
{
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().ShieldPelletEaten(this);
    }
}
