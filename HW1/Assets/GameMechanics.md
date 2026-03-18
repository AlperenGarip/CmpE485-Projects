# Game Mechanics — 3D Maze Escape

## Game World

The game takes place inside a **maze** built from stone blocks and corridors. The maze consists of at least **two branching paths** that split from a central fork near the player's starting position.

## Player

- A **low-poly human RPG character** controlled in **third person**.
- Movement: **WASD / Arrow Keys** to move (camera-relative).
- **Mouse** controls the camera orbit around the player.
- The player can push physics objects by walking into them.
- The player dies if:
  - They fall off the platform (kill zone below).
  - They stand on a disappearing trap tile when it vanishes.
  - They walk into an active laser beam.
  - They get too close to a patrolling guard.

## Key Object

- A **3D key prop** located at the end of **Path A**.
- Has full physics (gravity, collisions, spinning) — the player can **push it** by walking into it.
- The key must be pushed to the Door on Path B to win.
- If the key falls off the platform, the game is lost.

## Door Object

- A gate at the end of **Path B**.
- When the **Key collides with the Door** (detected via `OnCollisionEnter`), the game is won and the door slides open.

## Winning Condition

Push the Key into the Door. A **"You Win!"** panel appears with **Play Again** and **Quit** buttons.

## Traps

### Disappearing Floor Tiles
- Certain floor tiles on Path A periodically **disappear and reappear** using **Coroutines**.
- Timing: ~8 seconds visible, ~3 seconds invisible.
- If the player is standing on a tile when it disappears, they fall and die.

### Laser Beams
- Laser beams stretch wall-to-wall across the corridor, periodically turning on and off via **Coroutines**.
- Detection uses `Physics.RaycastAll` so crossing diagonally or at speed is still detected reliably.
- Touching an active laser kills the player instantly.

## Guards (Patrolling Enemies)

- **Two guards** patrol between waypoints using **Coroutines**.
- If the player comes within the guard's detection range (~2.5 units), the player dies.
- The player must observe each guard's patrol pattern and time their movement.

## Background Music

- Looping background music plays when the game starts.
- A **Music toggle button** on the win/lose panels lets the player mute/unmute.
- The music preference is saved across restarts (via `PlayerPrefs`).

## Death / Fail Condition

When the player dies, a **"You Died!"** panel appears with **Play Again** and **Quit** buttons.


## Assets & Credits

All third-party assets are from the **Unity Asset Store** and are used under the [Unity Asset Store EULA](https://unity.com/legal/as-terms).

| Asset | Publisher | Link |
|-------|-----------|------|
| Tileable Maze and Dungeon Blocks | Polytope Studio | [Asset Store](https://assetstore.unity.com/packages/3d/environments/dungeons/tileable-maze-and-dungeon-blocks-259878) |
| Free Low Poly Human RPG Character | acorn_au | [Asset Store](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/free-low-poly-human-rpg-character-219979) |
| Simple Keys | Limeware | [Asset Store](https://assetstore.unity.com/packages/3d/props/tools/simple-keys-231162) |
