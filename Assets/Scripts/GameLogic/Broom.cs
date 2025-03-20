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

    public Broom(Glyph[] g, BroomFrame myFrame)
    {
        glyphs = g;
        frame = myFrame;
        graph = new BroomShapeGraph(frame.getShape());

        // Add all glyphs to the shape graph
        foreach (var glyph in glyphs)
        {
            graph.addNewShape(glyph.GetShape());
        }

        setSumStats();
        setWeightClass();
    }

    private void setSumStats()
    {
        StatBlock[] blocks = new StatBlock[glyphs.Length];
        for (int i = 0; i < glyphs.Length; i++)
        {
            blocks[i] = glyphs[i].GetStatBlock();
        }
        sumStats = StatBlock.sumOfBlocks(blocks);
    }

    private void setWeightClass()
    {
        weight = 0;
        for (int i = 0; i < glyphs.Length; i++)
        {
            weight += glyphs[i].getWeight();
        }

        if (frame.getMaxWeight() < weight)
        {
            Console.WriteLine("Broom is too heavy to be possible!!!");
            throw new Exception("Broom is too heavy to be possible!");
        }

        weightClass = frame.getWeightClass(weight);
    }

    // Add some helper methods for debugging
    public int GetGlyphCount()
    {
        return glyphs != null ? glyphs.Length : 0;
    }

    public BroomFrame GetFrame()
    {
        return frame;
    }

    public StatBlock GetStats()
    {
        return sumStats;
    }

    public WeightClass GetWeightClass()
    {
        return weightClass;
    }
}