using System;
public class Dice
{

    private int size;
    private Random rnd;
    public Dice(int s)
    {
        size = s;
        rnd = new Random();
    }

    public int roll()  // Dice ALWAYS EXPLODE i.e. when they have the highest total, they reroll and add
    {
        int result = rnd.Next(1, size + 1);
        
        if (result == size)
        {
            // Exploding dice: roll again and add (result - 1)
            return result + roll() - 1;
        }
        
        return result;
    }
}
