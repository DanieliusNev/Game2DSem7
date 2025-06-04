# Milestone 1: Core Foundations — UI, Map & Pellets

Our first milestone focused on establishing a stable foundation for our Pac-Man-inspired game by building core game systems and user interface flows. These essentials included the playable environment, interactive elements like pellets and fruit, and responsive UI transitions across game states.

## Project Goals for Milestone 1

The primary objective was to implement a tile-based map system to guide Pac-Man’s movement, support player interaction through various types of pellets, and integrate a user interface that would communicate game state and score clearly to the player. This early work would serve as the scaffolding for future gameplay mechanics and AI behaviors.

## Implemented Systems

### 1. Scene Setup & Collision Layers

We began with organizing the Unity scene and defining game object layers. Using Unity’s Layer Collision Matrix, we ensured that Pac-Man, ghosts, walls, pellets, and other elements only interacted where necessary. This reduced unnecessary physics checks and improved in-game responsiveness. Having a solid collision foundation early on was crucial to avoid downstream bugs in movement and AI.

### 2. Maze, Pellets, and Nodes via Tilemaps

With the collision groundwork laid, we built out the core map using multiple tilemaps. The Maze Tilemap defined impassable walls, while the Pellets Tilemap allowed us to efficiently place hundreds of collectibles across the grid. In parallel, a Node system (`Node.cs`) was implemented at intersections. These nodes dynamically detect available directions via raycasting, acting as navigation waypoints used later by both Pac-Man and ghost AI to make decisions.

### 3. Pellet Mechanics

Pellets were implemented as interactive objects that react to Pac-Man’s collisions. The base `Pellet.cs` script awards 10 points and is removed from play upon collection. To expand gameplay, we added two more pellet types. The `PowerPellet.cs` grants Pac-Man the ability to frighten ghosts for 8 seconds, changing their state and behavior. The `SpeedPellet.cs`, a more dynamic element, gives a temporary speed boost and visually marks Pac-Man with a color shift, using a coroutine-driven timer. Each pellet is handled via Unity's `OnTriggerEnter2D` and communicates back to the `GameManager` for scoring and logic control.

### 4. Game Manager

The `GameManager.cs` script ties together game logic and handles multiple responsibilities. It manages score and lives, tracks how many pellets have been collected, and handles ghost resets and fruit spawning. The game progresses through rounds, automatically advancing to the next level once all pellets are cleared. Additionally, it orchestrates transitions between win, loss, and new rounds while keeping track of the current level and adapting ghost speed accordingly.

### 5. User Interface

To provide smooth transitions and feedback throughout gameplay, a series of UI panels were created. The Start Screen appears at launch, displaying the game title and a Play button. Upon defeat, the Game Over Screen is shown, revealing the final score and a Restart option to replay. If the player wins by clearing all levels, a Win Screen summarizes their performance and offers a Replay button. Real-time updates are provided for score and lives using TextMeshPro elements. The `FruitDisplay.cs` script helps show which fruit corresponds to the current level by dynamically adjusting sprites.

## Technical Challenges

* **Collision Matrix Setup**: Balancing physics interactions without overloading the system required trial and error.
* **Node Logic**: Implementing a reliable node detection system to feed future AI logic took careful calibration of raycasts and obstacle layers.
* **Pellet and Fruit Management**: Coordinating pellet states across levels and triggering fruit appearances only once per round demanded tightly controlled state tracking.
* **UI Transitions**: Ensuring UI panels didn’t overlap or behave unpredictably between game states (like restarting mid-animation) was a key focus in polishing user experience.

This foundation has positioned us well for the next stage: developing the ghost AI logic and integrating real-time enemy behaviors that elevate the gameplay tension and challenge.
