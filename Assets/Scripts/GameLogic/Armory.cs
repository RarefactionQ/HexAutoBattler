using System;
using System.Collections.Generic;
public class Armory
{
    private List<Glyph> unattachedGlyphs;
    private List<BroomFrame> unattachedBroomFrames;
    private List<Glyph> orderedGlyphs;
    private List<BroomFrame> orderedBroomFrames;
    private List<BroomFrame> orderedFullBrooms;
    private List<Broom> brooms;
    private int money;

    public Armory(int startingMoney)
    {

    }

    public void createBroom(BroomFrame frame, Glyph[] glyphs)
    {
        
        // Remove frame from list, remove Glyphs from list;
        // Throw exception if it isn't there.
    }

    public bool orderBroomFrame(BroomFrame frame)
    {
        if(frame.getCost() > money)
        {
            Console.WriteLine("Tried to order a BroomFrame that was too expensive");
            return false;
        }
        money -= frame.getCost();
        orderedBroomFrames.Add(frame);
        return true;
    }

    public void cancelBroomFrameOrder(BroomFrame frame)
    {
        if (orderedBroomFrames.Contains(frame))
        {
            Console.WriteLine("Tried to remove a BroomFrame order that doesn't exist");
            throw new Exception();
        }
        orderedBroomFrames.Remove(frame);
        money += frame.getCost();
    }

    public bool orderGlyph(Glyph glyph)
    {
        if (glyph.getCost() > money)
        {
            Console.WriteLine("Tried to order a Glyph that was too expensive");
            return false;
        }
        money -= glyph.getCost();
        orderedGlyphs.Add(glyph);
        return true;
    }

    public void cancelGlyphOrder(Glyph glyph)
    {
        if (orderedGlyphs.Contains(glyph))
        {
            Console.WriteLine("Tried to remove a Glyph order that doesn't exist");
            throw new Exception();
        }
        orderedGlyphs.Remove(glyph);
        money += glyph.getCost();
    }

    public void orderBroom()
    {
        //TODO: for convenience ordering a whole broom at once
    }

    public void fulfilOrders()
    {
        unattachedBroomFrames.AddRange(orderedBroomFrames);
        orderedBroomFrames = new List<BroomFrame>();
        unattachedGlyphs.AddRange(orderedGlyphs);
        orderedGlyphs = new List<Glyph>();
    }

    public int getMoney()
    {
        return money;
    }
}
