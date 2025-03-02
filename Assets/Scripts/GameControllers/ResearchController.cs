using UnityEngine;
using System.Collections;
 

public class ResearchController : MonoBehaviour
{

    public UIObjects uiObjects;

    public ResearchController(UIObjects objs)
    {
        uiObjects = objs;
    }

    public void handleObjectClicked(GameObject obj)
    {
        if (uiObjects.buttonGoToArmoryfromResearchTree == obj)
        {
            uiObjects.turnOffResearchTreeView();
            uiObjects.turnOnArmoryView();
        }
    }

    private void handleResearchButtonClicked()
    {

    }

    private void handleAdvanceWeekClicked()
    {

    }

    private void handleBacktoArmoryClicked()
    {

    }


    // Pick a research topic
    // Optional: pick multiple topics
    // Advance week
    // Go back to Armory
}


