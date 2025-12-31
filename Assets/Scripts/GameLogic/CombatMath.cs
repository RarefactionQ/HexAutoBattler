public static class CombatMath
{
    public static bool ResolveContest(int attackerSum, int defenderSum, int K, Dice die)
    {
        int dieRoll = die.roll();
        return attackerSum - (defenderSum + K + dieRoll) >= 0;
    }
}

