# Blog Post #6: Final Game Summary

Our final game is a 2D Pac-Man clone, developed in Unity and playable both on PC and on the VIA XR Lab arcade machine. At its core, the game follows the original Pac-Man mechanics: the player navigates a maze, eats pellets, avoids ghosts, and tries to clear levels. However, we introduced several custom features and improvements to make the experience more dynamic and modern.

## Key Features

### Ghost AI
Each ghost has unique behavior:
- **Blinky** chases Pac-Man directly.
- **Pinky** aims 4 tiles ahead of Pac-Man’s direction.
- **Inky** uses both Blinky’s and Pac-Man’s positions for triangulated targeting.
- **Clyde** alternates between chasing and retreating depending on distance.

These behaviors are implemented using a modular system of interchangeable chase strategies that switch based on game state (chase, scatter, frightened, home).

### Pellets & Power-Ups
The maze includes:
- **Regular pellets**: Increase score by 10 points each.
- **Power pellets**: Trigger frightened mode where ghosts can be eaten.
- **Bonus fruits**: Spawn mid-level for extra points and indicate the current level.
- **New power-ups**: 
  - *Shield*: Allows one ghost collision without losing a life.
  - *Speed Boost*: Temporarily increases Pac-Man's movement speed.

### Game Logic
The game tracks score, lives (starting at 3), and level progression up to level 10. When all pellets are eaten, a new round starts. After 10 levels, the player wins. If all lives are lost, the Game Over screen appears. All UI, scoring, and reset logic is handled via a central GameManager script to ensure consistency and smooth transitions.

## Arcade-Ready Build
The game was finalized with input support for arcade buttons and joystick and tested successfully in the VIA XR Lab arcade machine. It also runs on desktop, making it accessible for further testing or showcasing.

## Conclusion
This project helped us go from a simple "Roll-a-Ball" game to a full-featured arcade title. We implemented core systems like AI, movement, scoring, and UI, while also learning how to manage complex game states. The result is a stable, engaging game that plays well both casually and in an arcade setting.
