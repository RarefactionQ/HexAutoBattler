using System;
public class Dice
{

    private int size;
    private Random rnd;
    public Dice(int s)
    {
        s = size;
        rnd = new Random();
    }

    public int roll()  // Dice ALWAYS EXPLODE i.e. when they have the highest total, they reroll and add
    {
        int sum = 0;
        bool exploding = true;
        while (exploding)
        {
            int temp = rnd.Next(0, size);

            if(temp != size - 1)
            {
                exploding = false;
            }
            sum += temp;
        }

        return sum;
    }
}
