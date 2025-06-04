
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets; //transform bc need to look through all the children
    public GameOverScreen gameOverScreen;

    public AudioClip gameOverClip;
    public AudioClip victoryClip;
    public AudioClip pelletClip;
    public AudioClip powerPelletClip;
    public AudioClip ghostEatenClip;
    public AudioClip pacmanEatenClip;

    private AudioSource audioSource;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; } // can access the score but cant change it
    public int lives { get; private set; }
    public TMP_Text scoreText;  // score
    public TMP_Text livesText;
    public GameObject deathAnimationPrefab; // assign prefab in inspector
    public GameObject startScreenPanel;

    public int currentLevel = 1;
    public FruitDisplay fruitDisplay;

    //To collect fruit
    public GameObject fruitPrefab; // Assign in Inspector
    private GameObject currentFruitInstance;
    public Transform fruitSpawnPoint; // Create empty in scene
    public int[] fruitScores = { 100, 300, 500, 700, 1000, 1000, 2000, 3000, 3000, 5000 };
    //to track when fruit should appear
    private int pelletsEaten = 0;
    private bool fruitSpawned = false;

    private void Start() {
        pacman.gameObject.SetActive(false);
        foreach (Ghost ghost in ghosts) {
            ghost.gameObject.SetActive(false);
        }

        // Optional: hide pellets too
        foreach (Transform pellet in this.pellets) {
            pellet.gameObject.SetActive(false);
        }
    }


    public void StartGame() {
        startScreenPanel.SetActive(false);
        NewGame();
    }


    private void Update()
    {
        if (false)
        { //for now any key to start the game over, I think later its better to set up a specific screen with a specific button and key
            NewGame();
        }
    }
    private void NewGame()
    {
        SetScore(0); //start game with 0
        SetLives(3);
        NewRound();
    }

    public void RestartGame()
    {
        gameOverScreen.Reset();

        currentLevel = 1;
        SetScore(0);
        SetLives(3);

        pelletsEaten = 0;       
        fruitSpawned = false;

        pacman.gameObject.SetActive(true);
        foreach (Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(true);
        }

        ResetState(); //  Force reset their positions and states

        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        if (fruitDisplay != null)
        {
            fruitDisplay.SetFruitForLevel(currentLevel);
        }
    }




    private void SetScore(int Score)
    {
        this.score = Score;
        this.scoreText.text = "" + this.score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        this.livesText.text = "x" + this.lives;
    }

    private void NewRound()
    {
        if (fruitDisplay != null)
            fruitDisplay.SetFruitForLevel(currentLevel);

        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
        pelletsEaten = 0;
        fruitSpawned = false;


    }


    private void ResetState()
    {
        ResetGhostMultiplier();

        for (int i = 0; i < this.ghosts.Length; i++)
        {

            this.ghosts[i].ResetState();
            // Set ghost speed based on current level
            this.ghosts[i].movement.SetSpeedByLevel(currentLevel);
        }
        this.pacman.ResetState();
    }

    private void GameOver()
    {
        currentLevel = 1;
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false); //turning all object off

        gameOverScreen.Setup(score);

        AudioSource.PlayClipAtPoint(gameOverClip, transform.position, 1f);
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points + this.ghostMultiplier;
        AudioSource.PlayClipAtPoint(ghostEatenClip, transform.position, 1f);
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        Vector3 deathPos = pacman.transform.position;

        AudioSource.PlayClipAtPoint(pacmanEatenClip, transform.position, 1f);

        // Show animation (instantiated clone plays at Pacman's last position)
        GameObject anim = Instantiate(deathAnimationPrefab, deathPos, Quaternion.identity);
        anim.GetComponent<PacmanDeathAnimation>().Play(deathPos);

        // Hide Pacman
        pacman.gameObject.SetActive(false);

        // Update lives
        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        AudioSource.PlayClipAtPoint(pelletClip, transform.position, 0.8f);

        SetScore(this.score + pellet.points);

        pelletsEaten++;

        // Spawn fruit at 70th pellet, only once
        if (pelletsEaten == 70 && !fruitSpawned)
        {
        SpawnFruitForLevel(currentLevel);
        fruitSpawned = true;
        }

        if (!HasRemainingPellets())
        {
            currentLevel++;
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        AudioSource.PlayClipAtPoint(powerPelletClip, transform.position, 1f);

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
    
    private void SpawnFruitForLevel(int level)
{
    if (fruitPrefab == null || fruitSpawnPoint == null)
        return;

    if (currentFruitInstance != null)
        Destroy(currentFruitInstance);

    currentFruitInstance = Instantiate(fruitPrefab, fruitSpawnPoint.position, Quaternion.identity);

    // Set correct sprite
    SpriteRenderer sr = currentFruitInstance.GetComponent<SpriteRenderer>();
    Fruit fruit = currentFruitInstance.GetComponent<Fruit>();

    int index = Mathf.Clamp(level - 1, 0, fruitDisplay.fruitSprites.Length - 1);
    sr.sprite = fruitDisplay.fruitSprites[index];
    fruit.score = fruitScores[index];

    // Optional: auto-destroy after 10 seconds
    Destroy(currentFruitInstance, 5f);
}
public void AddFruitScore(int fruitPoints)
{
    SetScore(this.score + fruitPoints);
}


}
