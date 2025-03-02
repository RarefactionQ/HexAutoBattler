using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIObjects: MonoBehaviour
{
    // Parent GameObjects which hold each "view"
    public GameObject ArmoryGameObjects; 
    public GameObject BroomBuilderGameObjects; 
    public GameObject ResearchTreeGameObjects; 
    public GameObject BattleGameObjects; 

    //Armory GameObjects

    public GameObject buttonGoToBroomBuilder; 
    public GameObject buttonGoToResearchTree; 
    public GameObject buttonGoToBattle; 

    public GameObject pickBroomFrameGameObject; 
    public GameObject orderBroomFrameGameObject;
    public GameObject cancelOrderBroomFrameGameObject;

    public GameObject pickGlyphGameObject;
    public GameObject orderGlyphGameObject;
    public GameObject cancelOrderGlyphGameObject;

    public GameObject displayArmoryState;

    // BroomBuilder GameObjects

    public GameObject buttonGoToArmoryfromBroomBuilder; 

    public GameObject broomFramePicker; 
    public GameObject glyphShelf;

    public GameObject singleHex;

    // ResearchTree GameObjects
    
    public GameObject buttonGoToArmoryfromResearchTree; 

    public GameObject categoryLayout;
    public GameObject currentResearchHighlight;
    public GameObject singleResearchNode; //Prefab of what research nodes look like

    public GameObject[] allResearchNodes;

    public GameObject buttonAdvanceWeek; 

    // Battle GameObjects

    public GameObject buttonGoToArmoryfromBattle; 

    public GameObject buttonStartBattle; 

    private void Start()
    {

    }

    private void Update()
    {
        
    }



    public void addGameObjectResearchTree(GameObject obj)
    {
        // GameController builds the research tree
        obj.transform.parent = ResearchTreeGameObjects.transform;
    }

    // additional functions for adding GameObjects to different "views" e.g. BroomBuilder, Battle, etc.

    public void removeGameObject(GameObject obj)
    {
        // Go through all of my GameObjects and throw away the matching one
    }

    //public void buttonResearchSelected(GameObject obj)
    //{

    //}

    public void turnOffArmoryView()
    {
        ArmoryGameObjects.SetActive(false);
    }

    public void turnOnArmoryView()
    {
        ArmoryGameObjects.SetActive(true);
    }

    public void turnOffBroomBuilderView()
    {
        BroomBuilderGameObjects.SetActive(false);
    }

    public void turnOnBroomBuilderView()
    {
        BroomBuilderGameObjects.SetActive(true);
    }

    public void turnOffResearchTreeView()
    {
        ResearchTreeGameObjects.SetActive(false);
    }

    public void turnOnResearchTreeView()
    {
        ResearchTreeGameObjects.SetActive(true);
    }

    public void turnOffBattleView()
    {
        BattleGameObjects.SetActive(false);
    }

    public void turnOnBattleView()
    {
        BattleGameObjects.SetActive(true);
    }
}
