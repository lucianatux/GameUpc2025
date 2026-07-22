# Ricardo, el que come y no convida

A **2D action-adventure** built in Unity (C#) by a team of four. Ricardo fights his way through a body-horror digestive system — rooms full of enemies, locked doors, keys, and a cake boss — in a room-based progression with waves and checkpoints.

**My role: Producer + Programmer.** I ran production end to end — boards, milestones, task breakdown and Git branch coordination — and programmed alongside the team.

---

## Architecture

Around 70 C# scripts organised by domain (`Player`, `EnemiesScripts`, `DoorsScripts`, `EventsManagers`, `Environment`, `UI`, `Utils`), built on four design patterns applied where each actually earned its place:

**State pattern — enemy and boss AI.** `IEnemyState` with `EnterState` / `UpdateState`, implemented as Waiting, Chase and Attack states. `BossAI` inherits from `EnemyAI` and overrides `InitializeStates` to swap in its own state set, so the boss reuses the base AI loop while behaving differently — charge attacks, cherry-bomb projectiles, phase changes.

**Strategy pattern — player abilities.** `IAttackAbility` exposes a single `UseAbility(origin, direction)`. `MeleeAbility` and `FireballAbility` implement it, and `AbilityController` drives whichever is equipped with independent cooldowns. The fireball is gated behind an unlock flag, so a new ability is a new class rather than a new branch in the controller.

**Observer pattern — decoupled audio and reactions.** Three event managers (`PlayerEventsManager`, `EnemiesEventsManager`, `EnvironmentEventsManager`) publish gameplay events. `SoundManager` subscribes to all of them instead of being called directly, so nothing in the gameplay code needs to know audio exists. Same channel drives animations and UI reactions.

**ScriptableObjects — content without code.** `ThoughtSO` holds the narrative "thought" lines (text, duration, show-once flag) as assets, and `SoundData` holds per-clip audio config. Designers add content in the inspector; no recompile.

## Systems

- **Room-based progression** — `RoomManager` tracks the current room, enemy count and cleared state, firing `OnRoomEntered` / `OnRoomCleared` / `OnCallWaves` events that drive wave spawning, checkpoints and boss activation
- **Doors and keys** — `IDoor` abstraction over single-key, multi-key and auto-opening variants, with a player inventory
- **NavMesh enemy pathing**, teleport nodes, respawn from last checkpoint, life orbs, minimap toggle, pause and menu flow
- **Boss encounter** with its own state machine, health UI and death sequence

## Stack

Unity 2022.3.43f1 · C# · NavMesh · Unity Animator (state machines per character) · Git with coordinated branching across four contributors

---

Built by a team of four · 
Development: Lautaro Gabriel, Luciana Caminos Cano, Simón Frías
Art: Camila Boess, Luciana Caminos Cano
Game Design: Everyone

Production: Luciana Caminos Cano

Córdoba, Argentina · [LinkedIn](https://linkedin.com/in/lucianacaminos) · [itch.io](https://tuxiara.itch.io)
