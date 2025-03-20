using UnityEngine;
using UnityEngine.UI;

// Attach this script to a canvas in the scene to create testing UI
public class BroomBuilderTestUI : MonoBehaviour
{
    [SerializeField] private BroomBuilderController broomController;

    // UI elements
    private Button frameButton;
    private Button[] glyphButtons = new Button[3];
    private Button rotateButton;
    private Button finishButton;
    private Button backButton;

    void Start()
    {
        if (broomController == null)
        {
            broomController = FindObjectOfType<BroomBuilderController>();
            if (broomController == null)
            {
                Debug.LogError("BroomBuilderController not found in scene!");
                return;
            }
        }

        // Create a simple UI for testing
        CreateTestUI();
    }

    void CreateTestUI()
    {
        // Create a canvas if not attached to one
        Canvas canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Add canvas scaler
            CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            // Add raycaster
            gameObject.AddComponent<GraphicRaycaster>();
        }

        // Create a panel for the UI
        GameObject panel = new GameObject("Test UI Panel");
        panel.transform.SetParent(transform, false);
        RectTransform panelRect = panel.AddComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0, 0);
        panelRect.anchorMax = new Vector2(0, 1);
        panelRect.pivot = new Vector2(0, 0.5f);
        panelRect.sizeDelta = new Vector2(200, 0);

        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        // Add a vertical layout group
        VerticalLayoutGroup layout = panel.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(10, 10, 10, 10);
        layout.spacing = 10;
        layout.childAlignment = TextAnchor.UpperCenter;
        layout.childControlWidth = true;
        layout.childControlHeight = false;
        layout.childForceExpandWidth = true;
        layout.childForceExpandHeight = false;

        // Create buttons
        frameButton = CreateButton(panel, "Select Frame", () => broomController.HandlePickBroomFrame());

        // Create glyph buttons
        glyphButtons[0] = CreateButton(panel, "Glyph 1 (L Shape)", () => broomController.HandlePickGlyph(0));
        glyphButtons[1] = CreateButton(panel, "Glyph 2 (Line)", () => broomController.HandlePickGlyph(1));
        glyphButtons[2] = CreateButton(panel, "Glyph 3 (Triangle)", () => broomController.HandlePickGlyph(2));

        rotateButton = CreateButton(panel, "Rotate Glyph (R)", () => broomController.HandleRotateGlyph());
        finishButton = CreateButton(panel, "Finish Broom", () => broomController.HandleFinishBroom());
        backButton = CreateButton(panel, "Back to Armory", () => broomController.HandleBackToArmory());

        // Add instructions text
        GameObject instructionsObj = new GameObject("Instructions");
        instructionsObj.transform.SetParent(panel.transform, false);

        Text instructions = instructionsObj.AddComponent<Text>();
        instructions.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        instructions.fontSize = 14;
        instructions.color = Color.white;
        instructions.text = "Left-click to place\nRight-click to rotate";
        instructions.alignment = TextAnchor.MiddleCenter;

        RectTransform instructRect = instructions.GetComponent<RectTransform>();
        instructRect.sizeDelta = new Vector2(180, 60);
    }

    Button CreateButton(GameObject parent, string text, UnityEngine.Events.UnityAction action)
    {
        GameObject buttonObj = new GameObject(text);
        buttonObj.transform.SetParent(parent.transform, false);

        // Add image component
        Image image = buttonObj.AddComponent<Image>();
        image.color = new Color(0.3f, 0.3f, 0.3f);

        // Add text component
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);

        Text buttonText = textObj.AddComponent<Text>();
        buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        buttonText.text = text;
        buttonText.fontSize = 14;
        buttonText.alignment = TextAnchor.MiddleCenter;
        buttonText.color = Color.white;

        RectTransform textRect = buttonText.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        // Set up the button
        Button button = buttonObj.AddComponent<Button>();
        button.targetGraphic = image;

        // Set up transitions
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.3f, 0.3f, 0.3f);
        colors.highlightedColor = new Color(0.4f, 0.4f, 0.4f);
        colors.pressedColor = new Color(0.2f, 0.2f, 0.2f);
        button.colors = colors;

        // Add listener
        button.onClick.AddListener(action);

        // Set rect transform
        RectTransform rect = buttonObj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(180, 40);

        return button;
    }

    void Update()
    {
        // You can add keyboard shortcuts here if you want
        if (Input.GetKeyDown(KeyCode.R))
        {
            broomController.HandleRotateGlyph();
        }
    }
}