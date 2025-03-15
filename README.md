# Sync Dash

## Overview
Sync Dash is a hyper-casual game developed in Unity, featuring real-time state synchronization, shader effects, and performance optimization. The game simulates network syncing locally by mirroring player movements on the left side of the screen.


## Gameplay Preview
[Watch Gameplay Video](/Assets/Gameplay%20Recording/Gameplay.mp4)  

### Playing the Game
- Tap the screen to make the cube jump.
- Avoid obstacles and collect orbs.
- Observe the left-side mirroring the player's actions.
- Keep playing as the game speed increases!

## Game Concept
- The right side of the screen is controlled by the player.
- The left side mirrors the player's movements in real-time, simulating a networked opponent.
- The player controls a glowing cube that moves forward automatically.
- Tapping the screen makes the cube jump to avoid obstacles and collect glowing orbs.
- The game speed increases over time.

## Features
### 1. Core Gameplay
- **Auto-movement**: The player-controlled cube moves forward automatically.
- **Tap to jump**: Avoid obstacles and navigate the level.
- **Collect glowing orbs**: Earn points by collecting orbs.
- **Real-time mirroring**: The left side mimics the player's actions with simulated network sync.
- **Scoring system**: Score is based on distance traveled and orbs collected (10 points per 50m distance travlled).
- **Increasing difficulty**: Game speed increases as the player progresses.

### 2. Real-Time State Synchronization
- The left-side character mirrors the player's movements, jumps, orb collection, and collisions.
- Introduces configurable lag to simulate real-world multiplayer delay.
- Uses interpolation to ensure smooth movement on the left side.
- Implements a local data structure (queue) for syncing player actions.


## Installation & Usage
### Prerequisites
- Unity 2021 or later




---
Happy Coding! 🎮🚀
