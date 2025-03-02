using System;
using System.Collections.Generic;
using HexBoardGame.Runtime;
using System.Linq;


public class BroomShapeGraph
{

    private Shape baseGraph;
    private Shape[] embeddedGlyphs;
    private HashSet<Hex> occupied;
    private bool mutable;

    public BroomShapeGraph(Shape based)
    {
        baseGraph = based;
        embeddedGlyphs = new Shape[0];
        mutable = true;
        occupied = new HashSet<Hex>();
    }

    public BroomShapeGraph(Shape based, Glyph[] glyphs)
    {
        Shape[] shapes = new Shape[glyphs.Length];

        for(int i = 0; i < glyphs.Length; i++)
        {
            shapes[i] = glyphs[i].GetShape();
        }

        baseGraph = based;
        embeddedGlyphs = shapes;
        mutable = false;
        markHexAsOccupied();

        //TODO: check if this is valid;
    }

    private void markHexAsOccupied()
    {
        for (int i = 0; i < embeddedGlyphs.Length; i++)
        {
            Hex[] temp = embeddedGlyphs[i].getHexes();
            occupied.UnionWith(temp);
        }
    }

    public bool willItFit(Shape newOne)
    {
        Hex[] baseHexes = baseGraph.getHexes();
        Hex[] newHexes = newOne.getHexes();

        for(int i = 0; i < newHexes.Length; i++)
        {
            if(!baseHexes.Contains(newHexes[i]) || (occupied.Contains(newHexes[i])))
            {
                return false;
            }
            
        }
        return true;

    }

    public bool addNewShape(Shape newOne)
    {
        if (!willItFit(newOne)) {return false;}

        Hex[] newHexes = newOne.getHexes();

        for (int i = 0; i < newHexes.Length; i++)
        {
            occupied.Add(newHexes[i]);
        }

        Shape[] temp = { newOne };
        Shape[] newEmbed = embeddedGlyphs.Concat(temp).ToArray(); //This probably doesn't work!
        embeddedGlyphs = newEmbed;
        return true;
    }

}
