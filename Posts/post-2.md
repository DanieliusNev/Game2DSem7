# Game Design Document

## Game Overview

This Pac-Man clone is a modernized 2D arcade-style game that preserves the nostalgic gameplay of the original while introducing unique design elements. The player navigates a maze, consuming pellets and power-ups while avoiding ghosts with advanced AI behaviors. The goal is to achieve the highest possible score across increasing levels of difficulty.

## Gameplay

- **Objective**: Clear the maze by eating all pellets while avoiding ghosts.
- **Lives**: The player starts with 3 lives. Losing all lives ends the game.
- **Pellets**: Regular pellets grant points. Eating all triggers the next level.
- **Power-Ups**:
  - **Shield**: Grants immunity to ghost collision for one hit.
  - **Speed Boost**: Temporarily increases Pac-Man’s movement speed.
- **Fruits**: Appear during rounds for bonus points. Different fruit per level.
- **Levels**: The game includes 10 levels with progressive ghost speed and complexity.
- **Tunnels**: Functional teleportation tunnels allow for strategic escapes.
- **Victory/Defeat**:
  - Clear all levels or maximize score = Victory.
  - Lose all lives = Game Over screen.

## Controls

- **Movement**: WASD or Arrow keys
- **Pause**: Escape (optional)
- **Restart**: R (optional debug feature)

## Characters & AI

- **Player**: Pac-Man
- **Enemies**: 4 Ghosts with distinct AI:
  - **Blinky**: Directly chases Pac-Man.
  - **Pinky**: Aims 4 tiles ahead of Pac-Man’s direction.
  - **Inky**: Uses Blinky’s position + Pac-Man’s direction for triangulation.
  - **Clyde**: Chases if far away; scatters to corner if close.
- **Frightened Mode**: Ghosts turn vulnerable after Power Pellet is eaten.

## Visuals

- **Style**: Retro 2D grid-based maze with colorful sprites.
- **UI**: Includes Start Screen, Game Over screen, score display, and lives tracker.
- **Fruit Icons**: Display current level with corresponding fruit.
- **Ghosts**: Visual indicators for frightened state and unique colors per ghost.

## Sound (Planned)

- Start sound, pellet eating, power-up collected, ghost eaten, player death, etc.

## Milestones

1. **UI & Map Setup**  
   - Designed the maze layout and grid system  
   - Added UI elements: start screen, game over screen, score, and lives display

2. **Ghost Behaviors & AI**  
   - Implemented modular AI strategies for all four ghosts  
   - Integrated frightened mode and scatter logic

3. **Pac-Man & Game Logic**  
   - Core movement and collision  
   - Score system, power-ups, fruit, level progression  
   - Reset and round transition logic

## Arcade Consideration

The game is designed to be playable in an arcade environment. Inputs are mapped for joystick and buttons, and gameplay can be looped for continuous play. Final testing and calibration for the arcade hardware will be conducted later.

