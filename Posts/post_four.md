# Milestone 2: Chasing Logic — Ghost AI and Behaviors

Our second milestone focused on giving life and personality to the enemies of the game—namely, the four iconic ghosts. This stage emphasized the development of movement logic, AI decision-making, and state-based behavior systems that allow each ghost to act uniquely and pose a real challenge to players.

## Project Goals for Milestone 2

The primary objective was to design and implement ghost behaviors that closely mimic the original Pac-Man experience, while maintaining modularity for future enhancement. Each ghost needed to:
- Move with intent using node-based logic
- Transition fluidly between chase, scatter, frightened, and home states
- Demonstrate distinct AI strategies based on proximity and prediction
- React dynamically to player actions and power-up effects

Achieving this meant building a flexible behavior system that could accommodate both shared and unique ghost logic.

## Implemented Systems

### 1. Ghost State Architecture

We structured each ghost using the `Ghost.cs` script, which ties together various components: `GhostChase`, `GhostScatter`, `GhostFrightened`, and `GhostHome`. Upon initialization or reset, the ghost’s behavior scripts are selectively enabled based on its current state. This state-driven architecture ensures consistent transitions—for instance, switching from chase to scatter mode, or entering the frightened state when Pac-Man consumes a power pellet.

### 2. Node-Based Navigation

Navigating the maze intelligently is crucial to the ghost experience. At every intersection (defined via `Node.cs`), ghosts evaluate their available movement options. Using raycasting, nodes detect unblocked directions and present them to the ghost. The `GhostChase.cs` script then selects the direction that minimizes the distance to a target position calculated by its current AI strategy. This makes ghost movement feel deliberate and strategic, while remaining responsive to Pac-Man's position.

### 3. Behavioral States and Transitions

Ghosts alternate between various behaviors depending on game events:
- **Home** (`GhostHome.cs`): Ghosts begin here or return after being eaten. An exit coroutine smoothly transitions them into active play.
- **Scatter**: Temporarily diverts ghosts to specific map corners, breaking up chase patterns.
- **Chase**: Activates ghost-specific pursuit logic targeting Pac-Man.
- **Frightened**: Triggered by `PowerPellet.cs`, ghosts become vulnerable and move randomly to avoid the player.

Each behavior is encapsulated as a separate component that can be toggled independently, making transitions clean and debuggable.

### 4. Individual AI Strategies

To make each ghost unique, we implemented the Strategy design pattern using `GhostChaseStrategyBase.cs`. Each ghost follows a distinct chase behavior:
- **Blinky** targets Pac-Man directly.
- **Pinky** (`PinkyChaseStrategy.cs`) targets four tiles ahead of Pac-Man’s current direction.
- **Inky** (`InkyChaseStrategy.cs`) calculates a vector between Blinky and a point ahead of Pac-Man to determine its target.
- **Clyde** (`ClydeChaseStrategy.cs`) switches between chasing Pac-Man and fleeing to a corner based on proximity.

This modular strategy system allows for easy tuning and expansion, encouraging experimentation with future enemy types or difficulty modes.

## Technical Challenges

* **Node Evaluation Accuracy**: Ensuring ghosts consistently choose valid and optimal paths at intersections required precise distance checking and raycast tuning.
* **Behavior Switching**: Managing transitions—especially from frightened back to chase—demanded careful sequencing and timing controls.
* **Ghost Diversity**: Creating behaviorally distinct yet balanced ghosts required extensive testing to avoid predictable or erratic patterns.
* **State Modularity**: Isolating state logic into separate components helped maintain clarity but increased the need for consistent state coordination.

With these AI systems in place, our ghosts are now fully operational and pose a real threat. The groundwork laid here sets the stage for Milestone 3, where Pac-Man’s responsiveness and game-wide logic will be brought to completion.

