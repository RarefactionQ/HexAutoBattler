using System;
public class StatBlock
{
    public int Divination;
    public int Illusion;
    public int Enchantment;
    public int Evocation;
    public int Abjuration;
    public int Transmutation;
    public int Conjuration;

    public StatBlock()
    {
        Divination = 0;
        Illusion = 0;
        Enchantment = 0;
        Evocation = 0;
        Abjuration = 0;
        Transmutation = 0;
        Conjuration = 0;
    }

    public StatBlock(int divination, int illusion, int enchantment, int evocation, int abjuration, int transmutation, int conjuration)
    {
        Divination = divination;
        Illusion = illusion;
        Enchantment = enchantment;
        Evocation = evocation;
        Abjuration = abjuration;
        Transmutation = transmutation;
        Conjuration = conjuration;
    }

    public StatBlock(int[] setup)
    {
        if (setup.Length != 7)
        {
            Console.WriteLine("Statblock tried to be created with an array sized " + setup.Length);
            throw new Exception(); 
        }

        Divination = setup[0];
        Illusion = setup[1];
        Enchantment = setup[2];
        Evocation = setup[3];
        Abjuration = setup[4];
        Transmutation = setup[5];
        Conjuration = setup[6];
    }

    public int[] getStats()
    {
        return new int[] { Divination, Illusion, Enchantment, Evocation, Abjuration, Transmutation, Conjuration };
    }

    public static StatBlock sumOfBlocks(StatBlock[] blocks)
    {
        StatBlock newBlock = new StatBlock();

        for (int i = 0; i < blocks.Length; i++)
        {
            newBlock.Divination += blocks[i].Divination;
            newBlock.Illusion += blocks[i].Illusion;
            newBlock.Enchantment += blocks[i].Enchantment;
            newBlock.Evocation += blocks[i].Evocation;
            newBlock.Abjuration += blocks[i].Abjuration;
            newBlock.Transmutation += blocks[i].Transmutation;
            newBlock.Conjuration += blocks[i].Conjuration;
        }

        return newBlock;
    }

}
