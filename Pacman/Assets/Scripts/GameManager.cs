
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets; //transform bc need to look through all the children

    // Win screen
    public GameObject winScreenPanel;
    public TMP_Text winScoreText; // link to final score text on win screen

    // Game Over screen
    public GameOverScreen gameOverScreen;

    // Pause screen
    public GameObject pauseScreenPanel;
    public GameObject pauseButton;
    private bool isPaused = false;

    // Speed pellet
    public GameObject speedPelletPrefab;
    public Transform speedPelletSpawnPoint;

    private float speedPelletTimer = 0f;
    private bool speedPelletActive = false;

    // Shield pellet
    public GameObject shieldPelletPrefab;
    public Transform shieldPelletSpawnPoint;

    private float shieldPelletTimer = 0f;
    private bool shieldPelletActive = false;

    private bool shieldActive = false;

    // Audio clips
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
    public GameObject startButton;

    private void Start()
    {
        ShowStartScreen();
        pacman.gameObject.SetActive(false);
        foreach (Ghost ghost in ghosts)
        {
            ghost.gameObject.SetActive(false);
        }

        // Optional: hide pellets too
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        startScreenPanel.SetActive(false);
        NewGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;

        pauseScreenPanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        pauseScreenPanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (!speedPelletActive)
        {
            speedPelletTimer -= Time.deltaTime;

            if (speedPelletTimer <= 0f)
            {
                SpawnSpeedPellet();
                speedPelletActive = true;
            }
        }

        if (!shieldPelletActive)
        {
            shieldPelletTimer -= Time.deltaTime;

            if (shieldPelletTimer <= 0f)
            {
                SpawnShieldPellet();
                shieldPelletActive = true;
            }
        }
    }

    private void NewGame()
    {
        speedPelletTimer = 30f;
        speedPelletActive = false;

        shieldPelletTimer = 40f; // Time until first shield pellet appears
        shieldPelletActive = false;


        SetScore(0); //start game with 0
        SetLives(3);
        NewRound();
    }

    public void RestartGame()
    {
        gameOverScreen.Reset();

        if (winScreenPanel != null)
        {
            winScreenPanel.SetActive(false);
        }

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

    private void WinGame()
    {
        // Stop everything
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);

        if (winScoreText != null)
        {
            winScoreText.text = "Final Score: " + this.score;
        }

        if (winScreenPanel != null)
        {
            winScreenPanel.SetActive(true);
        }
    }

    private void SpawnSpeedPellet()
    {
        if (speedPelletPrefab != null && speedPelletSpawnPoint != null)
        {
            Instantiate(speedPelletPrefab, speedPelletSpawnPoint.position, Quaternion.identity);
        }
    }

    public void SpeedPelletEaten(SpeedPellet pellet)
    {
        PelletEaten(pellet);
        StartCoroutine(SpeedBoost(pellet.duration));
        speedPelletTimer = 30f;
        speedPelletActive = false;
    }

    private void SpawnShieldPellet()
    {
        if (shieldPelletPrefab != null && shieldPelletSpawnPoint != null)
        {
            Instantiate(shieldPelletPrefab, shieldPelletSpawnPoint.position, Quaternion.identity);
        }
    }

    public void ShieldPelletEaten(ShieldPellet pellet)
    {
        PelletEaten(pellet);
        shieldActive = true;
        pacman.GetComponent<SpriteRenderer>().color = Color.cyan;

        shieldPelletTimer = 30f;
        shieldPelletActive = false;

        // Start shield expiration countdown
        StartCoroutine(ExpireShieldAfterTime(10f)); // 10 seconds
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
        if (shieldActive)
        {
            shieldActive = false;
            pacman.GetComponent<SpriteRenderer>().color = Color.white;

            // Disable all ghost colliders briefly
            StartCoroutine(DisableGhostColliders(0.5f));

            Debug.Log("Shield absorbed the hit!");
            return;
        }

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
            if (currentLevel >= 10)
            {
                WinGame();
            }
            else
            {
                currentLevel++;
                this.pacman.gameObject.SetActive(false);
                Invoke(nameof(NewRound), 3.0f);
            }
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

        Destroy(currentFruitInstance, 5f);
    }

    public void AddFruitScore(int fruitPoints)
    {
        SetScore(this.score + fruitPoints);
    }

    private IEnumerator SpeedBoost(float duration)
    {
        float originalMultiplier = pacman.movement.speedMultiplier;

        pacman.GetComponent<SpriteRenderer>().color = Color.red;

        pacman.movement.speedMultiplier = 1.5f;

        yield return new WaitForSeconds(duration);

        pacman.movement.speedMultiplier = originalMultiplier;
        pacman.GetComponent<SpriteRenderer>().color = Color.white;
    }
    
    public void ShowStartScreen()
    {
        startScreenPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null); // Clear any previous selection
        EventSystem.current.SetSelectedGameObject(startButton); // Highlight the Play button
    }

    private IEnumerator ExpireShieldAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (shieldActive)
        {
            shieldActive = false;
            pacman.GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("Shield expired after 10 seconds.");
        }
    }

    private IEnumerator DisableGhostColliders(float duration)
    {
        foreach (Ghost ghost in ghosts)
        {
            Collider2D col = ghost.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;
        }

        yield return new WaitForSeconds(duration);

        foreach (Ghost ghost in ghosts)
        {
            Collider2D col = ghost.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = true;
        }
    }
}
