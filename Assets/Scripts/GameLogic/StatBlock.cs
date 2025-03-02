using System;
public class StatBlock
{

    // Convert this to a Dict

    private int[] stats;
    public static readonly int NUMSTATS = 8;
    public StatBlock()
    {
        stats = new int[NUMSTATS];
    }

    public StatBlock(int [] setup)
    {
        if (setup.Length != NUMSTATS)
        {
            Console.WriteLine("Statblock tried to be created with an array sized " + setup.Length);
            throw new Exception(); 
        }

        stats = setup;
    }

    public int[] getStats()
    {
        return stats;
    }

    public static StatBlock sumOfBlocks(StatBlock [] blocks)
    {
        StatBlock newBlock = null;
        int[] tempStat = new int[NUMSTATS];

        for (int i = 0; i < blocks.Length; i++)
        {
            int[] innerStat = blocks[i].getStats();
            for(int k = 0; k < NUMSTATS; k++)
            {
                tempStat[k] += innerStat[k];
            }
        }

        newBlock = new StatBlock(tempStat);
        return newBlock;
    }

}
