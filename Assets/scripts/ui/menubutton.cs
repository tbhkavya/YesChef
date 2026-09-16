using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;

    private RectTransform buttonRect;
    private Canvas canvas;

    private void Awake()
    {
        buttonRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Camera cam = null;

        if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
        {
            cam = canvas.worldCamera;
        }

        bool clicked =
            RectTransformUtility.RectangleContainsScreenPoint(
                buttonRect,
                Input.mousePosition,
                cam
            );

        if (clicked)
        {
            Debug.Log("START GAME CLICKED!");

            mainMenu.StartGame();
        }
    }
}