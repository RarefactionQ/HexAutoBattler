using UnityEngine;
using System.Collections.Generic;
using HexBoardGame.Runtime;

public class BroomBuilderController : MonoBehaviour
{
    public UIObjects uiObjects;

    [SerializeField] private BroomBuilderAdapter builderAdapter;
    [SerializeField] private Camera builderCamera;

    private BroomFrame selectedFrame;
    private Glyph selectedGlyph;
    private bool isPlacingGlyph = false;
    private List<Glyph> placedGlyphs = new List<Glyph>();

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
                handleRotateGlyphClicked();
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; // Set a distance from the camera
        return builderCamera.ScreenToWorldPoint(mousePos);
    }

    public void handlePickBroomFrameClicked()
    {
        // Show frame selection UI
        // For now, we'll just use a test frame
        selectedFrame = CreateTestBroomFrame();

        // Initialize the builder with this frame
        builderAdapter.SetupForBroomFrame(selectedFrame);

        // Clear any previously placed glyphs
        placedGlyphs.Clear();
    }

    private BroomFrame CreateTestBroomFrame()
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

        Shape frameShape = new Shape(frameHexes);

        // light, medium, heavy weights, flux, summon cost, speed, agility, durability, cost, shape
        return new BroomFrame(10, 15, 20, 100, 50, 5, 3, 10, 100, frameShape);
    }

    public void handlePickGlyphClicked()
    {
        // Show glyph selection UI
        // For now, we'll just use a test glyph
        selectedGlyph = CreateTestGlyph();

        // Tell the adapter about the selected glyph
        builderAdapter.SetSelectedGlyph(selectedGlyph);

        // Enter placement mode
        isPlacingGlyph = true;
    }

    private Glyph CreateTestGlyph()
    {
        // Create a simple glyph for testing (L shape)
        Hex[] glyphHexes = new Hex[]
        {
            new Hex(0, 0, 0),
            new Hex(1, -1, 0),
            new Hex(0, -1, 1)
        };

        StatBlock stats = new StatBlock(new[] { 1, 2, 1, 0, 0, 0, 0, 0 });

        // Return a new Glyph with the given shape, stats, and some arbitrary values
        return new Glyph(glyphHexes, stats, 5, 20, 3);
    }

    public void handleRotateGlyphClicked() // This is right click vs. left click for everything else
    {
        if (selectedGlyph != null)
        {
            // Tell the adapter to rotate the glyph
            builderAdapter.RotateSelectedGlyph();
        }
    }

    public void handlePlaceGlyphClicked()
    {
        // Placing is now handled by the adapter through the UiTileMapInputHandler
        // We just need to reset our state when placement occurs

        // The adapter will create a new glyph when placement succeeds
        // We can check if the selectedGlyph was reset to null by the adapter
        if (selectedGlyph == null)
        {
            isPlacingGlyph = false;
        }
    }

    public void handleCreateBroomClicked()
    {
        // Get the finalized broom from the adapter
        Broom newBroom = builderAdapter.FinalizeBroom();

        if (newBroom != null)
        {
            // Here you would add the broom to the player's inventory
            // For now, just log the creation
            Debug.Log("Created new broom with glyphs");

            // Reset the builder
            selectedFrame = null;
            selectedGlyph = null;
            isPlacingGlyph = false;

            // Go back to the armory
            handleBackToArmoryClicked();
        }
        else
        {
            Debug.Log("Cannot create broom - need a frame and at least one glyph");
        }
    }

    public void handleBackToArmoryClicked()
    {
        // Clean up
        selectedFrame = null;
        selectedGlyph = null;
        isPlacingGlyph = false;

        // Switch to armory view
        uiObjects.turnOffBroomBuilderView();
        uiObjects.turnOnArmoryView();
    }
}