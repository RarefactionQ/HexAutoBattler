using System;
public class Technology
{
    public static readonly int RESEARCH_POINTS_PER_WEEK = 100;

    private ResearchCategory[] categories;
    private int visibilityThreshold;
    private int totalCost;
    private int researchSoFar;
    private String name;
    private int id;

    private String[] descriptions;

    private object unlockGlyphOrBroom;

    public Technology(ResearchCategory[] myCategories, int myVis, int cost, String myName, int myID, object unlock)
    {
        categories = myCategories;
        visibilityThreshold = myVis;
        totalCost = cost;
        researchSoFar = 0;
        name = myName;
        id = myID;

        unlockGlyphOrBroom = unlock;

        setUpDescriptions();
    }

    private void setUpDescriptions()
    {
        //TODO: builid this out
        descriptions = new string[4];
        descriptions[0] = name + " level 0";
        descriptions[1] = name + " level 1";
        descriptions[2] = name + " level 2";
        descriptions[3] = name + " total";

    }

    public String getLevelDescription(int level)
    {
        return descriptions[level];
    }

    public String getAppropriateDescription()
    {
        if (researchSoFar <= totalCost / 3)
        {
            return getLevelDescription(0);
        }
        else if (2*researchSoFar <= totalCost / 3)
        {
            return getLevelDescription(1);
        }
        else if(researchSoFar < totalCost)
        {
            return getLevelDescription(2);
        }
        else
        {
            return getLevelDescription(3);
        }

    }

    public ResearchCategory[] GetResearchCategories()
    {
        return categories;
    }

    public void doWeeksResearch()
    {
        int points = RESEARCH_POINTS_PER_WEEK;
        for (int i = 0; i < categories.Length; i++)
        {
            points += (points * categories[i].getExpertise())/100;
            categories[i].addExpertise();
        }

        researchSoFar += points;
    }

    public String getName()
    {
        return name;
    }

    public int getVisibility()
    {
        return visibilityThreshold;
    }
    // TODO: getter and setters

}
