# Yes Chef! 🍳

A 3D top-down cooking game developed in Unity.

## 🎮 Game Overview

Yes Chef! is a time-based kitchen management game where the player prepares ingredients and fulfills customer orders before the 3-minute game session ends.

The player can:

- Move around the kitchen using WASD
- Pick up ingredients from the refrigerator
- Chop vegetables
- Cook meat using two stove slots
- Deliver prepared ingredients to customers
- Discard unwanted ingredients using the trash bin
- Manage multiple customer orders simultaneously
- Earn points based on completed orders and preparation time

## 🛠️ Built With

- **Unity:** 6000.3.10f1
- **Language:** C#
- **Platform:** Windows PC
- **3D:** Unity primitives and custom game objects
- **UI:** Unity Canvas + TextMeshPro

## 🎯 Controls

| Input | Action |
|---|---|
| `W A S D` | Move |
| `E` | Interact |
| `1` | Select Vegetable |
| `2` | Select Cheese |
| `3` | Select Meat |
| `Mouse` | UI interaction |

## 🍅 Ingredients

The game contains three ingredient types:

- **Vegetable** — requires 2 seconds of chopping
- **Cheese** — ready to use immediately
- **Meat** — requires 6 seconds of cooking

The stove supports two simultaneous cooking slots.

## 👨‍🍳 Orders

Each customer receives a random order containing 2–3 ingredients.

Orders can contain duplicate ingredients.

Ingredients must be prepared before they can be delivered when preparation is required.

After completing an order, a score popup is displayed and a new order is generated after a short delay.

## 🏆 Scoring

Each ingredient contributes to the order's base score:

- Vegetable: 20 points
- Cheese: 10 points
- Meat: 30 points

The final order score is calculated by subtracting the number of elapsed seconds from the ingredient score.

The highest score is saved using Unity `PlayerPrefs`.

## ⚙️ Main Systems

### Player & Interaction
- CharacterController-based player movement
- Proximity-based interaction system
- Common `IInteractable` interface for interactable stations

### Ingredient System
- Ingredient type management
- Prepared/unprepared states
- Separate object pools for vegetables, cheese, and meat

### Cooking System
- Vegetable chopping timer
- Two-slot meat cooking system
- Prepared ingredient visual states

### Order System
- Random order generation
- Support for duplicate ingredients
- Individual customer order timers
- Order completion and respawn system

### Game Management
- 3-minute game timer
- Pause and resume
- Game Over state
- Play Again functionality
- Return to Main Menu
- Persistent high score

│   └── ui/
├── Prefabs/
└── ...
