using System;
using System.Linq;

public class ResearchTree
{
    private ResearchCategory[] researchCategories;
    private Technology[] allTechnologies;

    public ResearchTree(ResearchCategory[] myCategories, Technology[] myTechnologies)
    {
        researchCategories = myCategories;
        allTechnologies = myTechnologies;

    }

    public void spendWeekResearching(Technology t)
    {

        //TODO: logic for assigning expertise to the categories
        // Maybe we use the order of the categories to denote how much of a bonus to assign?

    }

    public String getResearchTreePrint()
    {
        string prettyPrint = "";
        for (int i = 0; i < researchCategories.Length; i++)
        {
            prettyPrint += researchCategories[i].getName() + "\n";
        }

        for(int i = 0; i < allTechnologies.Length; i++)
        {
            prettyPrint += allTechnologies[i].getAppropriateDescription();
            prettyPrint += string.Join(",", allTechnologies[i].GetResearchCategories().Select(p => p.getName()).ToArray());
            prettyPrint += "\n";
        }
        return prettyPrint;

    }

    public Technology[] getAllTechnologies()
    {
        return allTechnologies;
    }

}
