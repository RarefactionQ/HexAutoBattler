This document serves as the **Technical Source of Truth** for the Magic Broom Autobattler project. It is designed to be an "Anchor" for LLMs to ensure architectural consistency as the codebase expands.

---

# TECH_SPEC: Magic Broom Autobattler

## 1. Project Architecture & Environment

* **Engine:** Unity 2022.3 LTS (Standard Render Pipeline).
* **Namespace:** `HexBoardGame.Runtime`.
* **Coordinate System:** Hexagonal Axial Coordinates and Odd-R Offset for Tilemap rendering.
* **Pattern:** **Data-Driven Architecture**. Use `ScriptableObject` for static data (Runes, Frames) and standard C# classes for runtime instances (Brooms, Spells).

---

## 2. Core Systems & Logic

### 2.1 The Stat System (Seven Schools)

All unit capabilities are derived from the **Seven Schools of Magic**. `StatBlock.cs` uses explicit integer fields for:

* **Divination:** Initiative and Detection.
* **Illusion:** Stealth and Evasion.
* **Enchantment:** Buffing and Support.
* **Evocation:** Suppression and Direct Damage.
* **Abjuration:** Area-of-Effect Negation and Defense.
* **Transmutation:** Mobility, Movement, and Charges.
* **Conjuration:** Summoning and Board Presence.

### 2.2 Combat Resolution Math

Combat is handled by the `CombatMath` utility. Every contest (Attack, Counter, Shield) follows the **Exploding Contest Formula**:

* **:** Success.
* **:** Failure.
* **:** A constant specific to the spell or mission.
* **:** Dice roll logic handled by `Dice.cs`. If a die rolls its maximum value (), it rolls again and adds  recursively:



---

## 3. Data Definitions

### 3.1 Glyphs & BroomFrames

* **Glyph:** Contains a `Shape` (hex array), a `StatBlock`, and an optional `ActiveSpell`.
* **BroomFrame:** Defines the "Board" for the Designer. It has a `Shape` (the frame), `FluxCapacity`, and `WeightCapacity` (Light, Medium, Heavy thresholds).
* **Broom:** A runtime object composed of 1 `BroomFrame` and  `Glyphs`. Total stats are the sum of all components.

### 3.2 Weight Categories

Performance is modified by the total weight of Glyphs vs. the Frame's capacity:

* **Light:** +50% Speed/Agility, -50% Fuel Consumption.
* **Medium:** Standard base stats.
* **Heavy:** -50% Speed/Agility, +50% Fuel Consumption.

---

## 4. Battle Sequence Logic (Planned)

The `BattleSequenceManager` executes combat in three discrete phases:

1. **Countermagic Phase:** The Defender may trigger a `CounterSpell` contest to cancel the attack entirely.
2. **Hit Phase:** The Attacker runs a `CombatMath` contest against the Defender's evasion stats.
3. **Shield Phase:** If a hit occurs, the Defender may trigger a `ShieldSpell` contest to negate or reduce the damage/effect.

---

## 5. Behavior & AI (Roles and Regions)

Unit behavior is a function of their **Role** and their **Region** on the grid.

* **Roles:** Shock, Skirmish, Escort, Support, Raider, Remote.
* **Regions:** Behind Friendly Lines, Friendly Lines, Enemy Lines, Behind Enemy Lines.
* **Calculation:** Roles define the destination/target; Regions provide flat stat modifiers (e.g., +Suppression in Enemy Lines).

