# Traffic Jam Minigame - README

## Introduction

Thank you for your interest in the Traffic Jam Minigame project. This README provides an overview of the design and structure of the game, highlighting key features and principles adhered to during development.

### Purpose

The Traffic Jam Minigame is designed to showcase solid software design principles, including SOLID principles and common design patterns. The project focuses on creating a modular, extensible, and maintainable codebase for a car simulation minigame.

## Implementation Overview

### Player Car System

#### Mechanics:

1. **Moveable Class:**
   - **Responsibility:** Moves the object forward using physics.
   - **Interface Implemented:** IMoveable.
   - **Attributes:**
     - `stopCar`: Indicates whether the car movement should be stopped.
     - `carSpeed`: Represents the speed of the car.
   - **Methods:**
     - `MoveForward`: Moves the object forward using physics.
     - `MoveForwardToPoint`: Moves the object forward towards a specified point if the distance is greater than 1.5f.

2. **Rotatable Class:**
   - **Responsibility:** Controls the rotation of the object based on a specified position.
   - **Interface Implemented:** IRotatable.
   - **Attributes:**
     - `rotationSpeed`: Determines the rotation speed of the object.
     - `targetPoint`: Represents the target point towards which the object should rotate.
   - **Methods:**
     - `RotateTowards`: Rotates the object towards a specified position in world space. Rotation occurs only if the distance between the current object position and the mouse raycast point is greater than 0.9f.

3. **InputHandler Class:**
   - **Responsibility:** Handles user input for movement and rotation, checking in FixedUpdate() for inputs.
   - **Attributes:**
     - `CanMove`: Indicates whether the object should move.
   - **Events:**
     - `OnMove`: Triggered when the object is supposed to move.
     - `OnRotate`: Triggered when the object is supposed to rotate.
   - **Methods:**
     - `FixedUpdate`: Checks for user input and mouse position, triggers move and rotate events accordingly.

4. **DetectBlueTrafficCollision Class:**
   - **Responsibility:** Detects hits on Blue car game objects.
   - **Attributes:**
     - `hitBill`: The amount of money subtracted from `gameManager.playerMoneyCount` when a collision occurs.
     - `hitEffect`: `ParticleSystem` for the hit NPC car effect.
   - **Methods:**
     - `OnCollisionEnter`: Handles collision events, deducts money, updates UI, plays hit effect, and deactivates the collided car.
     - `PlayHitEffect`: Instantiates and plays the hit effect at a given position.

### Design Principles Explanation

#### Single Responsibility Principle (SRP):

The Player Car System adheres to SRP by breaking down the logic into separate classes (Moveable, Rotatable, InputHandler, and DetectBlueTrafficCollision), each focusing on a specific responsibility. This ensures a more maintainable and understandable codebase.

#### Open-Closed Principle (OCP):

The use of interfaces (IRotable and IMoveable) follows the Open-Closed Principle, allowing for the extension of behavior without modifying existing code. This promotes a scalable and adaptable design.

#### Interface Segregation Principle (ISP):

The ICar interface, combining IRotable and IMoveable, adheres to ISP by ensuring that implementing classes only need to provide functionalities relevant to them. This avoids unnecessary dependencies and ensures classes are not burdened with methods they do not require.

### Blue Traffic Cars/NPCs

#### System Design Overview:

The logic for Blue Traffic Cars (NPCs) adheres to SOLID principles, emphasizing modularity, extensibility, and readability.

#### Single Responsibility Principle (SRP):

The SRP is honored by dividing the Blue Traffic Cars' logic into distinct classes (Rotable, Moveable, TrafficDetector, and BlueTrafficCar), each with a specific responsibility.

#### Advantages of the Design:

- **Modularity:** Each class has a clear responsibility, contributing to a modular and maintainable codebase.
- **Extensibility:** Interfaces and event-based communication allow for easy extension and addition of new features.
- **Readability:** The code structure and use of interfaces enhance code readability, making it easier to understand and maintain.

### Traffic Spawner

#### System Design Overview:

The Traffic Spawn system implements the Object Pool pattern to efficiently spawn traffic, enhancing performance by avoiding costly Instantiate and Destroy operations.

#### Object Pool Pattern Explanation:

The Object Pool pattern optimizes resource usage by reusing pre-created objects instead of creating and destroying them dynamically. This leads to improved performance and responsiveness.

#### Advantages of Object Pooling in Traffic Spawn System:

- **Reduced Resource Overhead:** Minimizes strain on system resources by avoiding frequent instantiation and destruction of traffic objects.
- **Optimized Memory Usage:** Keeps a fixed number of objects in the pool, optimizing memory usage and preventing unnecessary allocation and deallocation.
- **Smoother Performance:** Ensures a smoother traffic spawning process by reusing existing objects, reducing the likelihood of performance spikes.
- **Scalability:** Provides a scalable solution that adapts to varying levels of traffic, accommodating changing demands without sacrificing performance.

### Money System

#### Design Overview:

The Money System consists of three classes (MoneySpawner, MoneyValue, and MoneyRotable), handling the spawning, collection, and rotation of money objects within the game.

#### Key Concepts and Patterns Used:

- **Object Pool Pattern:** Efficiently manages the instantiation and deactivation of money objects to avoid the overhead of constant instantiation and destruction.

#### Overall:

The Money System is well-structured, providing an engaging and visually appealing experience for players collecting money objects. The use of the Object Pool pattern contributes to the system's efficiency and performance.

### Utility System

#### Overview:

The Utility System consists of two utility classes (RaycastUtility and StringUtility) providing common functionality for raycasting operations and string formatting, respectively.

#### Advantages of the Utility System Design:

- **Reusability:** Both utility classes provide reusable functions that can be used across different parts of the game without duplicating code.
- **Readability:** The utility classes encapsulate specific functionalities, making the code more readable and modular.
- **Consistent Formatting:** The StringUtility class ensures consistent and user-friendly formatting of money values throughout the game.
- **Ease of Maintenance:** Centralizing common functionalities in utility classes simplifies maintenance and updates.

### SceneTimer Class

#### Overview:

The SceneTimer class manages and displays the countdown timer for a game scene, updating it each frame and invoking events when the timer reaches zero.

#### Advantages:

- **Modularity:** The class is focused on a specific responsibility – managing and displaying the countdown timer.
- **Unity Events:** Utilizes Unity Events for extensibility, allowing additional actions to be triggered when the timer reaches zero.
- **Readability:** Clear method names and comments enhance code readability.
- **Initialization Handling:** The timer is properly initialized in the Start method.

### GameManager Class

#### Overview:

The GameManager class serves as a central hub for managing various aspects of the game, following the singleton pattern to ensure a single instance throughout the game.

#### Responsibilities:

- Player Money Management.
- Player Car Management.
- NPC Management.
- UI Element References.
- Level Time Management

.
- Singleton Implementation.

#### Advantages:

- Centralized Management.
- Singleton Pattern.
- Readability.
- Flexibility.
- Initialization Handling.

## Conclusion

The Traffic Jam Minigame project demonstrates a commitment to SOLID principles, design patterns, and best practices in object-oriented design. The thoughtful application of these principles results in a flexible, scalable, and maintainable codebase for a car simulation minigame. Feel free to explore the codebase, and we welcome any feedback or contributions.

Thank you for your interest and happy coding!
