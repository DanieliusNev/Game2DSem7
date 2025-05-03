
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public Ghost[] ghosts;
 public Pacman pacman;

 public Transform pellets; //transform bc need to look through all the children
 public int score{ get; private set;} // can access the score but cant change it
 public int lives{ get; private set;}

    private void Start(){
        NewGame();
    }

    private void Update(){
        if(this.lives <= 0 && Input.anyKeyDown){ //for now any key to start the game over, I think later its better to set up a specific screen with a specific button and key
            NewGame();
        }
    }
    private void NewGame(){
        SetScore(0); //start game with 0
        SetLives(3);
        NewRound();
    }
    

    private void SetScore(int Score){
        this.score = Score;
    }

    private void SetLives(int lives){
        this.lives = lives;
    }

    private void NewRound(){
        foreach(Transform pellet in this.pellets){
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState(){
        for(int i = 0; i<this.ghosts.Length; i++ ){
            this.ghosts[i].gameObject.SetActive(true);
        }
        this.pacman.gameObject.SetActive(true);
    }

    private void GameOver(){
        //ui later
        for(int i = 0; i<this.ghosts.Length; i++ ){
            this.ghosts[i].gameObject.SetActive(false);
        }
        this.pacman.gameObject.SetActive(false); //turning all object off
    }

    public void GhostEaten(Ghost ghost){
            SetScore(this.score + ghost.points);
    }
    public void PacmanEaten(){
        this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if(this.lives>0){
            Invoke(nameof(ResetState), 3.0f); //reseting round after 3sec
        } else{
            GameOver();
        }
    }

}
