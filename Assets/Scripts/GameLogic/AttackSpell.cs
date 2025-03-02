using System;
public class AttackSpell : ActiveSpell
{

    public AttackSpell(int fluxC, StatBlock myStats, StatBlock theirStats, int range, int charges, int baseChance, Dice d) :
        base(fluxC, myStats, theirStats, range, charges, baseChance, d)
    {


    }



    public bool rollAttackSpell(StatBlock defendStats, StatBlock attackStats, int modifier)
    {
        //check if it should have fired
        if (chargesPerTurn <= chargesThisTurn)
        {
            Console.WriteLine("ATTACKSPELL FIRING TOO OFTEN THIS TURN");
        }
        chargesThisTurn++;

        return base.spellContest(defendStats, attackStats, modifier);
    }

    public bool canFire()
    {
        if (chargesPerTurn > chargesThisTurn)
        {
            // TODO: add Flux cost.
            // TODO: add range calculation
            return true;
        }

        return false;
    }
}
