# Magic Broom Autobattler - Development State

## ⚠️ High-Level Architect Notes
* **Hex System:** Using `HexBoardGame.Runtime` (Odd-R Offset).
* **Combat Math:** AttackerStat - DefenderStat + ExplodingDice - K.

## ✅ Completed Features
* [Date]: Basic Hex Grid generation and UI Snapping.
* **Core Stat System Refactor:** Refactored `StatBlock.cs` to use 7 explicit integer fields (Divination, Illusion, Enchantment, Evocation, Abjuration, Transmutation, Conjuration) instead of int[] array. Updated all constructors and `sumOfBlocks` method. Updated `Dice.cs` roll() method to be recursive with exploding dice logic (rolls again and adds (result - 1) when roll equals size). Created `CombatMath.cs` static class with `ResolveContest` method implementing: attackerSum - (defenderSum + K + die.roll()) >= 0.

## 🛠 Current Focus
* (None specified)

## ❌ Known Bugs / Technical Debt
* (None currently)