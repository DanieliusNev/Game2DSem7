# Milestone 1: Core Foundations — UI, Map & Pellets

Our first milestone centered around establishing the core infrastructure of our Pac-Man-inspired game. We focused on designing the playable environment, implementing pellet collection, and building a clean user interface to support gameplay flow.

## Project Goals for Milestone 1

The goal was to implement a tile-based movement system for Pac-Man, enable player interaction through different pellet types, and provide clear UI elements to track game progress. This work served as the base for all upcoming game logic and AI features.

## Implemented Systems

### 1. Scene Setup & Collision Layers

We began by structuring Unity’s scene and configuring the Layer Collision Matrix. This setup allowed walls, pellets, Pac-Man, and ghosts to interact selectively, optimizing physics performance and reducing errors in movement and interactions.

### 2. Maze, Pellets, and Node System

The Maze Tilemap defines the impassable walls. A separate Pellets Tilemap was used to populate the grid with collectibles. We also built a custom node system (`Node.cs`) that identifies valid directions at intersections using raycasts, forming the basis for movement and AI navigation.

### 3. Pellet Mechanics

Three pellet types were implemented. `Pellet.cs` awards 10 points and disappears on contact. `PowerPellet.cs` triggers frightened mode in ghosts for 8 seconds. Each uses trigger events and is processed by `GameManager`.

### 4. Game Manager

The `GameManager.cs` handles scoring, lives, and level flow. When all pellets are collected, it advances to the next round. After 10 levels, the game concludes with a Win Screen. It also controls fruit spawning mid-round and handles state resets across levels.

### 5. User Interface

We implemented three core panels: Start, Game Over, and Win screens. Each offers essential feedback and options like Play or Restart. Score and lives are updated in real time using TextMeshPro. The `FruitDisplay.cs` manages fruit sprites based on level progression.

## Technical Challenges

Setting up the collision matrix required precision to ensure accurate interactions without unnecessary physics checks. Implementing node-based navigation brought challenges with raycasting and consistent direction detection at intersections. Pellet tracking needed careful control to manage state and trigger fruit spawns correctly. Finally, UI transitions had to be cleanly coordinated to avoid overlap and provide smooth feedback across game states.

This milestone provided the essential systems to support more complex game logic in future stages.
