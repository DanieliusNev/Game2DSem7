# Milestone 2: Ghost Behavior & AI

For this milestone, we worked on getting the ghost logic fully working in our Pac-Man game. In the beginning, all the ghosts were using the same logic — they just chased Pac-Man by heading straight toward his current position. While this worked for testing, it didn’t feel like the original game and made the ghosts too similar.

We started by setting up each ghost with:
- A **collider** and **Rigidbody2D** so they can detect intersections and collide with Pac-Man.
- A **Movement script** that handles moving in a direction until they hit a node, where they can switch direction.
- Separate behavior scripts for **Chase**, **Scatter**, **Frightened**, and **Home** states.

Each behavior is a separate MonoBehaviour script that can be enabled or disabled depending on what the ghost should be doing. For example, when Pac-Man eats a Power Pellet, we disable the Chase and Scatter scripts and enable the Frightened one.

## Introducing Unique AI for Each Ghost

To make the ghosts act differently, we implemented a new system using a **base class called `GhostChaseStrategyBase`**. This allowed us to give each ghost their own custom script for targeting Pac-Man. Here’s how each one works:

- **Blinky** (Red): Just chases Pac-Man’s current position.
- **Pinky** (Pink): Targets 4 tiles ahead of Pac-Man’s direction.
- **Inky** (Blue): Uses both Pac-Man’s and Blinky’s positions to calculate a strange offset.
- **Clyde** (Orange): If close to Pac-Man, he runs away to the corner; if far, he chases.

We connect these strategies in the inspector by dragging the correct script onto each ghost’s Chase behavior. At every node, the ghost checks which directions are available and picks the one that gets it closest to its current target.

## Handling Transitions and Bugs

One of the challenges we had was making sure the ghosts switch properly between states (chase, scatter, frightened, etc.). We also had to make sure that the ghosts don’t get stuck in tunnels or spin around at corners. We fixed this by checking the ghost’s current state and only changing direction when they reach a node.

Another small challenge was assigning the strategy scripts in Unity. Because we’re using an abstract base class, we had to make sure Unity can still let us drag and drop the right script in the inspector.

## Final Result

Now each ghost feels more unique and behaves in a way that matches their role in the classic game. The AI makes the game more interesting and difficult, and the behavior switching helps make the gameplay dynamic. This milestone gave us a solid enemy system to build on for the next phase, where we’ll finish up Pac-Man’s logic and polish the full game loop.
