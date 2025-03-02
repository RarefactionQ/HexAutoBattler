using System;
using UnityEngine;

public abstract class ActiveSpell
{

    protected int fluxCost;
    protected StatBlock relevantStatsforDefender; //Stats that impact the Defender
    protected StatBlock relevantStatsforAttacker; //Stats that impact the Attacker

    protected int myRange; //How far the spell covers
    protected int chargesPerTurn; //How many attempts per turn does this Spell get
    protected int chargesThisTurn; //Attempts this turn thus far
    protected int spellBaseConstant; //How much is automatically added to the Defender (this will sometimes be negative)
    protected Dice dice;

    public ActiveSpell(int fluxC, StatBlock defendStats, StatBlock attackStats, int range, int charges, int baseChance, Dice d)
    {
        fluxCost = fluxC;
        relevantStatsforDefender = defendStats;
        relevantStatsforAttacker = attackStats;
        myRange = range;
        chargesPerTurn = charges;
        chargesThisTurn = 0;
        spellBaseConstant = baseChance;
        dice = d;

        validateStats();
    }

    public void resetCharges()
    {
        chargesThisTurn = 0;
    }

    public bool spellContest(StatBlock defendStats, StatBlock attackStats, int modifier) // Modifier is any constant NOT represented by spellBaseConstant and is applied to Defender
    {

        int defendTotal = 0;
        int attackTotal = 0;

        defendTotal += modifier;
        defendTotal += spellBaseConstant;

        for (int i = 0; i < defendStats.getStats().Length; i++)
        {

            if(relevantStatsforDefender.getStats()[i] == 1)
            {
                defendTotal += defendStats.getStats()[i];
            }

            if (relevantStatsforAttacker.getStats()[i] == 1)
            {
                attackTotal += attackStats.getStats()[i];
            }

        }

        int roll = dice.roll();
        defendTotal += roll;

        if (defendTotal > attackTotal) //Tie goes to the attacker
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void validateStats()
    {
        for (int i = 0; i < relevantStatsforDefender.getStats().Length; i++)
        {
            int defendTemp = relevantStatsforDefender.getStats()[i];
            int attackTemp = relevantStatsforAttacker.getStats()[i];

            if(defendTemp != 0 && defendTemp != 1)
            {
                Debug.Log("MALFORMED RELEVANT STAT ARRAY DEFENDER \n"+string.Join(",",relevantStatsforDefender.getStats()));
                throw new Exception();
            }

            if (attackTemp != 0 && attackTemp != 1)
            {
                Debug.Log("MALFORMED RELEVANT STAT ARRAY ATTACKER");
                throw new Exception();
            }
        }
    }

}
