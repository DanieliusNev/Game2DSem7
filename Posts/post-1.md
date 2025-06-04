# Roll-a-Ball: A Brief Technical Description of Our First Unity Game

Creating the "Roll-a-Ball" game was our first collaborative hands-on experience with Unity and served as an introduction to 3D game development using the Unity 2022.3 engine. While one of us had some prior experience with Unity from the previous semester, this project allowed both of us to work together, share knowledge, and reinforce fundamental concepts. The game itself is small but complete, involving a player-controlled ball that collects objects scattered around a plane. Although simple in gameplay, the development process provided us with insight into key Unity features such as GameObjects, physics components, C# scripting, and basic UI.

The initial setup of the game environment was straightforward. After launching Unity and creating a new 3D project, we began by constructing the basic scene. The game area consisted of a plane, which acted as the ground, and a sphere, which served as the player-controlled ball. To simulate realistic movement, we added a Rigidbody component to the sphere, enabling it to react to physics forces.

To handle input and movement, we wrote a C# script called `PlayerController.cs`. This script used Unity’s input system to capture player input from the keyboard and applied force to the Rigidbody to move the ball. This was done within Unity’s `FixedUpdate()` function to ensure smooth physics-based movement. A key lesson here was understanding the difference between `Update()` and `FixedUpdate()`, and why physics calculations should be handled in the latter.

Next, we built the collectibles small cubes positioned around the plane. We tagged these cubes as "PickUp" and added a trigger collider to detect when the ball touches them. In the same player script, we implemented collision logic using `OnTriggerEnter()` to deactivate the cubes when collected. This was our first experience working with Unity’s tag and trigger system, which are essential for object interaction.

To make the game visually dynamic, we created a `Rotator.cs` script and attached it to the pickup cubes. This made the cubes rotate continuously, giving them a more engaging appearance. After creating a single pickup cube, we turned it into a prefab, allowing us to efficiently place multiple copies across the scene while retaining their behavior and appearance.

The user interface component was relatively simple but essential. We added a UI text element that updated the player’s score as they collected pickups. The same UI system was used to display a win message when all items were collected. These UI elements introduced us to canvas systems, text updates, and conditional logic based on game state.

Lastly, we used Unity’s build settings to compile the game into a standalone executable. This involved selecting the correct platform and building the project, giving us experience with the final step of deploying a game.

Overall, the Roll-a-Ball project was a valuable first experience in Unity development. It introduced us to core systems like GameObjects, physics, scripting, prefabs, and UI — all fundamental building blocks for more complex game projects. Completing this project gave us a solid foundation and the confidence to start designing and building our own game for the VIA Arcade machine in the XR Lab.

