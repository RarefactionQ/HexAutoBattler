using System;
public class ResearchCategory
{
    private string name;
    private int id;
    private int expertise;
    private int weight;

    public ResearchCategory(string myName, int myID, int myWeight)
    {
        name = myName;
        id = myID;
        expertise = 0;
        weight = myWeight;
    }

    public void addExpertise()
    {
        expertise += weight;
    }

    public void addExpertisebyValue(int value)
    {
        expertise += value;
    }

    public int getExpertise()
    {
        return expertise;
    }

    public int getWeight()
    {
        return weight;
    }

    public int getID()
    {
        return id;
    }

    public string getName()
    {
        return name;
    }
}
