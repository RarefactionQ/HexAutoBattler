using UnityEngine;
using System.Collections;

public class BattleController : MonoBehaviour
{
    public UIObjects uiObjects;

    public BattleController(UIObjects objs)
    {
        uiObjects = objs;
    }

    public void handleObjectClicked(GameObject obj)
    {
        if (uiObjects.buttonGoToArmoryfromBattle == obj)
        {
            uiObjects.turnOffBattleView();
            uiObjects.turnOnArmoryView();
        }
    }
}
