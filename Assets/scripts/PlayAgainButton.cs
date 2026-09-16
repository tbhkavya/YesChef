using UnityEngine;

public class PlayAgainButton : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private RectTransform buttonRect;
    private Canvas canvas;

    private void Awake()
    {
        buttonRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.PlayAgain();
            return;
        }

     
        if (Input.GetMouseButtonDown(0))
        {
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
                Debug.Log("PLAY AGAIN CLICKED!");

                gameManager.PlayAgain();
            }
        }
    }
}