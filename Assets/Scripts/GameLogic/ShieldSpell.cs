using System;
public class ShieldSpell : ActiveSpell
{

    public ShieldSpell(int fluxC, StatBlock myStats, StatBlock theirStats, int range, int charges, int baseChance, Dice d) :
        base(fluxC, myStats, theirStats, range, charges, baseChance, d)
    {


    }



    public bool rollShieldSpell(StatBlock defendStats, StatBlock attackStats, int modifier)
    {


        return base.spellContest(defendStats, attackStats, modifier);
    }

    public bool canFire()
    {

        // TODO: add Flux cost.
        return true;
        
    }
}
