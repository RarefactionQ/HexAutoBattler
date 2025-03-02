using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager:MonoBehaviour
{
    public UIObjects uiObjects;

    private Generator faker;
    private ResearchTree researchTree;
    // Set up all data stores here


    // View Controllers
    private ArmoryController armoryController;
    private BattleController battleController;
    private BroomBuilderController broomBuilderController;
    private ResearchController researchController;

    public GameStateManager(UIObjects ui)
    {
        uiObjects = ui;
        faker = new Generator();
        researchTree = faker.getFakeTree();
        // Use the generator to build all of the data stores




        // Build View Controllers
        armoryController = new ArmoryController(uiObjects);
        battleController = new BattleController(uiObjects);
        broomBuilderController = new BroomBuilderController(uiObjects);
        researchController = new ResearchController(uiObjects);


        // Populate View Controllers

        buildResearchTreeNodes();

    }

    public ResearchTree getResearchTree() 
    {
        return researchTree;
    }

    public void buildResearchTreeNodes()
    {
        Technology[] technologies = getResearchTree().getAllTechnologies();

        GameObject allResearchNodes = new GameObject("allResearchNodes");
        allResearchNodes.transform.SetParent(uiObjects.ResearchTreeGameObjects.transform);

        for (int i = 0; i < technologies.Length; i++)
        {
            //Debug.Log("prefab: " + uiObjects.singleResearchNode + " ResearchTreeGameObj: " + uiObjects.ResearchTreeGameObjects);
            GameObject newTech = Instantiate(uiObjects.singleResearchNode, allResearchNodes.transform);
            newTech.name = technologies[i].getName();
            newTech.transform.SetPositionAndRotation
                (new Vector3(0, technologies[i].getVisibility() / 10), Quaternion.identity);
        }

        uiObjects.addGameObjectResearchTree(allResearchNodes);
    }


    public void handleClick(GameObject obj)
    {
        GameObject viewHolder = getViewHolderGameObject(obj);

        if (viewHolder == uiObjects.ArmoryGameObjects)
        {
            handleArmoryObjectClicked(obj);
        }
        else if (viewHolder == uiObjects.BroomBuilderGameObjects)
        {
            handleBroomBuilderObjectClicked(obj);
        }
        else if (viewHolder == uiObjects.ResearchTreeGameObjects)
        {
            handleResearchTreeObjectClicked(obj);
        }
        else if (viewHolder == uiObjects.BattleGameObjects)
        {
            handleBattleObjectClicked(obj);
        }

        else
        {
            Debug.Log("something went wrong");
        }
    }

    private void handleArmoryObjectClicked(GameObject obj) {
        if (uiObjects.buttonGoToBroomBuilder == obj)
        {
            uiObjects.turnOffArmoryView();
            uiObjects.turnOnBroomBuilderView();
        }
        else if (uiObjects.buttonGoToResearchTree == obj)
        {
            uiObjects.turnOffArmoryView();
            uiObjects.turnOnResearchTreeView();
        }
        else if (uiObjects.buttonGoToBattle == obj)
        {
            uiObjects.turnOffArmoryView();
            uiObjects.turnOnBattleView();
        }
    }

    private void handleBroomBuilderObjectClicked(GameObject obj) {
        if (uiObjects.buttonGoToArmoryfromBroomBuilder == obj)
        {
            uiObjects.turnOffBroomBuilderView();
            uiObjects.turnOnArmoryView();
        }
    }

    private void handleResearchTreeObjectClicked(GameObject obj) {
        if (uiObjects.buttonGoToArmoryfromResearchTree == obj)
        {
            uiObjects.turnOffResearchTreeView();
            uiObjects.turnOnArmoryView();
        }
    }

    private void handleBattleObjectClicked(GameObject obj) {
        if (uiObjects.buttonGoToArmoryfromBattle == obj)
        {
            uiObjects.turnOffBattleView();
            uiObjects.turnOnArmoryView();
        }
    }

    //Get 2nd to highest GameObject in hierarchy
    private GameObject getViewHolderGameObject(GameObject obj)
    {
        if (obj.transform.parent == null)
        {
            Debug.Log("ERROR. GAMEOBJECT HAS NO PARENT");
            throw new Exception();
        }

        GameObject cur = obj;
        while(cur.transform.root != cur.transform.parent)
        {
            cur = cur.transform.parent.gameObject;
        }
        return cur;
    }
}
