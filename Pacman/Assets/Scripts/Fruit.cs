using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int score = 100; // Default, override per level

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            FindObjectOfType<GameManager>().AddFruitScore(score);
            gameObject.SetActive(false);
        }
    }
}
