using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Figure out what state updates as a result of a user action

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI mainDisplay;
    public GameObject canvas;
    public UIObjects uiObjects;
    private GameStateManager gameState;


    private void Start()
    {

        mainDisplay.SetText("This is a test!!!!");

        // Set up Data Model
        gameState = new GameStateManager(uiObjects);



        // Initialization
    }

    private void Update()
    {
       
        //Debug.Log(uiObjects.testButton.ToString());
        //Debug.Log(uiObjects.testButton.name);


        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            GameObject obj = null;

            if (hit)
            {
                //Debug.Log("hit was true");
                obj = hit.collider.gameObject;
            }

            if (obj == null)
            {
                //Do nothing
                //Debug.Log("Didn't click on anything");
                return;
            }

            gameState.handleClick(obj);
        }

    }

    // Figure out what is clicked on

    // Huge Tree of if statements
    // What should happen when a gameObject is clicked
    // UIController basically just holds all of the Unity GameObjects
    // It can tell you what object was clicked
    // They are hidden and shown rather than created
    // tell GameState that it happened (also massive)
    // figure out what changed

    // Call the various controllers which ask ONLY FOR WHAT THEY NEED FROM THE GAME STATE




}