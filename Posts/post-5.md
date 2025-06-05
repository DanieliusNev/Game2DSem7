# Milestone 3: Pac-Man & Game Logic

In the final milestone, we focused on implementing all the core game mechanics and logic that tie everything together into a playable experience. This included handling Pac-Man’s lives, scoring, level progression, round resets, and game over behavior.

## Round & Game Flow

One of the first things we tackled was creating a working round system. After eating all the pellets, a new round is triggered, ghosts reset, pellets are reactivated, and the game continues into the next level. To keep things clean and modular, we used a `NewRound()` function in the `GameManager` that handles all the resets.

However, we ran into some issues here, particularly with Pac-Man disappearing after losing. For a while, when the player lost all lives, Pac-Man wouldn’t reappear properly in the next game. We had to carefully manage the state resets in both the `PacmanEaten()` and `NewGame()` functions to make sure everything respawns in the right order.

## Lives & Game Over

We implemented a life system where Pac-Man starts with 3 lives. Each time he's eaten by a ghost (unless the ghost is in frightened mode), he loses one. When all lives are gone, the Game Over screen is shown.

Another tricky part was making sure the game could restart cleanly after a Game Over — resetting score, level, pellets, and character positions without any leftover glitches from the previous game session.

## Scoring System

The scoring system is based on classic Pac-Man rules. Each normal pellet gives 10 points, power pellets (big ones) give 100 points and eating a ghost while it's in frightened mode grants 200 points. We also added fruit bonuses which appear depending on the current level — these are placed in a separate UI element at the bottom of the screen.

The score updates dynamically during gameplay using the `SetScore()` method in the `GameManager`, and is shown on screen via a `TMP_Text` UI component.

## Power-Ups

Two new power-ups were added to spice up gameplay:

- Shield, which gives Pac-Man one free hit without dying.
- Speed Boost, temporarily making him move faster.

We used trigger collisions similar to pellets and added conditions in the collision logic to handle their effects. Implementing the shield required adjusting the ghost collision logic to check whether the shield was active or not.

## Overall Game Logic

Finally, we polished the main game loop so that:

- Rounds advance correctly
- Power-ups trigger their effects
- Ghosts reset after each life lost
- The player can always start a new game without weird behavior
- Scoring works correctly with all elements
