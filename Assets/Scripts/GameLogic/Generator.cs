using System;
using HexBoardGame.Runtime;

public class Generator
{
    private ResearchTree fakeTree;
    private int startingMoney;

    public Generator()
    {
        fakeData();
        startingMoney = 100;
    }

    private void fakeData()
    {

        // Glyph research categories
        ResearchCategory infernoMagic = new ResearchCategory("infernoMagic", 0, 10);
        ResearchCategory fireMagic = new ResearchCategory("fireMagic", 1, 3);
        ResearchCategory level1Magic = new ResearchCategory("level1Magic", 2, 3);
        ResearchCategory baseMagic = new ResearchCategory("baseMagic", 3, 1);


        // Broom research categories
        ResearchCategory supersonicBroom = new ResearchCategory("supersonicBroom", 4, 10);
        ResearchCategory speedBroom = new ResearchCategory("speedBroom", 5, 3);
        ResearchCategory baseBroom = new ResearchCategory("baseBroom", 6, 1);

        ResearchCategory[] myCategories = { infernoMagic, fireMagic, level1Magic, baseMagic, supersonicBroom, speedBroom, baseBroom };

        //Glyph Technologies

        //Firebolt -- active spell
        Hex[] hexes = { new Hex(0, 0, 0), new Hex(-1, 0, 1), new Hex(-1, -1, 2)}; //This is a v
        StatBlock block = new StatBlock(new[] { 0, 0, 0, 0, 0, 0, 0, 0 });
        AttackSpell activeSpell = new AttackSpell(10, new StatBlock(new[] { 0, 0, 1, 0, 0, 0, 0, 0 }),
            new StatBlock(new[] { 0, 1, 0, 0, 0, 0, 0, 0 }), 2, 1, 0, new Dice(10));

        Glyph fireBoltGlyph = new Glyph(hexes, block, 10, 10, 10, activeSpell);
        Technology fireBoltTech = new Technology(new[] { infernoMagic, fireMagic, level1Magic, baseMagic }, 0, 1000, "fireBolt", 0,fireBoltGlyph);


        //Fire Mastery -- passive spell
        hexes = new Hex[] { new Hex(0, 0, 0), new Hex(-1, 0, 1), new Hex(-2, -0, 2) }; //This is a line
        block = new StatBlock(new[] { 0, 0, 3, 0, 0, 0, 0, 0 });
        
        Glyph fireMasteryGlyph = new Glyph(hexes, block, 10, 10, 10);
        Technology fireMasteryTech = new Technology(new[] { infernoMagic, fireMagic, level1Magic, baseMagic }, 25, 1000, "fireMastery", 0, fireMasteryGlyph);

        //Broom Technologies

        Technology[] myTechnologies = { fireBoltTech, fireMasteryTech };


        fakeTree = new ResearchTree(myCategories, myTechnologies);


    }

    public ResearchTree getFakeTree()
    {
        return fakeTree;
    }

    public int getStartingMoney()
    {
        return startingMoney;
    }
}
