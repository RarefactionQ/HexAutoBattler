using System;
using HexBoardGame.Runtime;
public class Glyph
{

    private Shape myShape;
    private StatBlock stats;
    private ActiveSpell spell; // At most one spell per Glyph!
    private int fluxUpkeep; // Cost per turn for this Glyph to be equipped.
    private int cost;
    private int weight;

    public Glyph()
    {
        myShape = null;
        stats = new StatBlock();
    }

    public Glyph(Hex[] hexes, StatBlock block, int fluxU, int c, int w)
    {
        myShape = new Shape(hexes);
        stats = new StatBlock(block.getStats()); // Deep copy
        fluxUpkeep = fluxU;
        cost = c;
        weight = w;
    }

    public Glyph(Hex[] hexes, StatBlock block, int fluxU, int c, int w, ActiveSpell s):this(hexes,block,fluxU,c,w)
    {
        spell = s;
    }

    public Shape GetShape()
    {
        return myShape;
    }

    public StatBlock GetStatBlock()
    {
        return stats;
    }

    public bool isSpell()
    {
        return spell == null;
    }

    public ActiveSpell GetActiveSpell()
    {
        if (spell == null)
        {
            Console.WriteLine("Asked for a missing spell");
            return null;
        }
        else
        {
            return spell;
        }
    }

    public int getCost()
    {
        return cost;
    }

    public int getWeight()
    {
        return weight;
    }
}
