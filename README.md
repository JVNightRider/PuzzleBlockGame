# PuzzleBlock

Match-style puzzle game made in Unity. The goal is to remove contiguous groups of bricks of the same type before running out of moves.

## Requirements

- Unity Editor: 6000.3.11 LTS
- Scripting Runtime: .NET 4.x Equivalent (project targets .NET Framework 4.7.1)

## Quick start

1. Clone the repository:

   git clone https://github.com/JVNightRider/PuzzleBlockGame

Note: Set "Development" as the current branch, you can use `git checkout Development`
   
3. Open Unity Hub and select `Add` -> the project folder.
4. Open the Main Scene (or create a new scene and set up the objects below):
   - `GameplayManager` — assign `PuzzleBlockSettings`, `UIController`, and `PuzzleHandler`.
   - `PuzzleHandler` — assign the `EventSystem`, the list of `GameBrick` instances and `brickImages`.
   - `UIController` — assign `movesText`, `scoreText` and `GameOverPopup`.
5. Press Play to run the game.

## Controls & gameplay

- Click bricks to interact.  
- Groups of 2 or more adjacent bricks of the same type are removed. Columns collapse and new bricks spawn at the top.  
- Each removal consumes one movement. The game ends when movements reach zero.

## Project layout

- `Assets/PuzzleBlock/Scripts/Gameplay` — Core game logic (`GameplayManager`, `PuzzleHandler`, `GameBrick`).  
- `Assets/PuzzleBlock/Scripts/UI` — UI controllers (`UIController`, `GameOverPopup`).  
- `Assets/PuzzleBlock/Scripts/Settings` — `PuzzleBlockSettings` (ScriptableObject).

## Technical notes

- `PuzzleHandler` uses a recursive search to detect contiguous bricks and applies column gravity after removals.  
- The `EventSystem` is temporarily disabled while the board is reconfigured to avoid input race conditions.

## Building

- Application Target is Android.
- Build from Unity: File > Build Settings. Select target platform and add the main scene.

## Contributing

1. Create a branch: `git checkout -b issue-<number>-short-description`.  
2. Keep commits focused and descriptive.  
3. Open a Pull Request against `Development`.
