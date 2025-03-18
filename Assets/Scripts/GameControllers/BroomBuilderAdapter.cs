using UnityEngine;
using HexBoardGame.Runtime;
using HexBoardGame.Runtime.GameBoard;
using HexBoardGame.UI; // For UiTileMapInputHandler and UiHoverParticleSystem
using HexBoardGame.SharedData; // For HexagonalBoardDataShape
using UnityEngine.Tilemaps;
using System.Collections.Generic;

// Add this to the HexBoard GameObject
public class BroomBuilderAdapter : MonoBehaviour
{
    [SerializeField] private BoardController boardController;
    [SerializeField] private UiTileMapInputHandler inputHandler;
    [SerializeField] private UiHoverParticleSystem hoverParticleSystem;

    // Tile references - these will be created at runtime
    [SerializeField] private TileBase availableTile;
    [SerializeField] private TileBase occupiedTile;
    [SerializeField] private TileBase hoveredTile;
    [SerializeField] private TileBase invalidTile;

    // Optional: Color customization for tiles
    [Header("Tile Colors")]
    [SerializeField] private Color availableColor = new Color(0.4f, 0.7f, 1f);   // Blue
    [SerializeField] private Color occupiedColor = new Color(0.2f, 0.8f, 0.2f);  // Green
    [SerializeField] private Color hoveredColor = new Color(1f, 1f, 0.4f);       // Yellow
    [SerializeField] private Color invalidColor = new Color(1f, 0.4f, 0.4f);     // Red

    private BroomFrame currentFrame;
    private BroomShapeGraph shapeGraph;
    private Glyph selectedGlyph;
    private List<Glyph> placedGlyphs = new List<Glyph>();
    private Tilemap tilemap;
    private Dictionary<Hex, Vector3Int> hexToCellMap = new Dictionary<Hex, Vector3Int>();
    private Hex? hoverHex;

    private void Awake()
    {
        // Get references if not assigned
        if (boardController == null)
            boardController = GetComponent<BoardController>();
        if (inputHandler == null)
            inputHandler = GetComponentInChildren<UiTileMapInputHandler>();
        if (hoverParticleSystem == null)
            hoverParticleSystem = GetComponentInChildren<UiHoverParticleSystem>();

        tilemap = GetComponentInChildren<Tilemap>();

        // Create tile assets at runtime if not assigned
        CreateTilesAtRuntime();

        // Subscribe to events
        inputHandler.OnClickTile += HandleTileClick;

        // This will help us know when the board is ready
        boardController.OnCreateBoard += OnBoardCreated;
    }

    private void CreateTilesAtRuntime()
    {
        // Only create tiles if they aren't already assigned
        if (availableTile == null)
            availableTile = CreateColoredTile(availableColor);

        if (occupiedTile == null)
            occupiedTile = CreateColoredTile(occupiedColor);

        if (hoveredTile == null)
            hoveredTile = CreateColoredTile(hoveredColor);

        if (invalidTile == null)
            invalidTile = CreateColoredTile(invalidColor);
    }

    private TileBase CreateColoredTile(Color color)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();

        // Create a simple sprite
        Texture2D texture = new Texture2D(32, 32);
        Color[] pixels = new Color[32 * 32];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = color;

        texture.SetPixels(pixels);
        texture.Apply();

        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 32, 32), Vector2.one * 0.5f);
        tile.sprite = sprite;

        return tile;
    }

    private void OnBoardCreated(IBoard board)
    {
        Debug.Log("Board created for BroomBuilder");

        // Map all positions for quick lookup
        hexToCellMap.Clear();
        foreach (var position in board.Positions)
        {
            Vector3Int cell = BoardManipulationOddR.GetCellCoordinate(position.Point);
            hexToCellMap[position.Point] = cell;
        }

        // Initialize the board visuals for the current frame (if set)
        if (currentFrame != null)
        {
            UpdateBoardVisuals();
        }
    }

    // Call this to initialize the board with a broom frame
    public void SetupForBroomFrame(BroomFrame frame)
    {
        currentFrame = frame;
        shapeGraph = new BroomShapeGraph(frame.getShape());
        placedGlyphs.Clear();

        // Tell the board controller to create a hex grid of appropriate size
        // You can use one of your existing board shapes
        HexagonalBoardDataShape hexShape = ScriptableObject.CreateInstance<HexagonalBoardDataShape>();
        hexShape.radius = 3; // Size that fits your frame
        boardController.SetBoarDataAndCreate(hexShape);

        // Visuals will be updated via the OnBoardCreated callback
    }

    private void UpdateBoardVisuals()
    {
        // Clear tilemap first
        tilemap.ClearAllTiles();

        // Set tiles based on frame shape
        Shape frameShape = currentFrame.getShape();
        Hex[] frameHexes = frameShape.getHexes();

        foreach (var entry in hexToCellMap)
        {
            Hex hex = entry.Key;
            Vector3Int cell = entry.Value;

            // Check if this hex is part of the frame
            bool isInFrame = false;
            foreach (Hex frameHex in frameHexes)
            {
                if (hex.Equals(frameHex))
                {
                    isInFrame = true;
                    break;
                }
            }

            if (isInFrame)
            {
                // Check if occupied by a glyph
                bool isOccupied = !shapeGraph.willItFit(new Shape(new[] { hex }));

                // Set appropriate tile
                tilemap.SetTile(cell, isOccupied ? occupiedTile : availableTile);
            }
        }
    }

    public void SetSelectedGlyph(Glyph glyph)
    {
        selectedGlyph = glyph;
    }

    // Called by the BroomBuilderController during Update
    public void UpdateHover(Vector3 mouseWorldPosition)
    {
        if (selectedGlyph == null) return;

        // Convert world position to cell
        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPosition);

        // If there's a tile at this position
        if (tilemap.HasTile(cellPosition))
        {
            // Convert cell to hex
            Hex targetHex = BoardManipulationOddR.GetHexCoordinate(cellPosition);

            // If we're hovering over a new hex
            if (!hoverHex.HasValue || !hoverHex.Value.Equals(targetHex))
            {
                // Clear previous hover
                ClearHover();

                // Set new hover
                hoverHex = targetHex;

                // Show preview of glyph placement
                ShowGlyphPlacementPreview(targetHex);
            }
        }
        else if (hoverHex.HasValue)
        {
            // No longer hovering over a hex
            ClearHover();
        }
    }

    private void ClearHover()
    {
        hoverHex = null;
        UpdateBoardVisuals(); // Reset tile appearances
        if (hoverParticleSystem != null)
            hoverParticleSystem.Hide();
    }

    private void ShowGlyphPlacementPreview(Hex targetHex)
    {
        // Clone and move the glyph shape
        Shape previewShape = CloneAndMoveShape(selectedGlyph.GetShape(), targetHex);

        // Check if it's a valid placement
        bool isValid = shapeGraph.willItFit(previewShape);

        // Update visuals for all affected hexes
        foreach (Hex hex in previewShape.getHexes())
        {
            if (hexToCellMap.TryGetValue(hex, out Vector3Int cell))
            {
                // Set the appropriate tile
                tilemap.SetTile(cell, isValid ? hoveredTile : invalidTile);
            }
        }

        // Position the particle effect at the target hex (optional)
        if (hoverParticleSystem != null)
        {
            // Convert hex to world position for particles
            if (hexToCellMap.TryGetValue(targetHex, out Vector3Int cell))
            {
                Vector3 worldPos = tilemap.CellToWorld(cell);
                hoverParticleSystem.transform.position = worldPos;
                hoverParticleSystem.Show();
            }
        }
    }

    private void HandleTileClick(Vector3Int cellPosition)
    {
        if (selectedGlyph == null) return;

        // Convert cell to hex
        Hex targetHex = BoardManipulationOddR.GetHexCoordinate(cellPosition);

        // Try to place the glyph
        TryPlaceGlyphAt(targetHex);
    }

    public void RotateSelectedGlyph()
    {
        if (selectedGlyph != null)
        {
            // Rotate the glyph
            selectedGlyph.GetShape().rotateClockwise();

            // Update preview if hovering
            if (hoverHex.HasValue)
            {
                ShowGlyphPlacementPreview(hoverHex.Value);
            }
        }
    }

    private void TryPlaceGlyphAt(Hex targetHex)
    {
        // Move the glyph shape to the target position
        Shape movedShape = CloneAndMoveShape(selectedGlyph.GetShape(), targetHex);

        // Check if it fits
        if (shapeGraph.willItFit(movedShape))
        {
            // Place the glyph
            shapeGraph.addNewShape(movedShape);
            placedGlyphs.Add(selectedGlyph);
            selectedGlyph = null;
            hoverHex = null;

            // Update visuals
            UpdateBoardVisuals();
        }
    }

    private Shape CloneAndMoveShape(Shape originalShape, Hex targetHex)
    {
        // Create a copy of the original shape
        Hex[] originalHexes = originalShape.getHexes();
        Hex[] newHexes = new Hex[originalHexes.Length];
        System.Array.Copy(originalHexes, newHexes, originalHexes.Length);

        // Create a new shape with these hexes
        Shape newShape = new Shape(newHexes);

        // Move the shape to the target hex
        newShape.move(targetHex);

        return newShape;
    }

    // Call this when the player is done building
    public Broom FinalizeBroom()
    {
        if (currentFrame != null && placedGlyphs.Count > 0)
        {
            return new Broom(placedGlyphs.ToArray(), currentFrame);
        }
        return null;
    }
}