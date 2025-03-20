using UnityEngine;
using System.Collections.Generic;
using HexBoardGame.Runtime;

public class BroomBuilderController : MonoBehaviour
{
    public UIObjects uiObjects;

    [SerializeField] private BroomBuilderAdapter builderAdapter;
    [SerializeField] private Camera builderCamera;

    // Test glyphs and frame
    private BroomFrame testFrame;
    private List<Glyph> testGlyphs = new List<Glyph>();
    private Glyph selectedGlyph;
    private bool isPlacingGlyph = false;

    // Constructor used by the GameStateManager
    public BroomBuilderController(UIObjects objs)
    {
        uiObjects = objs;
    }

    private void Awake()
    {
        // Make sure we have references
        if (builderAdapter == null)
        {
            builderAdapter = FindObjectOfType<BroomBuilderAdapter>();
        }

        if (builderCamera == null)
        {
            builderCamera = Camera.main;
        }

        CreateTestObjects();
    }

    private void Start()
    {
        // Initialize the board with the test frame when the scene starts
        if (testFrame != null && builderAdapter != null)
        {
            builderAdapter.SetupForBroomFrame(testFrame);
        }
    }

    private void Update()
    {
        if (isPlacingGlyph && selectedGlyph != null)
        {
            // Get mouse world position
            Vector3 mouseWorldPos = GetMouseWorldPosition();

            // Update the hover preview in the adapter
            builderAdapter.UpdateHover(mouseWorldPos);

            // Handle rotation with right click
            if (Input.GetMouseButtonDown(1))
            {
                HandleRotateGlyph();
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; // Set a distance from the camera
        return builderCamera.ScreenToWorldPoint(mousePos);
    }

    // Create test objects for development
    private void CreateTestObjects()
    {
        try
        {
            // Create a simple hexagonal frame for testing
            Hex[] frameHexes = new Hex[]
            {
            new Hex(0, 0, 0),
            new Hex(1, -1, 0),
            new Hex(1, 0, -1),
            new Hex(0, 1, -1),
            new Hex(-1, 1, 0),
            new Hex(-1, 0, 1),
            new Hex(0, -1, 1)
            };

            // Verify the frameHexes array is valid
            if (frameHexes == null || frameHexes.Length == 0)
            {
                Debug.LogError("Failed to create frameHexes array");
                return;
            }

            Shape frameShape = new Shape(frameHexes);
            if (frameShape == null)
            {
                Debug.LogError("Failed to create frameShape");
                return;
            }

            // light, medium, heavy weights, flux, summon cost, speed, agility, durability, cost, shape
            testFrame = new BroomFrame(10, 15, 20, 100, 50, 5, 3, 10, 100, frameShape);
            if (testFrame == null)
            {
                Debug.LogError("Failed to create testFrame");
                return;
            }

            // Initialize the list if it's null
            if (testGlyphs == null)
            {
                testGlyphs = new List<Glyph>();
            }

            // Create test glyph shapes
            Hex[] lShapeHexes = new Hex[] { new Hex(0, 0, 0), new Hex(1, -1, 0), new Hex(0, -1, 1) };
            Hex[] lineShapeHexes = new Hex[] { new Hex(0, 0, 0), new Hex(1, -1, 0), new Hex(2, -2, 0) };
            Hex[] triangleShapeHexes = new Hex[] { new Hex(0, 0, 0), new Hex(1, -1, 0), new Hex(1, 0, -1) };

            // Create a basic stat block
            int[] statValues = new int[] { 1, 2, 1, 0, 0, 0, 0, 0 };
            StatBlock stats = new StatBlock(statValues);

            // Create and add glyphs to the list
            try
            {
                testGlyphs.Add(new Glyph(lShapeHexes, stats, 5, 20, 3));
                testGlyphs.Add(new Glyph(lineShapeHexes, stats, 5, 20, 3));
                testGlyphs.Add(new Glyph(triangleShapeHexes, stats, 5, 20, 3));

                Debug.Log("Successfully created test objects with " + testGlyphs.Count + " glyphs");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error creating glyphs: " + e.Message);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in CreateTestObjects: " + e.Message + "\n" + e.StackTrace);
        }
    }

    // Public methods to be called by UI buttons

    public void HandlePickBroomFrame()
    {
        if (testFrame != null && builderAdapter != null)
        {
            builderAdapter.SetupForBroomFrame(testFrame);
            Debug.Log("Selected broom frame");
        }
    }

    public void HandlePickGlyph(int glyphIndex = 0)
    {
        if (testGlyphs.Count > 0 && glyphIndex < testGlyphs.Count)
        {
            selectedGlyph = testGlyphs[glyphIndex];
            builderAdapter.SetSelectedGlyph(selectedGlyph);
            isPlacingGlyph = true;
            Debug.Log("Selected glyph: " + glyphIndex);
        }
    }

    public void HandleRotateGlyph()
    {
        if (selectedGlyph != null)
        {
            builderAdapter.RotateSelectedGlyph();
            Debug.Log("Rotated glyph");
        }
    }

    public void HandleFinishBroom()
    {
        Broom newBroom = builderAdapter.FinalizeBroom();
        if (newBroom != null)
        {
            Debug.Log("Broom created successfully with " + newBroom.GetGlyphCount() + " glyphs");
        }
        else
        {
            Debug.Log("Failed to create broom");
        }
    }

    public void HandleBackToArmory()
    {
        selectedGlyph = null;
        isPlacingGlyph = false;
        uiObjects.turnOffBroomBuilderView();
        uiObjects.turnOnArmoryView();
    }
}