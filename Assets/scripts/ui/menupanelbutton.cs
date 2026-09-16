using UnityEngine;

public class MenuPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject panelToShow;
    [SerializeField] private GameObject panelToHide;

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
            if (panelToHide != null)
                panelToHide.SetActive(false);

            if (panelToShow != null)
                panelToShow.SetActive(true);
        }
    }
}