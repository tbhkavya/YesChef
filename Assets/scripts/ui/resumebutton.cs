using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    [SerializeField] private PauseManager pauseManager;

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
            cam = canvas.worldCamera;

        bool clicked =
            RectTransformUtility.RectangleContainsScreenPoint(
                buttonRect,
                Input.mousePosition,
                cam
            );

        if (clicked)
        {
            pauseManager.ResumeGame();
        }
    }
}