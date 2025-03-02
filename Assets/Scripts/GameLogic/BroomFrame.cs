using System;

public enum WeightClass
{
    Light,
    Medium,
    Heavy
}


public class BroomFrame
{
    private class WeightCapacity
    {
        public int Light;
        public int Medium;
        public int Heavy;
        public WeightCapacity(int L, int M, int H)
        {
            Light = L;
            Medium = M;
            Heavy = H;
        }

        public WeightClass getWeightClass(int i)
        {
            if (i <= Light)
            {
                return WeightClass.Light;
            }
            else if(i <= Light + Medium)
            {
                return WeightClass.Medium;
            }
            else if(i <= Light+Medium+Heavy)
            {
                return WeightClass.Heavy;
            }
            else
            {
                Console.WriteLine("Tried to ask for a WeightClass that is too heavy!!");
                throw new Exception();
            }
        }

        public bool tooHeavy(int i)
        {
            return i > Light + Medium + Heavy;
        }
    }

    private WeightCapacity carryingCapacity;
    private int fluxCapacity;
    private int summoningCost;
    private int speed;
    private int agility;
    private int durability;
    private int cost;
    private Shape shape;

    public BroomFrame()
    {
        carryingCapacity = new WeightCapacity(-1,-1,-1);
        fluxCapacity = -1;
        summoningCost = -1;
        speed = -1;
        agility = -1;
        
    }

    public BroomFrame(int light, int medium, int heavy, int flux, int summon, int spd, int agi, int dur, int c, Shape s)
    {
        carryingCapacity = new WeightCapacity(light, medium, heavy);
        fluxCapacity = flux;
        summoningCost = summon;
        speed = spd;
        agility = agi;
        durability = dur;
        cost = c;
        shape = s;
    }

    public bool tooHeavy(int weight)
    {
        return carryingCapacity.tooHeavy(weight);
    }

    public int getMaxWeight()
    {
        return carryingCapacity.Light + carryingCapacity.Medium + carryingCapacity.Heavy;
    }

    public WeightClass getWeightClass(int weight)
    {
        return carryingCapacity.getWeightClass(weight);
    }

    public int getCost()
    {
        return cost;
    }

    public Shape getShape()
    {
        return shape;
    }

    //TODO: getters
}
