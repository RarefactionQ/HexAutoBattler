using System;
public class Broom
{
    private Glyph[] glyphs;
    private BroomShapeGraph graph; //Contains all of the Glyphs
    private StatBlock sumStats;
    private BroomFrame frame;
    private WeightClass weightClass;
    private int weight;

    public Broom()
    {
    }

    // public Broom(BroomShapeGraph myGraph, etc.)

    public Broom(Glyph[] g, BroomFrame myFrame)
    {
        glyphs = g;
        graph = new BroomShapeGraph(frame.getShape(), glyphs);
        frame = myFrame;
        setSumStats();

    }

    private void setSumStats()
    {

        StatBlock[] blocks = new StatBlock[glyphs.Length];
        for(int i = 0; i < glyphs.Length; i++)
        {
            blocks[i] = glyphs[i].GetStatBlock();
        }
        sumStats = StatBlock.sumOfBlocks(blocks);
    }

    private void setWeightClass()
    {
        weight = 0;
        for(int i = 0; i < glyphs.Length; i++)
        {
            weight += glyphs[i].getWeight();
        }

        if (frame.getMaxWeight() < weight)
        {
            Console.WriteLine("Broom is too heavy to be possible!!!");
            throw new Exception();
        }

        weightClass = frame.getWeightClass(weight);
    }
    // Needs to hold Glyphs
    // Holds a BroomShapeGraph to make sure the SHAPES of the Glyphs are good
}
