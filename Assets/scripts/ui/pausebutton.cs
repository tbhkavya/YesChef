using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private PauseManager pauseManager;

    private RectTransform buttonRect;
    private Canvas canvas;
    [SerializeField] private GameManager gameManager;
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
            cam = canvas.worldCamera;

        bool clicked =
            RectTransformUtility.RectangleContainsScreenPoint(
                buttonRect,
                Input.mousePosition,
                cam
            );

        if (clicked)
        {
            if (!gameManager.IsGameRunning)
                return;

            pauseManager.PauseGame();
        }
    }
}