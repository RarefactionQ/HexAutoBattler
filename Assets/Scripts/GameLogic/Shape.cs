using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexBoardGame.Runtime;


/*

Representation of a collection of Hex tiles in some configuration relative to itself
Can be ROTATED
myRotation = -1 means it isn't set
There are 6 possible rotations of a shape labeled 0 to 5 in clockwise order

*/
public class Shape
{
    private int myRotation = -1;
    private Hex[] filledSpaces = null;

    // Set this set of filled spaces as myRotation = 0

    public Shape(Hex[] fSpaces)
    {
        filledSpaces = fSpaces;
        myRotation = 0;

    }

    public Shape(Hex[] fSpaces, int curRotation)
    {
        filledSpaces = fSpaces;
        myRotation = curRotation;

    }

    public Hex[] getHexes()
    {
        return filledSpaces;
    }

    public void rotateClockwise()
    {
        if (myRotation == 5)
        {
            myRotation = 0;
        }
        else
            myRotation++;

        // Rotating clockwise goes froom (q,r,s) to (-r,-s-q)
        Hex[] newFilledSpaces = new Hex[filledSpaces.Length];

        for (int i = 0; i < filledSpaces.Length; i++)

        {

            newFilledSpaces[i] = new Hex(-filledSpaces[i].r, -filledSpaces[i].s, -filledSpaces[i].q);

        }

        filledSpaces = newFilledSpaces;

    }

    public void rotateCounterClockwise()
    {
        if (myRotation == 0)
        {
            myRotation = 5;
        }
        else
            myRotation--;

        // Rotating counter-clockwise goes froom (q,r,s) to (-s,-q-r)
        Hex[] newFilledSpaces = new Hex[filledSpaces.Length];

        for (int i = 0; i < filledSpaces.Length; i++)

        {

            newFilledSpaces[i] = new Hex(-filledSpaces[i].s, -filledSpaces[i].q, -filledSpaces[i].r);

        }

        filledSpaces = newFilledSpaces;

    }

    public void move(Hex target)
    {
        Hex distance = Hex.Subtract(target, filledSpaces[0]);
        for(int i = 0; i < filledSpaces.Length; i++)
        {
            filledSpaces[i] = Hex.Add(filledSpaces[i], distance);
        }
    }


}
