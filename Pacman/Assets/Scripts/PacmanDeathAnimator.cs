using UnityEngine;

public class PacmanDeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] deathFrames;
    public float frameTime = 0.1f;

    private int currentFrame;
    private float timer;
    private bool playing;

    public void Play(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        spriteRenderer.sprite = deathFrames[0];
        currentFrame = 0;
        timer = 0f;
        playing = true;
    }

    private void Update()
    {
        if (!playing) return;

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;
            currentFrame++;

            if (currentFrame < deathFrames.Length)
            {
                spriteRenderer.sprite = deathFrames[currentFrame];
            }
            else
            {
                playing = false;
                Destroy(gameObject); // destroy when finished
            }
        }
    }
}
